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

            var dataTable = connDb.ExecuteQuery(sql);
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                NhanVienModel nhanVien = new NhanVienModel
                {
                    MaNV = (int)row["MaNV"],
                    HoTen = row["HoTen"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    NgaySinh = (DateTime)row["NgaySinh"],
                    ChucVu = row["ChucVu"].ToString(),
                    SoDienThoai = row["SDT"].ToString(),
                    Email = row["Email"].ToString(),
                    //Anh = row["Anh"] as byte[] // Lấy  ảnh dưới dạng  byte
                };
                listNhanVien.Add(nhanVien);
            }
            return listNhanVien;
        }

        public bool ThemNhanVien(NhanVienModel nv)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = @"INSERT INTO NhanVien 
            (HoTen, GioiTinh, NgaySinh, ChucVu, SDT, Email, Anh) 
            VALUES (@HoTen, @GioiTinh, @NgaySinh, @ChucVu, @SDT, @Email, @Anh)";

                var parameters = new SqlParameter[]
                {
            new SqlParameter("@HoTen", nv.HoTen),
            new SqlParameter("@GioiTinh", nv.GioiTinh),
            new SqlParameter("@NgaySinh", nv.NgaySinh),
            new SqlParameter("@ChucVu", nv.ChucVu),
            new SqlParameter("@SDT", nv.SoDienThoai),
            new SqlParameter("@Email", nv.Email),
            // Kiểm tra nếu nv.Anh là null thì truyền DBNull.Value, nếu có ảnh thì truyền mảng byte
            new SqlParameter("@Anh", System.Data.SqlDbType.VarBinary) { Value = nv.Anh ?? (object)DBNull.Value }
                };

                return connDb.ExecuteNonQuery(sql, parameters) > 0;
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
                        Email = @Email,
                        Anh = @Anh
                    WHERE Id = @Id";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@HoTen", nv.HoTen),
                    new SqlParameter("@GioiTinh", nv.GioiTinh),
                    new SqlParameter("@NgaySinh", nv.NgaySinh),
                    new SqlParameter("@ChucVu", nv.ChucVu),
                    new SqlParameter("@SDT", nv.SoDienThoai),
                    new SqlParameter("@Email", nv.Email),
                    new SqlParameter("@Anh", System.Data.SqlDbType.VarBinary) { Value = nv.Anh },
                    new SqlParameter("@MaNV", nv.MaNV)
                };

                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
        }

        public bool XoaNhanVien(int maNv)
        {
            string sql = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
            
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", maNv)
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public List<NhanVienModel> TimNhanVien(string hoTen = "", string gioiTinh = "", 
            DateTime? ngaySinh = null, string soDienThoai = "", string email = "",
            string chucVu = "")
        {
            List<NhanVienModel> listNhanVien = new List<NhanVienModel>();

            string sql = @"SELECT * FROM NhanVien
                WHERE (@HoTen = '' OR HoTen LIKE '%' + @HoTen + '%')
                  AND (@GioiTinh = '' OR GioiTinh = @GioiTinh)
                  AND (@NgaySinh IS NULL OR NgaySinh = @NgaySinh)
                  AND (@ChucVu = '' OR ChucVu LIKE '%' + @ChucVu + '%')
                  AND (@SDT = '' OR SDT LIKE '%' + @SDT + '%')
                  AND (@Email = '' OR Email LIKE '%' + @Email + '%')";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@GioiTinh", gioiTinh),
                new SqlParameter("@NgaySinh", (object)ngaySinh ?? DBNull.Value),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@SDT", soDienThoai),
                new SqlParameter("@Email", email)
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                NhanVienModel nhanVien = new NhanVienModel
                {
                    MaNV = (int)row["MaNV"],
                    HoTen = row["HoTen"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    NgaySinh = (DateTime)row["NgaySinh"],
                    ChucVu = row["ChucVu"].ToString(),
                    SoDienThoai = row["SDT"].ToString(),
                    Email = row["Email"].ToString(),
                    Anh = row["Anh"] as byte[] // Lấy  ảnh dưới dạng  byte
                };
                listNhanVien.Add(nhanVien);
            }
            return listNhanVien;
        }

        public bool CapNhatAnh(int maNV, string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            string sql = "UPDATE NhanVien SET Anh = @Anh WHERE MaNV = @MaNV";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Anh", System.Data.SqlDbType.VarBinary) { Value = imageBytes },
                new SqlParameter("@MaNV", maNV)
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public byte[] LayAnhNhanVien(int maNV)
        {
            string sql = "SELECT Anh FROM NhanVien WHERE MaNV = @MaNV";
            SqlParameter[] parameters = { new SqlParameter("@MaNV", maNV) };

            try
            {
                var result = connDb.ExecuteScalar(sql, parameters);
                if (result != null && result != DBNull.Value)
                {
                    return (byte[])result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi lấy ảnh (repo): " + ex.Message);
            }

            return null;
        }

    }
}
