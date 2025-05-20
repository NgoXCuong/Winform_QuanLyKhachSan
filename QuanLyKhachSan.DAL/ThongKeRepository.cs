using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAL
{
    public class ThongKeRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public decimal GetTongDoanhThu()
        {
            string sql = "SELECT SUM(TongTien) FROM HoaDon";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public int GetSoKhach()
        {
            string sql = "SELECT COUNT(*) FROM KhachHang";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        public int GetSoPhong()
        {
            string sql = "SELECT COUNT(*) FROM Phong";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        public int GetSoNhanVien()
        {
            string sql = "SELECT COUNT(*) FROM NhanVien";
            var result = connDb.ExecuteScalar(sql);
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        public Dictionary<DateTime, decimal> GetDoanhThuTheoNgay()
        {
            string sql = @"
                SELECT CONVERT(DATE, NgayLap) AS Ngay, SUM(TongTien) AS DoanhThu
                FROM HoaDon
                GROUP BY CONVERT(DATE, NgayLap)
                ORDER BY Ngay";

            var doanhthutheoNgay = new Dictionary<DateTime, decimal>();
            DataTable table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                decimal doanhThu = Convert.ToDecimal(row["DoanhThu"]);
                doanhthutheoNgay[ngay] = doanhThu;
            }

            return doanhthutheoNgay;
        }

        
        public Dictionary<int, decimal> GetDoanhThuTheoThang()
        {
            string sql = @"
                SELECT MONTH(NgayLap) AS Thang, SUM(TongTien) AS DoanhThu
                FROM HoaDon
                GROUP BY MONTH(NgayLap)
                ORDER BY Thang";

            var doanhthutheoThang = new Dictionary<int, decimal>();
            DataTable table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                int thang = Convert.ToInt32(row["Thang"]);
                decimal doanhThu = Convert.ToDecimal(row["DoanhThu"]);
                doanhthutheoThang[thang] = doanhThu;
            }

            return doanhthutheoThang;
        }

        // ✅ 3. Doanh thu theo năm
        public Dictionary<int, decimal> GetDoanhThuTheoNam()
        {
            string sql = @"
                SELECT YEAR(NgayLap) AS Nam, SUM(TongTien) AS DoanhThu
                FROM HoaDon
                GROUP BY YEAR(NgayLap)
                ORDER BY Nam";

            var doanhthutheoNam = new Dictionary<int, decimal>();
            DataTable table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                int nam = Convert.ToInt32(row["Nam"]);
                decimal doanhThu = Convert.ToDecimal(row["DoanhThu"]);
                doanhthutheoNam[nam] = doanhThu;
            }

            return doanhthutheoNam;
        }
    }
}
