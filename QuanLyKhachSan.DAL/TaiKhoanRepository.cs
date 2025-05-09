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
            using (SqlConnection conn = connDb.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TaiKhoanModel tk = new TaiKhoanModel
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        Quyen = reader["Quyen"].ToString(),
                        TrangThai = reader["TrangThai"].ToString()
                    };
                    list.Add(tk);
                }
            }
            return list;
        }

        public List<TaiKhoanModel> getTaiKhoanLogin()
        {
            List<TaiKhoanModel> list = new List<TaiKhoanModel>();
            string query = "SELECT * FROM TaiKhoan WHERE Quyen = 'Admin' AND TrangThai = 1";
            using (SqlConnection conn = connDb.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TaiKhoanModel tk = new TaiKhoanModel
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),
                        //MaNV = reader["MaNV"].ToString(),
                        //Quyen = reader["Quyen"].ToString(),
                        //TrangThai = reader["TrangThai"].ToString()
                    };
                    list.Add(tk);
                }
            }
            return list;
        }
    }
}
