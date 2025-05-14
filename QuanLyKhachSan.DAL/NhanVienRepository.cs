using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            foreach (DataRow row in dataTable.Rows)
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

        public List<NhanVienModel> GetNhanVienByIdName()
        {
            List<NhanVienModel> ds = new List<NhanVienModel>();
            string sql = "SELECT MaNV, HoTen FROM NhanVien";

            // Giả sử ExecuteQuery thực thi truy vấn và trả về DataTable
            var table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                ds.Add(new NhanVienModel
                {
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"].ToString()
                });
            }
            return ds;
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
                        Email = @Email
                        WHERE MaNV = @MaNV";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@HoTen", nv.HoTen),
                    new SqlParameter("@GioiTinh", nv.GioiTinh),
                    new SqlParameter("@NgaySinh", nv.NgaySinh),
                    new SqlParameter("@ChucVu", nv.ChucVu),
                    new SqlParameter("@SDT", nv.SoDienThoai),
                    new SqlParameter("@Email", nv.Email),
                    //new SqlParameter("@Anh", (object)nv.Anh ?? DBNull.Value),
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

        public List<NhanVienModel> TimNhanVien(string keyword)
        {
            List<NhanVienModel> listNhanVien = new List<NhanVienModel>();

            string sql = @"SELECT * FROM NhanVien
                WHERE (@Keyword = '' OR HoTen LIKE '%' + @Keyword + '%')
                OR (@Keyword = '' OR GioiTinh LIKE '%' + @Keyword + '%')
                OR (@Keyword = '' OR NgaySinh BETWEEN @NgaySinhStart AND @NgaySinhEnd)  -- Tìm trong khoảng thời gian
                OR (@Keyword = '' OR ChucVu LIKE '%' + @Keyword + '%')
                OR (@Keyword = '' OR SDT LIKE '%' + @Keyword + '%')
                OR (@Keyword = '' OR Email LIKE '%' + @Keyword + '%')";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", keyword),
                new SqlParameter("@NgaySinhStart", DateTime.TryParse(keyword, out DateTime startDate) ? (object)startDate : DBNull.Value),
                new SqlParameter("@NgaySinhEnd", DateTime.TryParse(keyword, out DateTime endDate) ? (object)endDate.AddDays(1) : DBNull.Value)  // Cộng thêm một ngày để tìm đến hết ngày kết thúc
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters);

            foreach (DataRow row in dataTable.Rows)
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
                    Anh = row["Anh"] as byte[] // Lấy ảnh dưới dạng byte
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
                new SqlParameter("@Anh", SqlDbType.VarBinary) { Value = imageBytes },
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

        public bool XoaAnhNhanVien(int maNV)
        {
            string sql = "UPDATE NhanVien SET Anh = NULL WHERE MaNV = @MaNV";
            SqlParameter[] parameters = { new SqlParameter("@MaNV", maNV) };

            try
            {
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi xóa ảnh: " + ex.Message);
                return false;
            }
        }

        // Lấy chức vụ trong bảng 
        public string GetChucVuByMaNV(int maNV)
        {
            string sql = "SELECT ChucVu FROM NhanVien WHERE MaNV = @MaNV";

            var parameters = new SqlParameter[]
            { 
                new SqlParameter("@MaNV", maNV)
            };

            var result = connDb.ExecuteScalar(sql, parameters);

            if (result != null)
            {
                return result.ToString();
            }
            return null; // Trả về null nếu không tìm thấy
        }
    }
}
