//using QuanLyKhachSan.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;

//namespace QuanLyKhachSan.DAL
//{
//    public class TaiKhoanRepository
//    {
//        private readonly ConnectDB connDb = new ConnectDB();

//        // ✅ Lấy tất cả tài khoản
//        public List<TaiKhoanModel> GetAllTaiKhoan()
//        {
//            List<TaiKhoanModel> list = new List<TaiKhoanModel>();
//            string query = "SELECT * FROM TaiKhoan";

//            var dataTable = connDb.ExecuteQuery(query);

//            foreach (System.Data.DataRow row in dataTable.Rows)
//            {
//                TaiKhoanModel tk = new TaiKhoanModel
//                {
//                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
//                    TenDangNhap = row["TenDangNhap"].ToString(),
//                    MatKhau = row["MatKhau"].ToString(),
//                    MaNV = Convert.ToInt32(row["MaNV"]),
//                    VaiTro = row["VaiTro"].ToString(),
//                    TrangThai = row["TrangThai"].ToString()
//                };
//                list.Add(tk);
//            }
//            return list;
//        }

//        // ✅ Lấy danh sách tài khoản quản trị
//        public List<TaiKhoanModel> GetTaiKhoanAdmin()
//        {
//            List<TaiKhoanModel> list = new List<TaiKhoanModel>();
//            string query = "SELECT * FROM TaiKhoan WHERE VaiTro = N'Quản trị' AND TrangThai = N'Hoạt động'";

//            var dataTable = connDb.ExecuteQuery(query);

//            foreach (System.Data.DataRow row in dataTable.Rows)
//            {
//                TaiKhoanModel tk = new TaiKhoanModel
//                {
//                    TenDangNhap = row["TenDangNhap"].ToString(),
//                    MatKhau = row["MatKhau"].ToString(),
//                    VaiTro = row["VaiTro"].ToString(),
//                    TrangThai = row["TrangThai"].ToString()
//                };
//                list.Add(tk);
//            }
//            return list;
//        }

//        // ✅ Thêm tài khoản
//        public bool ThemTaiKhoan(TaiKhoanModel tk)
//        {
//            string sql = @"INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNV, VaiTro, TrangThai) 
//                           VALUES (@TenDangNhap, @MatKhau, @MaNV, @VaiTro, @TrangThai)";
//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@TenDangNhap", tk.TenDangNhap),
//                new SqlParameter("@MatKhau", tk.MatKhau),
//                new SqlParameter("@MaNV", tk.MaNV),
//                new SqlParameter("@VaiTro", tk.VaiTro),
//                new SqlParameter("@TrangThai", tk.TrangThai)
//            };
//            return connDb.ExecuteNonQuery(sql, parameters) > 0;
//        }

//        // ✅ Sửa tài khoản
//        public bool SuaTaiKhoan(TaiKhoanModel tk)
//        {
//            string sql = @"UPDATE TaiKhoan 
//                           SET MatKhau = @MatKhau, VaiTro = @VaiTro, TrangThai = @TrangThai 
//                           WHERE TenDangNhap = @TenDangNhap";
//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@TenDangNhap", tk.TenDangNhap),
//                new SqlParameter("@MatKhau", tk.MatKhau),
//                new SqlParameter("@VaiTro", tk.VaiTro),
//                new SqlParameter("@TrangThai", tk.TrangThai)
//            };
//            return connDb.ExecuteNonQuery(sql, parameters) > 0;
//        }

//        // ✅ Xóa tài khoản
//        public bool XoaTaiKhoan(string tenDangNhap)
//        {
//            string sql = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@TenDangNhap", tenDangNhap)
//            };
//            return connDb.ExecuteNonQuery(sql, parameters) > 0;
//        }

//        // ✅ Tìm kiếm tài khoản
//        public List<TaiKhoanModel> TimKiemTaiKhoan(string keyword)
//        {
//            List<TaiKhoanModel> list = new List<TaiKhoanModel>();

//            string sql = @"SELECT * FROM TaiKhoan 
//                           WHERE TenDangNhap LIKE @Keyword 
//                              OR VaiTro LIKE @Keyword 
//                              OR TrangThai LIKE @Keyword";

//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@Keyword", $"%{keyword}%")
//            };

//            var dataTable = connDb.ExecuteQuery(sql, parameters);

