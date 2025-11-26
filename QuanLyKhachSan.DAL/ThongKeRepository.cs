using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Cần thêm nếu dùng SqlParameter (tùy chọn)

namespace QuanLyKhachSan.DAL
{
    public class ThongKeRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        // 1. Tổng doanh thu (Chỉ tính hóa đơn ĐÃ thanh toán)
        public decimal GetTongDoanhThu()
        {
            // Lọc theo TrangThaiThanhToan hoặc kiểm tra NgayThanhToan IS NOT NULL
            string sql = "SELECT SUM(TongTienThanhToan) FROM HoaDon WHERE TrangThaiThanhToan = N'Đã thanh toán'";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
        }

        // 2. Số lượng (Khách, Phòng, Nhân viên) - Giữ nguyên logic
        public int GetSoKhach()
        {
            string sql = "SELECT COUNT(*) FROM KhachHang";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
        }

        public int GetSoPhong()
        {
            string sql = "SELECT COUNT(*) FROM Phong";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
        }

        public int GetSoNhanVien()
        {
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE TrangThai = N'Đang làm'"; // Thêm điều kiện chỉ đếm nhân viên đang làm
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
        }

        // 3. Doanh thu theo NGÀY (Sử dụng NgayThanhToan)
        public Dictionary<DateTime, decimal> GetDoanhThuTheoNgay()
        {
            // Lấy 30 ngày gần nhất để biểu đồ không bị quá tải dữ liệu cũ
            string sql = @"
                SELECT CAST(NgayThanhToan AS DATE) AS Ngay, SUM(TongTienThanhToan) AS DoanhThu
                FROM HoaDon
                WHERE NgayThanhToan IS NOT NULL 
                  AND TrangThaiThanhToan = N'Đã thanh toán'
                  AND NgayThanhToan >= DATEADD(DAY, -30, GETDATE()) 
                GROUP BY CAST(NgayThanhToan AS DATE)
                ORDER BY Ngay";

            var resultDict = new Dictionary<DateTime, decimal>();
            DataTable table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                if (row["Ngay"] != DBNull.Value)
                {
                    DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                    decimal doanhThu = row["DoanhThu"] != DBNull.Value ? Convert.ToDecimal(row["DoanhThu"]) : 0;
                    resultDict[ngay] = doanhThu;
                }
            }
            return resultDict;
        }

        // 4. Doanh thu theo THÁNG (Trong năm hiện tại)
        public Dictionary<int, decimal> GetDoanhThuTheoThang()
        {
            string sql = @"
                SELECT MONTH(NgayThanhToan) AS Thang, SUM(TongTienThanhToan) AS DoanhThu
                FROM HoaDon
                WHERE NgayThanhToan IS NOT NULL 
                  AND TrangThaiThanhToan = N'Đã thanh toán'
                  AND YEAR(NgayThanhToan) = YEAR(GETDATE())
                GROUP BY MONTH(NgayThanhToan)
                ORDER BY Thang";

            var resultDict = new Dictionary<int, decimal>();
            DataTable table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                int thang = Convert.ToInt32(row["Thang"]);
                decimal doanhThu = row["DoanhThu"] != DBNull.Value ? Convert.ToDecimal(row["DoanhThu"]) : 0;
                resultDict[thang] = doanhThu;
            }
            return resultDict;
        }

        // 5. Doanh thu theo NĂM (5 năm gần nhất)
        public Dictionary<int, decimal> GetDoanhThuTheoNam()
        {
            // Thêm ORDER BY Nam ASC để sắp xếp năm tăng dần
            string sql = @"
        SELECT YEAR(NgayThanhToan) AS Nam, SUM(TongTienThanhToan) AS DoanhThu
        FROM HoaDon
        WHERE NgayThanhToan IS NOT NULL 
          AND TrangThaiThanhToan = N'Đã thanh toán'
        GROUP BY YEAR(NgayThanhToan)
        ORDER BY Nam ASC"; // <--- SỬA Ở ĐÂY (ASC = Tăng dần)

            var resultDict = new Dictionary<int, decimal>();
            DataTable table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                if (row["Nam"] != DBNull.Value)
                {
                    int nam = Convert.ToInt32(row["Nam"]);
                    decimal doanhThu = row["DoanhThu"] != DBNull.Value ? Convert.ToDecimal(row["DoanhThu"]) : 0;
                    resultDict[nam] = doanhThu;
                }
            }
            return resultDict;
        }

        // 6. Bổ sung: Hàm tính Tổng tiền phòng & Dịch vụ cho biểu đồ tròn (Pie Chart)
        // Trong class ThongKeRepository
        public decimal GetTongTienPhong()
        {
            // Dùng ISNULL(..., 0) để nếu không có hóa đơn nào thì trả về 0 thay vì null
            string sql = "SELECT SUM(ISNULL(TienPhong, 0)) FROM HoaDon WHERE TrangThaiThanhToan = N'Đã thanh toán'";

            var result = connDb.ExecuteScalar(sql);

            // Kiểm tra an toàn để tránh lỗi khi convert
            return result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
        }

        public decimal GetTongTienDichVu()
        {
            // Tiền dịch vụ = Tổng tiền - Tiền phòng
            string sql = "SELECT SUM(ISNULL(TongTienThanhToan, 0) - ISNULL(TienPhong, 0)) FROM HoaDon WHERE TrangThaiThanhToan = N'Đã thanh toán'";

            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
        }
    }
}