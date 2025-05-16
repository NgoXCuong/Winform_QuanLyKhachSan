using System;
using System.Collections.Generic;
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

    }
}
