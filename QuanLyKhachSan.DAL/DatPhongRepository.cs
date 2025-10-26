using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class DatPhongRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<DatPhongModel> GetAllDatPhong()
        {
            var list = new List<DatPhongModel>();
            string sql = "SELECT * FROM DatPhong";

            var table = connDb.ExecuteQuery(sql);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new DatPhongModel
                {
                    MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    MaPhong = Convert.ToInt32(row["MaPhong"]),
                    MaNV = row["MaNV"] == DBNull.Value ? null : (int?)row["MaNV"],
                    NgayNhanPhong = Convert.ToDateTime(row["NgayNhanPhong"]),
                    NgayTraPhong = Convert.ToDateTime(row["NgayTraPhong"]),
                    SoNguoi = Convert.ToInt32(row["SoNguoi"]),
                    TongTien = row["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongTien"]),
                    TrangThai = row["TrangThai"]?.ToString(),
                    GhiChu = row["GhiChu"]?.ToString(),
                    NgayTao = Convert.ToDateTime(row["NgayTao"])
                });
            }

            return list;
        }

        public bool InsertDatPhong(DatPhongModel dp)
        {
            string sql = @"
                INSERT INTO DatPhong
                (MaKH, MaPhong, MaNV, NgayNhanPhong, NgayTraPhong, SoNguoi, TongTien, TrangThai, GhiChu, NgayTao)
                VALUES
                (@MaKH, @MaPhong, @MaNV, @NgayNhanPhong, @NgayTraPhong, @SoNguoi, @TongTien, @TrangThai, @GhiChu, @NgayTao)";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", dp.MaKH),
                new SqlParameter("@MaPhong", dp.MaPhong),
                new SqlParameter("@MaNV", dp.MaNV ?? (object)DBNull.Value),
                new SqlParameter("@NgayNhanPhong", dp.NgayNhanPhong),
                new SqlParameter("@NgayTraPhong", dp.NgayTraPhong),
                new SqlParameter("@SoNguoi", dp.SoNguoi),
                new SqlParameter("@TongTien", dp.TongTien),
                new SqlParameter("@TrangThai", dp.TrangThai ?? (object)DBNull.Value),
                new SqlParameter("@GhiChu", dp.GhiChu ?? (object)DBNull.Value),
                new SqlParameter("@NgayTao", dp.NgayTao)
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool UpdateDatPhong(DatPhongModel dp)
        {
            string sql = @"
                UPDATE DatPhong SET
                    MaKH = @MaKH,
                    MaPhong = @MaPhong,
                    MaNV = @MaNV,
                    NgayNhanPhong = @NgayNhanPhong,
                    NgayTraPhong = @NgayTraPhong,
                    SoNguoi = @SoNguoi,
                    TongTien = @TongTien,
                    TrangThai = @TrangThai,
                    GhiChu = @GhiChu
                WHERE MaDatPhong = @MaDatPhong";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDatPhong", dp.MaDatPhong),
                new SqlParameter("@MaKH", dp.MaKH),
                new SqlParameter("@MaPhong", dp.MaPhong),
                new SqlParameter("@MaNV", dp.MaNV ?? (object)DBNull.Value),
                new SqlParameter("@NgayNhanPhong", dp.NgayNhanPhong),
                new SqlParameter("@NgayTraPhong", dp.NgayTraPhong),
                new SqlParameter("@SoNguoi", dp.SoNguoi),
                new SqlParameter("@TongTien", dp.TongTien),
                new SqlParameter("@TrangThai", dp.TrangThai ?? (object)DBNull.Value),
                new SqlParameter("@GhiChu", dp.GhiChu ?? (object)DBNull.Value)
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool DeleteDatPhong(int maDatPhong)
        {
            string sql = "DELETE FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDatPhong", maDatPhong)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public DatPhongModel GetDatPhongById(int maDatPhong)
        {
            string sql = "SELECT * FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
            var parameter = new SqlParameter("@MaDatPhong", maDatPhong);

            var table = connDb.ExecuteQuery(sql, new[] { parameter });
            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            return new DatPhongModel
            {
                MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                MaKH = Convert.ToInt32(row["MaKH"]),
                MaPhong = Convert.ToInt32(row["MaPhong"]),
                MaNV = row["MaNV"] == DBNull.Value ? null : (int?)row["MaNV"],
                NgayNhanPhong = Convert.ToDateTime(row["NgayNhanPhong"]),
                NgayTraPhong = Convert.ToDateTime(row["NgayTraPhong"]),
                SoNguoi = Convert.ToInt32(row["SoNguoi"]),
                TongTien = row["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongTien"]),
                TrangThai = row["TrangThai"]?.ToString(),
                GhiChu = row["GhiChu"]?.ToString(),
                NgayTao = Convert.ToDateTime(row["NgayTao"])
            };
        }
    }
}
