using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAL
{
    public  class TaiKhoanRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<TaiKhoanModel> getAllTaiKhoan()
        {
            List<TaiKhoanModel> list = new List<TaiKhoanModel>();
            string query = "SELECT * FROM TaiKhoan";

            var dataTable = connDb.ExecuteQuery(query);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                TaiKhoanModel tk = new TaiKhoanModel
                {
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    MaNV = Convert.ToInt32(row["MaNV"]),  // Chuyển đổi sang kiểu int
                    Quyen = row["Quyen"].ToString(),
                    TrangThai = Convert.ToBoolean(row["TrangThai"]) // Chuyển đổi sang kiểu bool
                };
                list.Add(tk);
            }
            return list;
        }

        public List<TaiKhoanModel> getTaiKhoanLogin()
        {
            List<TaiKhoanModel> list = new List<TaiKhoanModel>();
            string query = "SELECT * FROM TaiKhoan WHERE Quyen = 'Admin' AND TrangThai = 1";
            
            var dataTable = connDb.ExecuteQuery(query);

            foreach(System.Data.DataRow row in dataTable.Rows)
            {
                TaiKhoanModel tk = new TaiKhoanModel
                {
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    //MaNV = row["MaNV"].ToString(),
                    //Quyen = row["Quyen"].ToString(),
                    //TrangThai = row["TrangThai"].ToString()
                };
                list.Add(tk);
            }
            return list;
        }

        public bool ThemTaiKhoan (TaiKhoanModel tk)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = @"INSERT INTO TaiKhoan 
                        (TenDangNhap, MatKhau, MaNV, Quyen, TrangThai) 
                        VALUES (@TenDangNhap, @MatKhau, @MaNV, @Quyen, @TrangThai)";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", tk.TenDangNhap),
                    new SqlParameter("@MatKhau", tk.MatKhau),
                    new SqlParameter("@MaNV", tk.MaNV),
                    new SqlParameter("@Quyen", tk.Quyen),
                    new SqlParameter("@TrangThai", tk.TrangThai)
                };
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
        }

        public bool SuaTaiKhoan(TaiKhoanModel tk)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = @"UPDATE TaiKhoan 
                        SET MatKhau = @MatKhau, Quyen = @Quyen, TrangThai = @TrangThai 
                        WHERE TenDangNhap = @TenDangNhap";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", tk.TenDangNhap),
                    new SqlParameter("@MatKhau", tk.MatKhau),
                    new SqlParameter("@Quyen", tk.Quyen),
                    new SqlParameter("@TrangThai", tk.TrangThai)
                };
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
        }

        public bool XoaTaiKhoan(string tenDangNhap)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", tenDangNhap)
                };
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
        }

        public List<TaiKhoanModel> TimKiemTaiKhoan(string keyword)
        {
            List<TaiKhoanModel> list = new List<TaiKhoanModel>();

            int? trangThai = null;
            string keywordLower = keyword.ToLower();

            if (keywordLower.Contains("kích hoạt"))
                trangThai = 1;
            else if (keywordLower.Contains("chưa kích hoạt"))
                trangThai = 0;

            string sql = @"SELECT * FROM TaiKhoan
                WHERE ((@TrangThai IS NULL AND 
                (TenDangNhap = @Keyword
                OR MatKhau = @Keyword
                OR CAST(MaNV AS NVARCHAR) = @Keyword
                OR Quyen = @Keyword))
                OR (@TrangThai IS NOT NULL AND TrangThai = @TrangThai))";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Keyword", keyword),
                new SqlParameter("@TrangThai", (object)trangThai ?? DBNull.Value)
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters.ToArray());

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                TaiKhoanModel tk = new TaiKhoanModel
                {
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    Quyen = row["Quyen"].ToString(),
                    TrangThai = Convert.ToBoolean(row["TrangThai"])
                };
                list.Add(tk);
            }
            return list;
        }

        public bool KiemTraTonTaiMaNV(int maNV)
        {
            string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE MaNV = @MaNV";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNV)
            };
            int count = Convert.ToInt32(connDb.ExecuteScalar(sql, parameters));
            return count > 0;
        }
    }
}
