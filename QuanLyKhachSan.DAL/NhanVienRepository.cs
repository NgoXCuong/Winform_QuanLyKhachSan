using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyKhachSan.DAL
{
    public class NhanVienRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<NhanVienModel> GetAllNhanVien()
        {
            List<NhanVienModel> listNhanVien = new List<NhanVienModel>();
            string sql = "SELECT * FROM NhanVien";
            var dataTable = connDb.ExecuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                NhanVienModel nv = new NhanVienModel
                {
                    MaNV = row["MaNV"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"]?.ToString(),
                    GioiTinh = row["GioiTinh"]?.ToString(),
                    NgaySinh = row["NgaySinh"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgaySinh"]),
                    ChucVu = row["ChucVu"]?.ToString(),
                    SoDienThoai = row["SoDienThoai"]?.ToString(),
                    Email = row["Email"]?.ToString(),
                    Anh = row["Anh"] as byte[],
                    TrangThai = row["TrangThai"]?.ToString()
                };
                listNhanVien.Add(nv);
            }
            return listNhanVien;
        }

        public List<NhanVienModel> GetNhanVienByIdName()
        {
            List<NhanVienModel> ds = new List<NhanVienModel>();
            string sql = "SELECT MaNV, HoTen FROM NhanVien";
            var table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                ds.Add(new NhanVienModel
                {
                    MaNV = row["MaNV"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"]?.ToString(),
                    // Other properties can be null/default since they're not used for display
                    GioiTinh = null,
                    NgaySinh = null,
                    ChucVu = null,
                    SoDienThoai = null,
                    Email = null,
                    Anh = null,
                    TrangThai = null
                });
            }
            return ds;
        }

        public bool ThemNhanVien(NhanVienModel nv)
        {
            string sql = @"INSERT INTO NhanVien 
                           (HoTen, GioiTinh, NgaySinh, ChucVu, SoDienThoai, Email, Anh, TrangThai) 
                           VALUES (@HoTen, @GioiTinh, @NgaySinh, @ChucVu, @SoDienThoai, @Email, @Anh, @TrangThai)";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@GioiTinh", nv.GioiTinh),
                new SqlParameter("@NgaySinh", nv.NgaySinh),
                new SqlParameter("@ChucVu", nv.ChucVu),
                new SqlParameter("@SoDienThoai", nv.SoDienThoai),
                new SqlParameter("@Email", nv.Email),
                new SqlParameter("@Anh", SqlDbType.VarBinary) { Value = nv.Anh ?? (object)DBNull.Value },
                new SqlParameter("@TrangThai", nv.TrangThai ?? "Đang làm")
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool SuaNhanVien(NhanVienModel nv)
        {
            // Lấy trạng thái hiện tại của nhân viên
            string getCurrentStatusSql = "SELECT TrangThai FROM NhanVien WHERE MaNV = @MaNV";
            var getParams = new SqlParameter[] { new SqlParameter("@MaNV", nv.MaNV) };
            var currentStatusResult = connDb.ExecuteQuery(getCurrentStatusSql, getParams);

            string currentStatus = "";
            if (currentStatusResult.Rows.Count > 0)
            {
                currentStatus = currentStatusResult.Rows[0]["TrangThai"].ToString();
            }

            // Cập nhật nhân viên
            string sql = @"UPDATE NhanVien SET
                           HoTen = @HoTen,
                           GioiTinh = @GioiTinh,
                           NgaySinh = @NgaySinh,
                           ChucVu = @ChucVu,
                           SoDienThoai = @SoDienThoai,
                           Email = @Email,
                           TrangThai = @TrangThai
                           WHERE MaNV = @MaNV";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@HoTen", nv.HoTen),
                new SqlParameter("@GioiTinh", nv.GioiTinh),
                new SqlParameter("@NgaySinh", nv.NgaySinh),
                new SqlParameter("@ChucVu", nv.ChucVu),
                new SqlParameter("@SoDienThoai", nv.SoDienThoai),
                new SqlParameter("@Email", nv.Email),
                new SqlParameter("@TrangThai", nv.TrangThai),
                new SqlParameter("@MaNV", nv.MaNV)
            };

            bool result = connDb.ExecuteNonQuery(sql, parameters) > 0;

            // Nếu cập nhật thành công và trạng thái thay đổi thành "Nghỉ việc"
            if (result && currentStatus != "Nghỉ việc" && nv.TrangThai == "Nghỉ việc")
            {
                // Cập nhật trạng thái tài khoản thành "Ngưng hoạt động"
                string updateTaiKhoanSql = @"UPDATE TaiKhoan
                                           SET TrangThai = N'Ngưng hoạt động'
                                           WHERE MaNV = @MaNV";
                var taiKhoanParams = new SqlParameter[]
                {
                    new SqlParameter("@MaNV", nv.MaNV)
                };
                connDb.ExecuteNonQuery(updateTaiKhoanSql, taiKhoanParams);
            }

            return result;
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
            string sql = "SELECT * FROM NhanVien WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                List<string> conditions = new List<string>();

                if (int.TryParse(keyword, out int maNv))
                {
                    conditions.Add("MaNV = @MaNV");
                    parameters.Add(new SqlParameter("@MaNV", maNv));
                }

                conditions.Add("HoTen LIKE '%' + @kw + '%'");
                conditions.Add("GioiTinh LIKE '%' + @kw + '%'");
                conditions.Add("ChucVu LIKE '%' + @kw + '%'");
                conditions.Add("SoDienThoai LIKE '%' + @kw + '%'");
                conditions.Add("Email LIKE '%' + @kw + '%'");
                parameters.Add(new SqlParameter("@kw", keyword));

                if (DateTime.TryParse(keyword, out DateTime ngaySinh))
                {
                    conditions.Add("(NgaySinh >= @NgaySinhStart AND NgaySinh < @NgaySinhEnd)");
                    parameters.Add(new SqlParameter("@NgaySinhStart", ngaySinh.Date));
                    parameters.Add(new SqlParameter("@NgaySinhEnd", ngaySinh.Date.AddDays(1)));
                }

                sql += " AND (" + string.Join(" OR ", conditions) + ")";
            }

            var dataTable = connDb.ExecuteQuery(sql, parameters.ToArray());

            foreach (DataRow row in dataTable.Rows)
            {
                NhanVienModel nv = new NhanVienModel
                {
                    MaNV = row["MaNV"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaNV"]),
                    HoTen = row["HoTen"]?.ToString(),
                    GioiTinh = row["GioiTinh"]?.ToString(),
                    NgaySinh = row["NgaySinh"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgaySinh"]),
                    ChucVu = row["ChucVu"]?.ToString(),
                    SoDienThoai = row["SoDienThoai"]?.ToString(),
                    Email = row["Email"]?.ToString(),
                    Anh = row["Anh"] as byte[],
                    TrangThai = row["TrangThai"]?.ToString()
                };
                listNhanVien.Add(nv);
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
            var result = connDb.ExecuteScalar(sql, parameters);
            return result != DBNull.Value && result != null ? (byte[])result : null;
        }

        public bool XoaAnhNhanVien(int maNV)
        {
            string sql = "UPDATE NhanVien SET Anh = NULL WHERE MaNV = @MaNV";
            SqlParameter[] parameters = { new SqlParameter("@MaNV", maNV) };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public string GetChucVuByMaNV(int maNV)
        {
            string sql = "SELECT ChucVu FROM NhanVien WHERE MaNV = @MaNV";
            var parameters = new SqlParameter[] { new SqlParameter("@MaNV", maNV) };
            var result = connDb.ExecuteScalar(sql, parameters);
            return result?.ToString();
        }

        public NhanVienModel GetById(int maNV)
        {
            string sql = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";
            var table = connDb.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaNV", maNV) });

            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            return new NhanVienModel
            {
                MaNV = row["MaNV"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaNV"]),
                HoTen = row["HoTen"]?.ToString(),
                GioiTinh = row["GioiTinh"]?.ToString(),
                NgaySinh = row["NgaySinh"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgaySinh"]),
                ChucVu = row["ChucVu"]?.ToString(),
                SoDienThoai = row["SoDienThoai"]?.ToString(),
                Email = row["Email"]?.ToString(),
                Anh = row["Anh"] as byte[],
                TrangThai = row["TrangThai"]?.ToString()
            };
        }
    }
}