//            foreach (System.Data.DataRow row in dataTable.Rows)
//            {
//                TaiKhoanModel tk = new TaiKhoanModel
//                {
//                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
//                    TenDangNhap = row["TenDangNhap"].ToString(),
//                    MatKhau = row["MatKhau"].ToString(),
//                    MaNV = Convert.ToInt32(row["MaNV"]),
//                    VaiTro = row["VaiTro"].ToString(),
//                    TrangThai = row["TrangThai"].ToString()
//                };
//                list.Add(tk);
//            }
//            return list;
//        }

//        // ✅ Kiểm tra tồn tại nhân viên
//        public bool KiemTraTonTaiMaNV(int maNV)
//        {
//            string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE MaNV = @MaNV";
//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@MaNV", maNV)
//            };
//            int count = Convert.ToInt32(connDb.ExecuteScalar(sql, parameters));
//            return count > 0;
//        }

//        // ✅ Đăng nhập
//        public TaiKhoanModel DangNhap(string tenDangNhap, string matKhau)
//        {
//            string sql = @"SELECT * FROM TaiKhoan 
//                           WHERE TenDangNhap = @TenDangNhap 
//                             AND MatKhau = @MatKhau 
//                             AND TrangThai = N'Hoạt động'";
//            var parameters = new SqlParameter[]
//            {
//                new SqlParameter("@TenDangNhap", tenDangNhap),
//                new SqlParameter("@MatKhau", matKhau)
//            };

//            var dt = connDb.ExecuteQuery(sql, parameters);
//            if (dt.Rows.Count > 0)
//            {
//                var row = dt.Rows[0];
//                return new TaiKhoanModel
//                {
//                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
//                    TenDangNhap = row["TenDangNhap"].ToString(),
//                    MatKhau = row["MatKhau"].ToString(),
//                    MaNV = Convert.ToInt32(row["MaNV"]),
//                    VaiTro = row["VaiTro"].ToString(),
//                    TrangThai = row["TrangThai"].ToString()
//                };
//            }
//            return null;
//        }

//        // ✅ Lấy tài khoản theo email
//        public TaiKhoanModel GetTaiKhoanByEmail(string email)
//        {
//            string sql = "SELECT * FROM TaiKhoan WHERE Email = @Email AND TrangThai = N'Hoạt động'";
//            var parameters = new SqlParameter[]
//            {
//        new SqlParameter("@Email", email)
//            };

//            var dt = connDb.ExecuteQuery(sql, parameters);
//            if (dt.Rows.Count > 0)
//            {
//                var row = dt.Rows[0];
//                return new TaiKhoanModel
//                {
//                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
//                    TenDangNhap = row["TenDangNhap"].ToString(),
//                    MatKhau = row["MatKhau"].ToString(),
//                    MaNV = Convert.ToInt32(row["MaNV"]),
//                    VaiTro = row["VaiTro"].ToString(),
//                    TrangThai = row["TrangThai"].ToString(),
//                    Email = row["Email"].ToString() // đảm bảo TaiKhoanModel có trường Email
//                };
//            }
//            return null;
//        }

