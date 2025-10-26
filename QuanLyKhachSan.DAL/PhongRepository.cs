using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class PhongRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        // Lấy tất cả phòng
        public List<PhongModel> GetAllPhong()
        {
            List<PhongModel> listPhong = new List<PhongModel>();
            string sql = "SELECT * FROM Phong";
            var dataTable = connDb.ExecuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                PhongModel phong = new PhongModel
                {
                    MaPhong = row["MaPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaPhong"]),
                    SoPhong = row["SoPhong"]?.ToString(),
                    MaLoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                    Tang = row["Tang"] == DBNull.Value ? 0 : Convert.ToInt32(row["Tang"]),
                    TrangThai = row["TrangThai"]?.ToString()
                };
                listPhong.Add(phong);
            }
            return listPhong;
        }

        // Thêm phòng
        public bool ThemPhong(PhongModel phong)
        {
            string sql = @"INSERT INTO Phong (SoPhong, MaLoaiPhong, Tang, TrangThai) 
                           VALUES (@SoPhong, @MaLoaiPhong, @Tang, @TrangThai)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@MaLoaiPhong", phong.MaLoaiPhong),
                new SqlParameter("@Tang", phong.Tang),
                new SqlParameter("@TrangThai", phong.TrangThai)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // Sửa phòng
        public bool SuaPhong(PhongModel phong)
        {
            string sql = @"UPDATE Phong 
                           SET SoPhong = @SoPhong, MaLoaiPhong = @MaLoaiPhong, Tang = @Tang, TrangThai = @TrangThai 
                           WHERE MaPhong = @MaPhong";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhong", phong.MaPhong),
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@MaLoaiPhong", phong.MaLoaiPhong),
                new SqlParameter("@Tang", phong.Tang),
                new SqlParameter("@TrangThai", phong.TrangThai)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // Xóa phòng
        public bool XoaPhong(int maPhong)
        {
            try
            {
                string sql = "DELETE FROM Phong WHERE MaPhong = @MaPhong";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaPhong", maPhong)
                };
                int rowsAffected = connDb.ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Khóa ngoại
                {
                    MessageBox.Show("Phòng không thể xóa vì tồn tại chi tiết đặt phòng liên quan.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        // Tìm phòng theo keyword
        public List<PhongModel> TimPhong(string keyword)
        {
            List<PhongModel> listPhong = new List<PhongModel>();
            string sql = @"SELECT * FROM Phong WHERE 
                CAST(MaPhong AS NVARCHAR) = @Keyword OR
                SoPhong = @Keyword OR
                CAST(MaLoaiPhong AS NVARCHAR) = @Keyword OR
                CAST(Tang AS NVARCHAR) = @Keyword OR
                TrangThai = @Keyword";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", keyword)
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                PhongModel phong = new PhongModel
                {
                    MaPhong = row["MaPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaPhong"]),
                    SoPhong = row["SoPhong"]?.ToString(),
                    MaLoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                    Tang = row["Tang"] == DBNull.Value ? 0 : Convert.ToInt32(row["Tang"]),
                    TrangThai = row["TrangThai"]?.ToString()
                };
                listPhong.Add(phong);
            }
            return listPhong;
        }

        // Kiểm tra số phòng tồn tại
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
    }
}
