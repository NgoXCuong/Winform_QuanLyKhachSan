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
                    MaNV = row["MaNV"].ToString(),
                    Quyen = row["Quyen"].ToString(),
                    TrangThai = row["TrangThai"].ToString()
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
                        WHERE MaNV = @MaNV";
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

        public bool XoaTaiKhoan(int maNhanVien)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = "DELETE FROM TaiKhoan WHERE MaNV = @MaNV";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", maNhanVien)
                };
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
        }
    }
}
