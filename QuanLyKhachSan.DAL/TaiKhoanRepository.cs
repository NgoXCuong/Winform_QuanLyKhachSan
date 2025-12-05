
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
                    Email = row["Email"].ToString() // Lấy từ JOIN với NhanVien
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
            // Lấy trạng thái hiện tại của tài khoản
            string getCurrentStatusSql = "SELECT TrangThai FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            var getParams = new SqlParameter[] { new SqlParameter("@TenDangNhap", tk.TenDangNhap) };
            var currentStatusResult = connDb.ExecuteQuery(getCurrentStatusSql, getParams);

            string currentStatus = "";
            if (currentStatusResult.Rows.Count > 0)
            {
                currentStatus = currentStatusResult.Rows[0]["TrangThai"].ToString();
            }

            // Cập nhật tài khoản
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
            bool result = connDb.ExecuteNonQuery(sql, parameters) > 0;

            // Nếu cập nhật thành công và trạng thái thay đổi thành "Ngưng hoạt động"
            if (result && currentStatus != "Ngưng hoạt động" && tk.TrangThai == "Ngưng hoạt động")
            {
                try
                {
                    // Cập nhật trạng thái nhân viên thành "Nghỉ việc"
                    string updateNhanVienSql = @"UPDATE NhanVien
                                               SET TrangThai = N'Nghỉ việc'
                                               WHERE MaNV = @MaNV";
                    var nhanVienParams = new SqlParameter[]
                    {
                        new SqlParameter("@MaNV", tk.MaNV)
                    };
                    int nhanVienUpdateResult = connDb.ExecuteNonQuery(updateNhanVienSql, nhanVienParams);

                    if (nhanVienUpdateResult == 0)
                    {
                        // Nếu không cập nhật được nhân viên, có thể do constraint
                        // Vẫn trả về true vì tài khoản đã được cập nhật thành công
                        // Log hoặc thông báo cho admin biết
                    }
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi khi cập nhật nhân viên (do constraint, trigger, etc.)
                    // Vẫn trả về true vì tài khoản đã được cập nhật
                    // Có thể log lỗi này để admin biết
                    Console.WriteLine($"Warning: Could not update employee status: {ex.Message}");
                }
            }

            return result;
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
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };

            // 1. Thử tìm trong bảng NhanVien (schema mới - khuyến nghị)
            string sqlNhanVien = @"
                SELECT tk.MaNguoiDung, tk.TenDangNhap, tk.MatKhau, tk.MaNV,
                       tk.VaiTro, tk.TrangThai, nv.Email
                FROM TaiKhoan tk
                JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                WHERE nv.Email = @Email AND tk.TrangThai = N'Hoạt động'";

            var dt = connDb.ExecuteQuery(sqlNhanVien, parameters);
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

            // 2. Thử tìm trong bảng TaiKhoan (schema cũ - tương thích ngược)
            try
            {
                string sqlTaiKhoan = @"
                    SELECT tk.MaNguoiDung, tk.TenDangNhap, tk.MatKhau, tk.MaNV,
                           tk.VaiTro, tk.TrangThai, tk.Email
                    FROM TaiKhoan tk
                    WHERE tk.Email = @Email AND tk.TrangThai = N'Hoạt động'";

                dt = connDb.ExecuteQuery(sqlTaiKhoan, parameters);
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
            }
            catch
            {
                // Nếu bảng TaiKhoan không có cột Email, bỏ qua lỗi này
            }

            // 3. Debug: Kiểm tra xem email có tồn tại không (bỏ qua trạng thái)
            string debugSql = @"
                SELECT COUNT(*) FROM (
                    SELECT nv.Email FROM TaiKhoan tk JOIN NhanVien nv ON tk.MaNV = nv.MaNV
                    UNION
                    SELECT Email FROM TaiKhoan WHERE Email IS NOT NULL
                ) AS AllEmails WHERE Email = @Email";

            try
            {
                int count = Convert.ToInt32(connDb.ExecuteScalar(debugSql, parameters));
                if (count > 0)
                {
                    // Email tồn tại nhưng tài khoản không hoạt động
                    return null; // Vẫn trả null để hiển thị lỗi phù hợp
                }
            }
            catch
            {
                // Bỏ qua lỗi debug
            }

            return null;
        }
    }
}
