using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAL
{
    public class NhanVienRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<NhanVienModel> getAllNhanVien()
        {
            List<NhanVienModel> listNhanVien = new List<NhanVienModel>();
            string sql = "SELECT * FROM NhanVien";
            using (SqlConnection conn = connDb.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NhanVienModel nhanVien = new NhanVienModel
                    {
                        MaNV = (int)reader["MaNV"],
                        HoTen = reader["HoTen"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        NgaySinh = (DateTime)reader["NgaySinh"],
                        ChucVu = reader["ChucVu"].ToString(),
                        SoDienThoai = reader["SDT"].ToString(),
                        Email = reader["Email"].ToString()
                    };
                    listNhanVien.Add(nhanVien);
                }
            }
            return listNhanVien;

        }

        public bool ThemNhanVien(NhanVienModel nv)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = @"INSERT INTO NhanVien 
                    (HoTen, GioiTinh, NgaySinh, ChucVu, SDT, Email) 
                    VALUES (@HoTen, @GioiTinh, @NgaySinh, @ChucVu, @SDT, @Email)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu);
                cmd.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", nv.Email);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaNhanVien(NhanVienModel nv)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = @"UPDATE NhanVien SET HoTen = @HoTen, 
                        GioiTinh = @GioiTinh, 
                        NgaySinh = @NgaySinh, 
                        ChucVu = @ChucVu, 
                        SDT = @SDT, 
                        Email = @Email 
                    WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
                cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu);
                cmd.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@Email", nv.Email);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaNhanVien(int id)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNV", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<NhanVienModel> TimNhanVien(string hoTen = "", string gioiTinh = "", 
            DateTime? ngaySinh = null, string soDienThoai = "", string email = "",
            string chucVu = "")
        {
            List<NhanVienModel> listNhanVien = new List<NhanVienModel>();

            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                conn.Open();

                string sql = @"SELECT * FROM NhanVien
                WHERE (@HoTen = '' OR HoTen LIKE '%' + @HoTen + '%')
                  AND (@GioiTinh = '' OR GioiTinh = @GioiTinh)
                  AND (@NgaySinh IS NULL OR NgaySinh = @NgaySinh)
                  AND (@ChucVu = '' OR ChucVu LIKE '%' + @ChucVu + '%')
                  AND (@SDT = '' OR SDT LIKE '%' + @SDT + '%')
                  AND (@Email = '' OR Email LIKE '%' + @Email + '%')";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@NgaySinh", (object)ngaySinh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ChucVu", chucVu);
                    cmd.Parameters.AddWithValue("@SDT", soDienThoai);
                    cmd.Parameters.AddWithValue("@Email", email);
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listNhanVien.Add( new NhanVienModel
                            {
                                MaNV = (int)reader["MaNV"],
                                HoTen = reader["HoTen"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                NgaySinh = (DateTime)reader["NgaySinh"],
                                ChucVu = reader["ChucVu"].ToString(),
                                SoDienThoai = reader["SDT"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }
            return listNhanVien;
        }
    }
}
