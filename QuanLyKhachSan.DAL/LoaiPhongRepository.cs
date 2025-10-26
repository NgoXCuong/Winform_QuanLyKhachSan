using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

            foreach (DataRow row in dataTable.Rows)
            {
                LoaiPhongModel loaiPhong = new LoaiPhongModel
                {
                    MaLoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                    TenLoaiPhong = row["TenLoaiPhong"]?.ToString(),
                    GiaCoBan = row["GiaCoBan"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GiaCoBan"]),
                    SucChuaToiDa = row["SucChuaToiDa"] == DBNull.Value ? 0 : Convert.ToInt32(row["SucChuaToiDa"]),
                    MoTa = row["MoTa"]?.ToString()  // Thêm dòng này
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

            foreach (DataRow row in table.Rows)
            {
                ds.Add(new LoaiPhongModel
                {
                    MaLoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                    TenLoaiPhong = row["TenLoaiPhong"]?.ToString()
                });
            }
            return ds;
        }

        public bool ThemLoaiPhong(LoaiPhongModel loaiPhong)
        {
            string sql = @"INSERT INTO LoaiPhong (TenLoaiPhong, GiaCoBan, SucChuaToiDa, MoTa) 
               VALUES (@TenLoaiPhong, @GiaCoBan, @SucChuaToiDa, @MoTa)";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLoaiPhong", loaiPhong.TenLoaiPhong),
                new SqlParameter("@GiaCoBan", loaiPhong.GiaCoBan),
                new SqlParameter("@SucChuaToiDa", loaiPhong.SucChuaToiDa),
                new SqlParameter("@MoTa", loaiPhong.MoTa ?? (object)DBNull.Value)
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool SuaLoaiPhong(LoaiPhongModel loaiPhong)
        {
            string sql = @"UPDATE LoaiPhong SET 
               TenLoaiPhong = @TenLoaiPhong, 
               GiaCoBan = @GiaCoBan,
               SucChuaToiDa = @SucChuaToiDa,
               MoTa = @MoTa
               WHERE MaLoaiPhong = @MaLoaiPhong";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TenLoaiPhong", loaiPhong.TenLoaiPhong),
                new SqlParameter("@GiaCoBan", loaiPhong.GiaCoBan),
                new SqlParameter("@SucChuaToiDa", loaiPhong.SucChuaToiDa),
                new SqlParameter("@MoTa", loaiPhong.MoTa ?? (object)DBNull.Value),
                new SqlParameter("@MaLoaiPhong", loaiPhong.MaLoaiPhong)
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool XoaLoaiPhong(int maLoaiPhong)
        {
            try
            {
                string sql = "DELETE FROM LoaiPhong WHERE MaLoaiPhong = @MaLoaiPhong";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLoaiPhong", maLoaiPhong)
                };
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Ràng buộc khóa ngoại
                {
                    Console.WriteLine("Không thể xóa loại phòng vì tồn tại phòng liên quan.");
                }
                else
                {
                    Console.WriteLine("Lỗi SQL: " + ex.Message);
                }
                return false;
            }
        }

        public List<LoaiPhongModel> TimLoaiPhong(string keyword)
        {
            List<LoaiPhongModel> listLoaiPhong = new List<LoaiPhongModel>();
            string sql = "SELECT * FROM LoaiPhong";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " WHERE TenLoaiPhong LIKE @kw OR CAST(GiaCoBan AS NVARCHAR(20)) LIKE @kw OR CAST(SucChuaToiDa AS NVARCHAR(10)) LIKE @kw";
                parameters.Add(new SqlParameter("@kw", "%" + keyword + "%"));
            }

            var dataTable = connDb.ExecuteQuery(sql, parameters.ToArray());

            foreach (DataRow row in dataTable.Rows)
            {
                LoaiPhongModel loaiPhong = new LoaiPhongModel
                {
                    MaLoaiPhong = row["MaLoaiPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaLoaiPhong"]),
                    TenLoaiPhong = row["TenLoaiPhong"]?.ToString(),
                    GiaCoBan = row["GiaCoBan"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GiaCoBan"]),
                    SucChuaToiDa = row["SucChuaToiDa"] == DBNull.Value ? 0 : Convert.ToInt32(row["SucChuaToiDa"]),
                    MoTa = row["MoTa"]?.ToString()
                };
                listLoaiPhong.Add(loaiPhong);
            }
            return listLoaiPhong;
        }

    }
}