//    }
//}
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyKhachSan.DAL
{
    public class TaiKhoanRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        // ✅ Lấy tất cả tài khoản (kèm email nhân viên)
        public List<TaiKhoanModel> GetAllTaiKhoan()
        {
            List<TaiKhoanModel> list = new List<TaiKhoanModel>();
            string query = @"
                SELECT tk.MaNguoiDung, tk.TenDangNhap, tk.MatKhau, tk.MaNV,
                       tk.VaiTro, tk.TrangThai, nv.Email
                FROM TaiKhoan tk
                JOIN NhanVien nv ON tk.MaNV = nv.MaNV";

            var dataTable = connDb.ExecuteQuery(query);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                TaiKhoanModel tk = new TaiKhoanModel
                {
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    VaiTro = row["VaiTro"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    Email = row["Email"].ToString()
                };
                list.Add(tk);
            }
            return list;
        }

        // ✅ Lấy danh sách tài khoản quản trị
        public List<TaiKhoanModel> GetTaiKhoanAdmin()
        {
            List<TaiKhoanModel> list = new List<TaiKhoanModel>();
            string query = @"
                SELECT tk.MaNguoiDung, tk.TenDangNhap, tk.MatKhau, tk.MaNV,
                       tk.VaiTro, tk.TrangThai, nv.Email
                FROM TaiKhoan tk
                JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE tk.VaiTro = N'Quản trị' AND tk.TrangThai = N'Hoạt động'";

            var dataTable = connDb.ExecuteQuery(query);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                TaiKhoanModel tk = new TaiKhoanModel
                {
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    VaiTro = row["VaiTro"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    Email = row["Email"].ToString()
                };
                list.Add(tk);
            }
            return list;
        }

        // ✅ Thêm tài khoản
        public bool ThemTaiKhoan(TaiKhoanModel tk)
        {
            string sql = @"INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaNV, VaiTro, TrangThai) 
                           VALUES (@TenDangNhap, @MatKhau, @MaNV, @VaiTro, @TrangThai)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", tk.TenDangNhap),
                new SqlParameter("@MatKhau", tk.MatKhau),
                new SqlParameter("@MaNV", tk.MaNV),
                new SqlParameter("@VaiTro", tk.VaiTro),
                new SqlParameter("@TrangThai", tk.TrangThai)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // ✅ Sửa tài khoản
        public bool SuaTaiKhoan(TaiKhoanModel tk)
        {
            string sql = @"UPDATE TaiKhoan 
                           SET MatKhau = @MatKhau, VaiTro = @VaiTro, TrangThai = @TrangThai 
                           WHERE TenDangNhap = @TenDangNhap";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", tk.TenDangNhap),
                new SqlParameter("@MatKhau", tk.MatKhau),
                new SqlParameter("@VaiTro", tk.VaiTro),
                new SqlParameter("@TrangThai", tk.TrangThai)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // ✅ Xóa tài khoản
        public bool XoaTaiKhoan(string tenDangNhap)
        {
            string sql = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", tenDangNhap)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // ✅ Tìm kiếm tài khoản (kèm email nhân viên)
        public List<TaiKhoanModel> TimKiemTaiKhoan(string keyword)
        {
            List<TaiKhoanModel> list = new List<TaiKhoanModel>();
            string sql = @"
                SELECT tk.MaNguoiDung, tk.TenDangNhap, tk.MatKhau, tk.MaNV,
                       tk.VaiTro, tk.TrangThai, nv.Email
                FROM TaiKhoan tk
                JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE tk.TenDangNhap LIKE @Keyword 
                   OR tk.VaiTro LIKE @Keyword 
                   OR tk.TrangThai LIKE @Keyword 
                   OR nv.Email LIKE @Keyword";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", $"%{keyword}%")
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                TaiKhoanModel tk = new TaiKhoanModel
                {
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    VaiTro = row["VaiTro"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    Email = row["Email"].ToString()
                };
                list.Add(tk);
            }
            return list;
        }

        // ✅ Kiểm tra tồn tại nhân viên
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

        // ✅ Đăng nhập (kèm email)
        public TaiKhoanModel DangNhap(string tenDangNhap, string matKhau)
        {
            string sql = @"
                SELECT tk.MaNguoiDung, tk.TenDangNhap, tk.MatKhau, tk.MaNV,
                       tk.VaiTro, tk.TrangThai, nv.Email
                FROM TaiKhoan tk
                JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE tk.TenDangNhap = @TenDangNhap 
                  AND tk.MatKhau = @MatKhau 
                  AND tk.TrangThai = N'Hoạt động'";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", tenDangNhap),
                new SqlParameter("@MatKhau", matKhau)
            };

            var dt = connDb.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                return new TaiKhoanModel
                {
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    VaiTro = row["VaiTro"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    Email = row["Email"].ToString()
                };
            }
            return null;
        }

        // ✅ Lấy tài khoản theo email nhân viên
        public TaiKhoanModel GetTaiKhoanByEmail(string email)
        {
            string sql = @"
                SELECT tk.MaNguoiDung, tk.TenDangNhap, tk.MatKhau, tk.MaNV,
                       tk.VaiTro, tk.TrangThai, nv.Email
                FROM TaiKhoan tk
                JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE nv.Email = @Email AND tk.TrangThai = N'Hoạt động'";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };

            var dt = connDb.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                return new TaiKhoanModel
                {
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    MaNV = Convert.ToInt32(row["MaNV"]),
                    VaiTro = row["VaiTro"].ToString(),
                    TrangThai = row["TrangThai"].ToString(),
                    Email = row["Email"].ToString()
                };
            }
            return null;
        }
    }
}
