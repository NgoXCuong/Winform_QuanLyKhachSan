using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyKhachSan.DAL
{
    public class PhongRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        // 🧩 Lấy tất cả phòng
        public List<PhongModel> GetAllPhong()
        {
            List<PhongModel> listPhong = new List<PhongModel>();

            // ✅ JOIN để lấy thêm tên loại phòng
            string sql = @"
        SELECT 
            p.MaPhong,
            p.SoPhong,
            p.MaLoaiPhong,
            lp.TenLoaiPhong,
            p.Tang,
            p.TrangThai,
            p.Anh
        FROM Phong p
        INNER JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong";

            var dataTable = connDb.ExecuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                listPhong.Add(new PhongModel
                {
                    MaPhong = row["MaPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaPhong"]),
                    SoPhong = row["SoPhong"]?.ToString(),
                    MaLoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                    TenLoaiPhong = row["TenLoaiPhong"]?.ToString(), // ✅ thêm dòng này
                    Tang = row["Tang"] == DBNull.Value ? 0 : Convert.ToInt32(row["Tang"]),
                    TrangThai = row["TrangThai"]?.ToString(),
                    Anh = row["Anh"] as byte[]
                });
            }

            return listPhong;
        }


        // ➕ Thêm phòng
        public bool ThemPhong(PhongModel phong)
        {
            string sql = @"INSERT INTO Phong (SoPhong, MaLoaiPhong, Tang, TrangThai, Anh) 
                           VALUES (@SoPhong, @MaLoaiPhong, @Tang, @TrangThai, @Anh)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@MaLoaiPhong", phong.MaLoaiPhong),
                new SqlParameter("@Tang", phong.Tang),
                new SqlParameter("@TrangThai", phong.TrangThai ?? "Trống"),
                new SqlParameter("@Anh", SqlDbType.VarBinary) { Value = phong.Anh ?? (object)DBNull.Value }
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // ✏️ Sửa phòng
        public bool SuaPhong(PhongModel phong)
        {
            string sql = @"UPDATE Phong 
                           SET SoPhong = @SoPhong, 
                               MaLoaiPhong = @MaLoaiPhong, 
                               Tang = @Tang, 
                               TrangThai = @TrangThai, 
                               Anh = @Anh
                           WHERE MaPhong = @MaPhong";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhong", phong.MaPhong),
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@MaLoaiPhong", phong.MaLoaiPhong),
                new SqlParameter("@Tang", phong.Tang),
                new SqlParameter("@TrangThai", phong.TrangThai),
                new SqlParameter("@Anh", SqlDbType.VarBinary) { Value = phong.Anh ?? (object)DBNull.Value }
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // 🗑️ Xóa phòng
        public bool XoaPhong(int maPhong)
        {
            try
            {
                string sql = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                var parameters = new SqlParameter[] { new SqlParameter("@MaPhong", maPhong) };
                int rowsAffected = connDb.ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Khóa ngoại tồn tại (liên quan đến DatPhong)
                {
                    MessageBox.Show("Không thể xóa phòng vì có dữ liệu đặt phòng liên quan.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // 🔍 Tìm phòng theo từ khóa
        public List<PhongModel> TimPhong(string keyword)
        {
            List<PhongModel> listPhong = new List<PhongModel>();
            string sql = "SELECT * FROM Phong WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                List<string> conditions = new List<string>();

                if (int.TryParse(keyword, out int maPhong))
                {
                    conditions.Add("MaPhong = @MaPhong");
                    parameters.Add(new SqlParameter("@MaPhong", maPhong));
                }

                conditions.Add("SoPhong LIKE '%' + @kw + '%'");
                conditions.Add("TrangThai LIKE '%' + @kw + '%'");
                conditions.Add("CAST(Tang AS NVARCHAR) LIKE '%' + @kw + '%'");
                parameters.Add(new SqlParameter("@kw", keyword));

                sql += " AND (" + string.Join(" OR ", conditions) + ")";
            }

            var dataTable = connDb.ExecuteQuery(sql, parameters.ToArray());
            foreach (DataRow row in dataTable.Rows)
            {
                listPhong.Add(new PhongModel
                {
                    MaPhong = row["MaPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaPhong"]),
                    SoPhong = row["SoPhong"]?.ToString(),
                    MaLoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                    Tang = row["Tang"] == DBNull.Value ? 0 : Convert.ToInt32(row["Tang"]),
                    TrangThai = row["TrangThai"]?.ToString(),
                    Anh = row["Anh"] as byte[]
                });
            }

            return listPhong;
        }

        // ✅ Kiểm tra số phòng tồn tại
        public bool KiemTraSoPhongTonTai(string soPhong)
        {
            string sql = "SELECT COUNT(*) FROM Phong WHERE SoPhong = @SoPhong";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@SoPhong", soPhong)
            };
            int count = (int)connDb.ExecuteScalar(sql, parameters);
            return count > 0;
        }

        // 🖼️ Cập nhật ảnh phòng (từ base64)
        public bool CapNhatAnh(int maPhong, string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            string sql = "UPDATE Phong SET Anh = @Anh WHERE MaPhong = @MaPhong";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Anh", SqlDbType.VarBinary) { Value = imageBytes },
                new SqlParameter("@MaPhong", maPhong)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // 🧾 Lấy ảnh phòng
        public byte[] LayAnhPhong(int maPhong)
        {
            string sql = "SELECT Anh FROM Phong WHERE MaPhong = @MaPhong";
            SqlParameter[] parameters = { new SqlParameter("@MaPhong", maPhong) };
            var result = connDb.ExecuteScalar(sql, parameters);
            return result != DBNull.Value && result != null ? (byte[])result : null;
        }

        // ❌ Xóa ảnh phòng
        public bool XoaAnhPhong(int maPhong)
        {
            string sql = "UPDATE Phong SET Anh = NULL WHERE MaPhong = @MaPhong";
            SqlParameter[] parameters = { new SqlParameter("@MaPhong", maPhong) };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // 🔎 Lấy thông tin phòng theo ID
        public PhongModel GetById(int maPhong)
        {
            string sql = "SELECT * FROM Phong WHERE MaPhong = @MaPhong";
            var table = connDb.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@MaPhong", maPhong) });

            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            return new PhongModel
            {
                MaPhong = row["MaPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaPhong"]),
                SoPhong = row["SoPhong"]?.ToString(),
                MaLoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                Tang = row["Tang"] == DBNull.Value ? 0 : Convert.ToInt32(row["Tang"]),
                TrangThai = row["TrangThai"]?.ToString(),
                Anh = row["Anh"] as byte[]
            };
        }
    }
}
