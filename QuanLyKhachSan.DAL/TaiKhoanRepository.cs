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
    }
}
