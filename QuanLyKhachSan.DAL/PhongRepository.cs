using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.Models;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace QuanLyKhachSan.DAL
{
    public class PhongRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<PhongModel> getAllPhong()
        {
            List<PhongModel> listPhong = new List<PhongModel>();
            string sql = "SELECT * FROM Phong";
            var dataTable = connDb.ExecuteQuery(sql);
            foreach (DataRow row in dataTable.Rows)
            {
                PhongModel phong = new PhongModel
                {
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    SoPhong = Convert.ToInt32(row["SoPhong"]),
                    LoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TrangThai = row["TrangThai"]?.ToString()
                };
                listPhong.Add(phong);
            }
            return listPhong;
        }

        public bool ThemPhong(PhongModel phong)
        {
            string sql = @"INSERT INTO Phong (SoPhong, MaLoaiPhong, TrangThai) 
                           VALUES (@SoPhong, @MaLoaiPhong, @TrangThai)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@SoPhong", phong.SoPhong),
                new SqlParameter("@MaLoaiPhong", phong.LoaiPhong),
                new SqlParameter("@TrangThai", phong.TrangThai)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool SuaPhong(PhongModel phong)
        {
            string sql = @"UPDATE Phong 
                       SET SoPhong = @SoPhong, MaLoaiPhong = @MaLoaiPhong, TrangThai = @TrangThai 
                       WHERE MaPhong = @MaPhong";
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@MaPhong", phong.MaPhong),
            new SqlParameter("@SoPhong", phong.SoPhong),
            new SqlParameter("@MaLoaiPhong", phong.LoaiPhong),
            new SqlParameter("@TrangThai", phong.TrangThai)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }
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
                // Kiểm tra nếu lỗi do ràng buộc khóa ngoại (error number 547)
                if (ex.Number == 547)
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

        // Tìm  phòng theo bất kỳ  keyword nào
        public List<PhongModel> TimPhong(string keyword)
        {
            List<PhongModel> listPhong = new List<PhongModel>();

            string sql = @"SELECT * FROM Phong WHERE 
                CAST(MaPhong AS NVARCHAR) = @Keyword OR
                CAST(SoPhong AS NVARCHAR) = @Keyword OR
                CAST(MaLoaiPhong AS NVARCHAR) = @Keyword OR
                TrangThai = @Keyword";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", keyword)
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                PhongModel phong = new PhongModel
                {
                    MaPhong = row["MaPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaPhong"]),
                    SoPhong = row["SoPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoPhong"]),
                    LoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                    TrangThai = row["TrangThai"] == DBNull.Value ? "" : row["TrangThai"].ToString()
                };
                listPhong.Add(phong);
            }
            return listPhong;
        }
    }
}
