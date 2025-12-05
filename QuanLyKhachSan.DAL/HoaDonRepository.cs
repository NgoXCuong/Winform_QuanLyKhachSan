using QuanLyKhachSan.Models; // Sử dụng Namespace chứa Model HoaDon mới
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyKhachSan.DAL
{
    public class HoaDonRepository
    {
        private readonly ConnectDB _db = new ConnectDB();

        // Lấy danh sách hiển thị lên Grid chính
        public List<HoaDonModel> GetAll()
        {
            List<HoaDonModel> list = new List<HoaDonModel>();
            string query = @"
                SELECT hd.*, kh.HoTen AS TenKhachHang, p.SoPhong, dp.MaKH,
                       (hd.TongTienThanhToan - ISNULL(hd.TienPhong, 0)) AS TienDichVu
                FROM HoaDon hd
                JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                JOIN Phong p ON dp.MaPhong = p.MaPhong
                ORDER BY hd.NgayTao DESC";

            DataTable dt = _db.ExecuteQuery(query);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new HoaDonModel
                {
                    MaHD = Convert.ToInt32(row["MaHD"]),
                    MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                    MaKH = Convert.ToInt32(row["MaKH"]), // Lấy từ DatPhong
                    TienPhong = Convert.ToDecimal(row["TienPhong"]),
                    TongTienThanhToan = Convert.ToDecimal(row["TongTienThanhToan"]),
                    TrangThaiThanhToan = row["TrangThaiThanhToan"].ToString(),
                    PhuongThucThanhToan = row["PhuongThucThanhToan"]?.ToString(),
                    NgayThanhToan = row["NgayThanhToan"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgayThanhToan"]),
                    GhiChu = row["GhiChu"]?.ToString(),
                    NgayTao = Convert.ToDateTime(row["NgayTao"]),
                    // Các trường hiển thị
                    TenKhachHang = row["TenKhachHang"].ToString(),
                    SoPhong = row["SoPhong"].ToString(),
                    TienDichVu = Convert.ToDecimal(row["TienDichVu"])
                });
            }
            return list;
        }

        // Thêm vào trong class HoaDonRepository
        public HoaDonModel GetById(int maHD)
        {
            string query = @"
        SELECT hd.*, kh.HoTen AS TenKhachHang, p.SoPhong, dp.MaKH,
               (hd.TongTienThanhToan - ISNULL(hd.TienPhong, 0)) AS TienDichVu
        FROM HoaDon hd
        JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
        JOIN KhachHang kh ON dp.MaKH = kh.MaKH
        JOIN Phong p ON dp.MaPhong = p.MaPhong
        WHERE hd.MaHD = @MaHD";

            DataTable dt = _db.ExecuteQuery(query, new SqlParameter("@MaHD", maHD));

            if (dt.Rows.Count > 0)
            {
                // Tái sử dụng hàm MapRowToHoaDon nếu bạn đã viết, hoặc viết trực tiếp như sau:
                DataRow row = dt.Rows[0];
                return new HoaDonModel
                {
                    MaHD = Convert.ToInt32(row["MaHD"]),
                    MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    TienPhong = Convert.ToDecimal(row["TienPhong"]),
                    TongTienThanhToan = Convert.ToDecimal(row["TongTienThanhToan"]),
                    TrangThaiThanhToan = row["TrangThaiThanhToan"].ToString(),
                    PhuongThucThanhToan = row["PhuongThucThanhToan"]?.ToString(),
                    NgayThanhToan = row["NgayThanhToan"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgayThanhToan"]),
                    GhiChu = row["GhiChu"]?.ToString(),
                    NgayTao = Convert.ToDateTime(row["NgayTao"]),

                    // Các trường hiển thị mở rộng
                    TenKhachHang = row["TenKhachHang"].ToString(),
                    SoPhong = row["SoPhong"].ToString(),
                    TienDichVu = Convert.ToDecimal(row["TienDichVu"])
                };
            }
            return null;
        }

        // Tự động tính tiền khi nhập Mã Đặt Phòng
        public ThongTinThanhToanDTO GetThongTinTuDatPhong(int maDatPhong)
        {
            // Logic: Tiền phòng = Giá cơ bản * (Ngày đi - Ngày đến). Nếu ở trong ngày tính là 1 ngày.
            string query = @"
                SELECT 
                    dp.MaKH, kh.HoTen, p.SoPhong,
                    (p.GiaCoBan * CASE WHEN DATEDIFF(DAY, dp.NgayNhanPhong, dp.NgayTraPhong) = 0 THEN 1 ELSE DATEDIFF(DAY, dp.NgayNhanPhong, dp.NgayTraPhong) END) AS TienPhong,
                    ISNULL((SELECT SUM(SoLuong * DonGia) FROM DatPhong_DichVu WHERE MaDatPhong = dp.MaDatPhong), 0) AS TienDichVu
                FROM DatPhong dp
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                JOIN Phong p ON dp.MaPhong = p.MaPhong
                WHERE dp.MaDatPhong = @MaDP AND dp.TrangThai != N'Đã hủy'";

            DataTable dt = _db.ExecuteQuery(query, new SqlParameter("@MaDP", maDatPhong));

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                var dto = new ThongTinThanhToanDTO
                {
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    TenKhachHang = row["HoTen"].ToString(),
                    SoPhong = row["SoPhong"].ToString(),
                    TienPhong = Convert.ToDecimal(row["TienPhong"]),
                    TienDichVu = Convert.ToDecimal(row["TienDichVu"])
                };
                dto.TongTien = dto.TienPhong + dto.TienDichVu;
                return dto;
            }
            return null;
        }

        // Lấy danh sách dịch vụ chi tiết cho Grid bên phải
        public DataTable GetDichVuByDatPhong(int maDatPhong)
        {
            string query = @"
                SELECT dv.TenDichVu, dpdv.SoLuong, dpdv.DonGia, (dpdv.SoLuong * dpdv.DonGia) AS ThanhTien, dpdv.NgaySuDung
                FROM DatPhong_DichVu dpdv
                JOIN DichVu dv ON dpdv.MaDV = dv.MaDV
                WHERE dpdv.MaDatPhong = @MaDP";
            return _db.ExecuteQuery(query, new SqlParameter("@MaDP", maDatPhong));
        }

        // Thêm mới
        public bool Add(HoaDonModel hd)
        {
            string query = @"INSERT INTO HoaDon (MaDatPhong, TienPhong, TongTienThanhToan, TrangThaiThanhToan, PhuongThucThanhToan, NgayThanhToan, GhiChu)
                             VALUES (@MaDP, @TienPhong, @TongTien, @TrangThai, @PhuongThuc, @NgayTT, @GhiChu);
                             SELECT SCOPE_IDENTITY();";
            SqlParameter[] param = {
                new SqlParameter("@MaDP", hd.MaDatPhong),
                new SqlParameter("@TienPhong", hd.TienPhong),
                new SqlParameter("@TongTien", hd.TongTienThanhToan),
                new SqlParameter("@TrangThai", hd.TrangThaiThanhToan),
                new SqlParameter("@PhuongThuc", hd.PhuongThucThanhToan),
                new SqlParameter("@NgayTT", hd.NgayThanhToan ?? (object)DBNull.Value),
                new SqlParameter("@GhiChu", hd.GhiChu ?? (object)DBNull.Value)
            };

            var result = _db.ExecuteQuery(query, param);
            bool success = result.Rows.Count > 0;

            // Nếu thêm thành công và trạng thái là "Đã thanh toán", cập nhật trạng thái phòng
            if (success && hd.TrangThaiThanhToan == "Đã thanh toán")
            {
                // Lấy mã phòng từ đặt phòng
                string getPhongQuery = @"
                    SELECT dp.MaPhong
                    FROM DatPhong dp
                    WHERE dp.MaDatPhong = @MaDatPhong";

                DataTable phongDt = _db.ExecuteQuery(getPhongQuery, new SqlParameter("@MaDatPhong", hd.MaDatPhong));
                if (phongDt.Rows.Count > 0)
                {
                    int maPhong = Convert.ToInt32(phongDt.Rows[0]["MaPhong"]);

                    // Cập nhật trạng thái phòng thành "Trống"
                    string updatePhongQuery = "UPDATE Phong SET TrangThai = N'Trống' WHERE MaPhong = @MaPhong";
                    _db.ExecuteNonQuery(updatePhongQuery, new SqlParameter("@MaPhong", maPhong));
                }
            }

            return success;
        }

        // Cập nhật
        public bool Update(HoaDonModel hd)
        {
            // Kiểm tra trạng thái thanh toán hiện tại
            string checkQuery = "SELECT TrangThaiThanhToan FROM HoaDon WHERE MaHD = @MaHD";
            DataTable dt = _db.ExecuteQuery(checkQuery, new SqlParameter("@MaHD", hd.MaHD));
            string trangThaiCu = "";
            if (dt.Rows.Count > 0)
            {
                trangThaiCu = dt.Rows[0]["TrangThaiThanhToan"].ToString();
            }

            // Cập nhật hóa đơn
            string query = @"UPDATE HoaDon SET TrangThaiThanhToan=@TrangThai, PhuongThucThanhToan=@PhuongThuc,
                             NgayThanhToan=@NgayTT, GhiChu=@GhiChu
                             WHERE MaHD=@MaHD";
            SqlParameter[] param = {
                new SqlParameter("@MaHD", hd.MaHD),
                new SqlParameter("@TrangThai", hd.TrangThaiThanhToan),
                new SqlParameter("@PhuongThuc", hd.PhuongThucThanhToan),
                new SqlParameter("@NgayTT", hd.NgayThanhToan ?? (object)DBNull.Value),
                new SqlParameter("@GhiChu", hd.GhiChu ?? (object)DBNull.Value)
            };
            bool result = _db.ExecuteNonQuery(query, param) > 0;

            // Nếu cập nhật thành công và trạng thái thay đổi thành "Đã thanh toán", cập nhật trạng thái phòng
            if (result && trangThaiCu != "Đã thanh toán" && hd.TrangThaiThanhToan == "Đã thanh toán")
            {
                // Lấy mã phòng từ đặt phòng
                string getPhongQuery = @"
                    SELECT dp.MaPhong
                    FROM HoaDon hd
                    JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
                    WHERE hd.MaHD = @MaHD";

                DataTable phongDt = _db.ExecuteQuery(getPhongQuery, new SqlParameter("@MaHD", hd.MaHD));
                if (phongDt.Rows.Count > 0)
                {
                    int maPhong = Convert.ToInt32(phongDt.Rows[0]["MaPhong"]);

                    // Cập nhật trạng thái phòng thành "Trống"
                    string updatePhongQuery = "UPDATE Phong SET TrangThai = N'Trống' WHERE MaPhong = @MaPhong";
                    _db.ExecuteNonQuery(updatePhongQuery, new SqlParameter("@MaPhong", maPhong));
                }
            }

            return result;
        }

        // Xóa
        public bool Delete(int maHD)
        {
            return _db.ExecuteNonQuery("DELETE FROM HoaDon WHERE MaHD=@MaHD", new SqlParameter("@MaHD", maHD)) > 0;
        }

        // Thêm vào class HoaDonRepository
        public DataTable GetDanhSachDatPhongChoComboBox()
        {
            // Lấy MaDatPhong, Tên Khách và Số Phòng để hiển thị dạng: "101 - Nguyen Van A - P.201"
            string query = @"
        SELECT 
            dp.MaDatPhong,
            (CAST(dp.MaDatPhong AS NVARCHAR) + ' - ' + kh.HoTen + ' - ' + p.SoPhong) AS HienThi
        FROM DatPhong dp
        JOIN KhachHang kh ON dp.MaKH = kh.MaKH
        JOIN Phong p ON dp.MaPhong = p.MaPhong
        WHERE dp.TrangThai != N'Đã hủy' -- Chỉ lấy phòng chưa hủy
        ORDER BY dp.MaDatPhong DESC";

            return _db.ExecuteQuery(query);
        }

        public decimal TinhTongTienTheoMaDatPhong(int maDatPhong)
        {
            // Tổng tiền dịch vụ
            string queryDV = @"SELECT ISNULL(SUM(DonGia * SoLuong), 0)
                               FROM DatPhong_DichVu
                               WHERE MaDatPhong = @MaDatPhong";
            var param = new SqlParameter("@MaDatPhong", maDatPhong);
            var resultDV = _db.ExecuteQuery(queryDV, new[] { param });
            decimal tongDichVu = resultDV.Rows.Count > 0 ? Convert.ToDecimal(resultDV.Rows[0][0]) : 0;

            // Lấy tiền phòng từ DatPhong + Phong (GiaCoBan đã được tích hợp vào bảng Phong)
            string queryPhong = @"
                SELECT p.GiaCoBan
                FROM DatPhong dp
                JOIN Phong p ON dp.MaPhong = p.MaPhong
                WHERE dp.MaDatPhong = @MaDatPhong";
            var resultPhong = _db.ExecuteQuery(queryPhong, new[] { param });
            decimal tienPhong = resultPhong.Rows.Count > 0 ? Convert.ToDecimal(resultPhong.Rows[0][0]) : 0;

            return tongDichVu + tienPhong;
        }
    }
}
