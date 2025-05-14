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
            string sql = "SELECT  dv.MaDV, dv.TenDV, dv.DonGia,  p.MaPhong,  p.SoPhong, sdv.SoLuong FROM SuDungDichVu sdv JOIN DichVu dv ON sdv.MaDV = dv.MaDV JOIN DatPhong dp ON sdv.MaDatPhong = dp.MaDatPhong JOIN Phong p ON dp.MaPhong = p.MaPhong;";
            var dataTable = connDb.ExecuteQuery(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                BookingRoomModel dichvu = new BookingRoomModel
                {
                    MaDichVu = Convert.ToInt32(row["MaDV"]),
                    SoPhong = Convert.ToInt32(row["SoPhong"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"])
                };
                listDichVu.Add(dichvu);
            }
            return listDichVu;
        }
        public bool insertBookingRoom(BookingRoomModel bookingRoom)
        {
            // Kiểm tra MaKH có hợp lệ không
            if (bookingRoom.MaKH <= 0)
            {
                MessageBox.Show("Mã khách hàng không hợp lệ.");
                return false;
            }

            // 1. Lấy MaPhong từ SoPhong
            string getMaPhongSql = "SELECT MaPhong FROM Phong WHERE SoPhong = @SoPhong";
            SqlParameter[] getMaPhongParams = new SqlParameter[] {
        new SqlParameter("@SoPhong", bookingRoom.SoPhong)
    };
            object maPhongObj = connDb.ExecuteScalar(getMaPhongSql, getMaPhongParams);
            if (maPhongObj == null)
            {
                MessageBox.Show("Không tìm thấy phòng với số phòng đã cho.");
                return false;
            }

            int maPhong = Convert.ToInt32(maPhongObj);

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


    }
}
