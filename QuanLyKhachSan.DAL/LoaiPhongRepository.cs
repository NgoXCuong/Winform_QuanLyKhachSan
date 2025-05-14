using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class LoaiPhongRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();
        public List<LoaiPhongModel> GetAllLoaiPhong()
        {
            List<LoaiPhongModel> listLoaiPhong = new List<LoaiPhongModel>();
            string sql = "SELECT * FROM LoaiPhong";
            var dataTable = connDb.ExecuteQuery(sql);
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                LoaiPhongModel loaiPhong = new LoaiPhongModel
                {
                    MaLoaiPhong = (int)row["MaLoaiPhong"],
                    TenLoaiPhong = row["TenLoaiPhong"].ToString(),
                    MoTa = row["MoTa"].ToString(),
                    GiaPhong = (decimal)row["GiaPhong"],
                    SoNguoiToiDa = (int)row["SoNguoiToiDa"]
                };
                listLoaiPhong.Add(loaiPhong);
            }
            return listLoaiPhong;
        }

        public List<LoaiPhongModel> GetLoaiPhongByIdName()
        {
            List<LoaiPhongModel> ds = new List<LoaiPhongModel>();
            string sql = "SELECT MaLoaiPhong, TenLoaiPhong FROM LoaiPhong";
            var table = connDb.ExecuteQuery(sql);
            foreach (System.Data.DataRow row in table.Rows)
            {
                ds.Add(new LoaiPhongModel
                {
                    MaLoaiPhong = Convert.ToInt32(row["MaLoaiPhong"]),
                    TenLoaiPhong = row["TenLoaiPhong"].ToString()
                });
            }
            return ds;
        }

        public bool ThemLoaiPhong(LoaiPhongModel loaiPhong)
        {
            string sql = "INSERT INTO LoaiPhong (TenLoaiPhong, MoTa, GiaPhong, SoNguoiToiDa) VALUES (@TenLoaiPhong, @MoTa, @GiaPhong, @SoNguoiToiDa)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@TenLoaiPhong", loaiPhong.TenLoaiPhong),
                new SqlParameter("@MoTa", loaiPhong.MoTa),
                new SqlParameter("@GiaPhong", loaiPhong.GiaPhong),
                new SqlParameter("@SoNguoiToiDa", loaiPhong.SoNguoiToiDa)
            };
            return connDb.ExecuteNonQuery(sql, parameters.ToArray()) > 0;
        }

        public bool SuaLoaiPhong(LoaiPhongModel loaiPhong)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = @"UPDATE LoaiPhong SET TenLoaiPhong = @TenLoaiPhong, 
                        MoTa = @MoTa,
                        GiaPhong = @GiaPhong,
                        SoNguoiToiDa = @SoNguoiToiDa
                        WHERE MaLoaiPhong = @MaLoaiPhong";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenLoaiPhong", loaiPhong.TenLoaiPhong),
                    new SqlParameter("@MoTa", loaiPhong.MoTa),
                    new SqlParameter("@GiaPhong", loaiPhong.GiaPhong),
                    new SqlParameter("@SoNguoiToiDa", loaiPhong.SoNguoiToiDa),
                    new SqlParameter("@MaLoaiPhong", loaiPhong.MaLoaiPhong)
                };

                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
        }

        public bool XoaLoaiPhong(int maLoaiPhong)
        {
            string sql = "DELETE FROM LoaiPhong WHERE MaLoaiPhong = @MaLoaiPhong";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLoaiPhong", maLoaiPhong)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public List<LoaiPhongModel> TimLoaiPhong(string keyword)
        {
            List<LoaiPhongModel> listLoaiPhong = new List<LoaiPhongModel>();

            string sql = @"SELECT * FROM LoaiPhong WHERE 
                MaLoaiPhong = @Keyword
                OR TenLoaiPhong = @Keyword
                OR MoTa = @Keyword
                OR CAST(GiaPhong AS NVARCHAR) = @Keyword
                OR CAST(SoNguoiToiDa AS NVARCHAR) = @Keyword";

            if (string.IsNullOrWhiteSpace(keyword))
            {
                sql = "SELECT * FROM LoaiPhong"; // Nếu không nhập từ khóa, trả về tất cả
            }

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", keyword ?? string.Empty),
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                LoaiPhongModel loaiPhong = new LoaiPhongModel
                {
                    MaLoaiPhong = (int)row["MaLoaiPhong"],
                    TenLoaiPhong = row["TenLoaiPhong"].ToString(),
                    MoTa = row["MoTa"].ToString(),
                    GiaPhong = (decimal)row["GiaPhong"],
                    SoNguoiToiDa = (int)row["SoNguoiToiDa"]
                };
                listLoaiPhong.Add(loaiPhong);
            }
            return listLoaiPhong;
        }

    }
}
