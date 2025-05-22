using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class BookingRoomRepository
    {

        private readonly ConnectDB connDb = new ConnectDB();
        public List<BookingRoomModel> getDichVu()
        {
            List<BookingRoomModel> listDichVu = new List<BookingRoomModel>();
            string sql = @"SELECT 
                        dv.MaDV, dv.TenDV, dv.DonGia, 
                        p.MaPhong, p.SoPhong, 
                        sdv.SoLuong,
                        dp.MaKH,
                        dp.MaNV
                   FROM SuDungDichVu sdv
                   JOIN DichVu dv ON sdv.MaDV = dv.MaDV
                   JOIN DatPhong dp ON sdv.MaDatPhong = dp.MaDatPhong
                   JOIN Phong p ON dp.MaPhong = p.MaPhong";

            var dataTable = connDb.ExecuteQuery(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                BookingRoomModel dichvu = new BookingRoomModel
                {
                    MaDichVu = Convert.ToInt32(row["MaDV"]),
                    TenDichVu = row["TenDV"].ToString(),
                    DonGia = Convert.ToDecimal(row["DonGia"]),
                    SoPhong = Convert.ToInt32(row["SoPhong"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    MaNV = Convert.ToInt32(row["MaNV"])
                };
                listDichVu.Add(dichvu);
            }
            return listDichVu;
        }

        public List<BookingRoomModel> GetAllPhongDat()
        {
            List<BookingRoomModel> listPhongDat = new List<BookingRoomModel>();

            string sql = @"
                SELECT DISTINCT
                    p.MaPhong,
                    p.SoPhong,
                    p.MaLoaiPhong,
                    p.TrangThai,
                    lp.TenLoaiPhong,
                    lp.GiaPhong
                FROM 
                    Phong p
                JOIN 
                    LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
                JOIN 
                    DatPhong dp ON p.MaPhong = dp.MaPhong
                WHERE 
                    p.TrangThai = @TrangThai";

            SqlParameter[] parameters = {
                new SqlParameter("@TrangThai", "Đã đặt")
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                listPhongDat.Add(new BookingRoomModel
                {
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    SoPhong = Convert.ToInt32(row["SoPhong"]),
                    MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TrangThai = row["TrangThai"].ToString(),
                    TenLoaiPhong = row["TenLoaiPhong"].ToString(),
                    GiaPhong = Convert.ToDecimal(row["GiaPhong"])
                });
            }

            return listPhongDat;
        }
        public decimal GetGiaPhongTheoSoPhong(int soPhong)
        {
            string sql = @"
                SELECT lp.GiaPhong
                FROM Phong p
                JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
                WHERE p.SoPhong = @SoPhong";

            SqlParameter[] parameters = {
                new SqlParameter("@SoPhong", soPhong)
            };

            object result = connDb.ExecuteScalar(sql, parameters);
            if (result != null && decimal.TryParse(result.ToString(), out decimal giaPhong))
            {
                return giaPhong;
            }
            return 0; // Nếu không tìm thấy giá phòng trả về 0
        }




        public bool insertBookingRoom(BookingRoomModel bookingRoom)
        {
            // Kiểm tra MaKH có hợp lệ không
            if (bookingRoom.MaKH <= 0)
            {
                MessageBox.Show("Mã khách hàng không hợp lệ.");
                return false;
            }

            // 1. Lấy MaPhong và TrangThai từ SoPhong
            string getPhongSql = "SELECT MaPhong, TrangThai FROM Phong WHERE SoPhong = @SoPhong";
            SqlParameter[] getPhongParams = new SqlParameter[] {
        new SqlParameter("@SoPhong", bookingRoom.SoPhong)
    };
            DataTable phongTable = connDb.ExecuteQuery(getPhongSql, getPhongParams);

            if (phongTable.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy phòng với số phòng đã cho.");
                return false;
            }

            string trangThai = phongTable.Rows[0]["TrangThai"].ToString();
            if (!trangThai.Equals("Trống", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Phòng đã được đặt hoặc không thể đặt.");
                return false;
            }

            int maPhong = Convert.ToInt32(phongTable.Rows[0]["MaPhong"]);

            // 2. Insert vào bảng DatPhong
            string insertDatPhongSql = @"
        INSERT INTO DatPhong (MaPhong, NgayDat, NgayNhanPhong, NgayTraPhong, MaKH, MaNV)
        OUTPUT INSERTED.MaDatPhong
        VALUES (@MaPhong, @NgayDat, @NgayNhan, @NgayTra, @MaKH, @MaNV)";
            SqlParameter[] datPhongParams = new SqlParameter[] {
        new SqlParameter("@MaPhong", maPhong),
        new SqlParameter("@NgayDat", bookingRoom.NgayDat),
        new SqlParameter("@NgayNhan", bookingRoom.NgayNhan),
        new SqlParameter("@NgayTra", bookingRoom.NgayTra),
        new SqlParameter("@MaKH", bookingRoom.MaKH),
        new SqlParameter("@MaNV", bookingRoom.MaNV)
    };
            object maDatPhongObj = connDb.ExecuteScalar(insertDatPhongSql, datPhongParams);
            if (maDatPhongObj == null)
            {
                MessageBox.Show("Không thể thêm đặt phòng.");
                return false;
            }


            int maDatPhong = Convert.ToInt32(maDatPhongObj);

            // 3. Insert vào bảng SuDungDichVu
            string insertDichVuSql = @"
                INSERT INTO SuDungDichVu (MaDV, MaDatPhong, SoLuong)
                VALUES (@MaDV, @MaDatPhong, @SoLuong)";
            SqlParameter[] dichVuParams = new SqlParameter[] {
                new SqlParameter("@MaDV", bookingRoom.MaDichVu),
                new SqlParameter("@MaDatPhong", maDatPhong),
                new SqlParameter("@SoLuong", bookingRoom.SoLuong)
            };

            return connDb.ExecuteNonQuery(insertDichVuSql, dichVuParams) > 0;
        }

        public bool CapNhatTrangThaiPhong_DaDat(BookingRoomModel bookingRoomModel)
        {
            string sql = "UPDATE Phong SET TrangThai = @TrangThai WHERE SoPhong = @SoPhong";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TrangThai", "Đã đặt"),
                new SqlParameter("@SoPhong", bookingRoomModel.SoPhong)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }
        public bool CapNhatTrangThaiPhong_Trong(BookingRoomModel bookingRoomModel)
        {
            string sql = "UPDATE Phong SET TrangThai = @TrangThai WHERE SoPhong = @SoPhong";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TrangThai", "Trống"),
                new SqlParameter("@SoPhong", bookingRoomModel.SoPhong)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool HuyDatPhong(BookingRoomModel bookingRoomModel)
        {
            // 1. Lấy MaPhong nếu chưa có
            if (bookingRoomModel.MaPhong == 0)
            {
                string sqlGetMaPhong = "SELECT MaPhong FROM Phong WHERE SoPhong = @SoPhong";
                SqlParameter[] paramPhong = { new SqlParameter("@SoPhong", bookingRoomModel.SoPhong) };
                object maPhongObj = connDb.ExecuteScalar(sqlGetMaPhong, paramPhong);
                if (maPhongObj == null) return false;
                bookingRoomModel.MaPhong = Convert.ToInt32(maPhongObj);
            }

            // 2. Lấy MaDatPhong nếu chưa có
            if (bookingRoomModel.MaDatPhong == 0)
            {
                string sqlGetMaDatPhong = "SELECT MaDatPhong FROM DatPhong WHERE MaPhong = @MaPhong";
                SqlParameter[] paramDatPhong = { new SqlParameter("@MaPhong", bookingRoomModel.MaPhong) };
                object maDatPhongObj = connDb.ExecuteScalar(sqlGetMaDatPhong, paramDatPhong);
                if (maDatPhongObj == null) return false;
                bookingRoomModel.MaDatPhong = Convert.ToInt32(maDatPhongObj);
            }

            // 3. Xóa SuDungDichVu
            string sqlDeleteDV = "DELETE FROM SuDungDichVu WHERE MaDatPhong = @MaDatPhong";
            SqlParameter[] paramDeleteDV = { new SqlParameter("@MaDatPhong", bookingRoomModel.MaDatPhong) };
            connDb.ExecuteNonQuery(sqlDeleteDV, paramDeleteDV);

            // 4. Xóa DatPhong (KHÔNG dùng lại paramDeleteDV)
            string sqlDeleteDP = "DELETE FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
            SqlParameter[] paramDeleteDP = { new SqlParameter("@MaDatPhong", bookingRoomModel.MaDatPhong) };
            connDb.ExecuteNonQuery(sqlDeleteDP, paramDeleteDP);

            // 5. Cập nhật trạng thái phòng thành "Trống"
            string sqlUpdateTrangThai = "UPDATE Phong SET TrangThai = N'Trống' WHERE MaPhong = @MaPhong";
            SqlParameter[] paramUpdatePhong = { new SqlParameter("@MaPhong", bookingRoomModel.MaPhong) };
            connDb.ExecuteNonQuery(sqlUpdateTrangThai, paramUpdatePhong);

            return true;
        }
        public bool HuyDichVuTheoSoPhongVaMaDV(BookingRoomModel bookingRoomModel)
        {
            try
            {
                // Lấy MaPhong từ SoPhong
                string sqlGetMaPhong = "SELECT MaPhong FROM Phong WHERE SoPhong = @SoPhong";
                SqlParameter[] paramPhong = { new SqlParameter("@SoPhong", bookingRoomModel.SoPhong) };
                object maPhongObj = connDb.ExecuteScalar(sqlGetMaPhong, paramPhong);
                if (maPhongObj == null) return false;

                int maPhong = Convert.ToInt32(maPhongObj);

                // Lấy MaDatPhong từ MaPhong
                string sqlGetMaDatPhong = "SELECT MaDatPhong FROM DatPhong WHERE MaPhong = @MaPhong";
                SqlParameter[] paramDatPhong = { new SqlParameter("@MaPhong", maPhong) };
                object maDatPhongObj = connDb.ExecuteScalar(sqlGetMaDatPhong, paramDatPhong);
                if (maDatPhongObj == null) return false;

                int maDatPhong = Convert.ToInt32(maDatPhongObj);

                // Lấy MaDV từ model
                int maDV = bookingRoomModel.MaDichVu;

                // Xóa dịch vụ dựa trên MaDatPhong và MaDV
                string sqlDeleteDV = "DELETE FROM SuDungDichVu WHERE MaDatPhong = @MaDatPhong AND MaDV = @MaDV";
                SqlParameter[] paramXoa = {
                    new SqlParameter("@MaDatPhong", maDatPhong),
                    new SqlParameter("@MaDV", maDV)
                };

                return connDb.ExecuteNonQuery(sqlDeleteDV, paramXoa) > 0;
            }
            catch
            {
                return false;
            }
        }



    }
}
