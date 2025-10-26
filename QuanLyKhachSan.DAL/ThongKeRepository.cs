using System;
using System.Collections.Generic;
using System.Data;
using QuanLyKhachSan.DAL;

namespace QuanLyKhachSan.DAL
{
    public class ThongKeRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        // Tổng doanh thu
        public decimal GetTongDoanhThu()
        {
            string sql = "SELECT SUM(TongTienThanhToan) FROM HoaDon";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToDecimal(result) : 0;
        }

        // Số khách hàng
        public int GetSoKhach()
        {
            string sql = "SELECT COUNT(*) FROM KhachHang";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
        }

        // Số phòng
        public int GetSoPhong()
        {
            string sql = "SELECT COUNT(*) FROM Phong";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
        }

        // Số nhân viên
        public int GetSoNhanVien()
        {
            string sql = "SELECT COUNT(*) FROM NhanVien";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value && result != null ? Convert.ToInt32(result) : 0;
        }

        // Doanh thu theo ngày
        public Dictionary<DateTime, decimal> GetDoanhThuTheoNgay()
        {
            string sql = @"
                SELECT CONVERT(DATE, NgayTao) AS Ngay, SUM(TongTienThanhToan) AS DoanhThu
                FROM HoaDon
                GROUP BY CONVERT(DATE, NgayTao)
                ORDER BY Ngay";

            var resultDict = new Dictionary<DateTime, decimal>();
            DataTable table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                decimal doanhThu = row["DoanhThu"] != DBNull.Value ? Convert.ToDecimal(row["DoanhThu"]) : 0;
                resultDict[ngay] = doanhThu;
            }

            return resultDict;
        }

        // Doanh thu theo tháng
        public Dictionary<int, decimal> GetDoanhThuTheoThang()
        {
            string sql = @"
                SELECT MONTH(NgayTao) AS Thang, SUM(TongTienThanhToan) AS DoanhThu
                FROM HoaDon
                GROUP BY MONTH(NgayTao)
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

        // Doanh thu theo năm
        public Dictionary<int, decimal> GetDoanhThuTheoNam()
        {
            string sql = @"
                SELECT YEAR(NgayTao) AS Nam, SUM(TongTienThanhToan) AS DoanhThu
                FROM HoaDon
                GROUP BY YEAR(NgayTao)
                ORDER BY Nam";

            var resultDict = new Dictionary<int, decimal>();
            DataTable table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                int nam = Convert.ToInt32(row["Nam"]);
                decimal doanhThu = row["DoanhThu"] != DBNull.Value ? Convert.ToDecimal(row["DoanhThu"]) : 0;
                resultDict[nam] = doanhThu;
            }

            return resultDict;
        }
    }
}
