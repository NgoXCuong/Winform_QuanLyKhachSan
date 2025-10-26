using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyKhachSan.DAL
{
    public class KhachHangRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<KhachHangModel> GetAllKhachHang()
        {
            List<KhachHangModel> listKhachHang = new List<KhachHangModel>();
            string sql = "SELECT * FROM KhachHang";
            var dataTable = connDb.ExecuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                KhachHangModel khachHang = new KhachHangModel
                {
                    MaKH = row["MaKH"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"]?.ToString(),
                    GioiTinh = row["GioiTinh"]?.ToString(),
                    NgaySinh = row["NgaySinh"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgaySinh"]),
                    SoDienThoai = row["SoDienThoai"]?.ToString(),
                    Email = row["Email"]?.ToString(),
                    CCCD = row["CCCD"]?.ToString(),
                    NgayTao = row["NgayTao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayTao"])
                };
                listKhachHang.Add(khachHang);
            }
            return listKhachHang;
        }

        public bool ThemKhachHang(KhachHangModel kh)
        {
            string sql = @"INSERT INTO KhachHang (HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, CCCD)
                           VALUES (@HoTen, @GioiTinh, @NgaySinh, @SoDienThoai, @Email, @CCCD)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@GioiTinh", kh.GioiTinh),
                new SqlParameter("@NgaySinh", kh.NgaySinh),
                new SqlParameter("@SoDienThoai", kh.SoDienThoai),
                new SqlParameter("@Email", kh.Email),
                new SqlParameter("@CCCD", kh.CCCD)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool SuaKhachHang(KhachHangModel kh)
        {
            string sql = @"UPDATE KhachHang SET 
                           HoTen = @HoTen, 
                           GioiTinh = @GioiTinh, 
                           NgaySinh = @NgaySinh,
                           SoDienThoai = @SoDienThoai,
                           Email = @Email,
                           CCCD = @CCCD
                           WHERE MaKH = @MaKH";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@GioiTinh", kh.GioiTinh),
                new SqlParameter("@NgaySinh", kh.NgaySinh),
                new SqlParameter("@SoDienThoai", kh.SoDienThoai),
                new SqlParameter("@Email", kh.Email),
                new SqlParameter("@CCCD", kh.CCCD)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool XoaKhachHang(int maKH)
        {
            string sql = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public List<KhachHangModel> TimKhachHang(string keyword)
        {
            List<KhachHangModel> listKhachHang = new List<KhachHangModel>();
            string sql = "SELECT * FROM KhachHang";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                List<string> conditions = new List<string>();

                if (int.TryParse(keyword, out int maKH))
                {
                    conditions.Add("MaKH = @MaKH");
                    parameters.Add(new SqlParameter("@MaKH", maKH));
                }

                conditions.Add("HoTen LIKE @kw");
                conditions.Add("GioiTinh LIKE @kw");
                conditions.Add("SoDienThoai LIKE @kw");
                conditions.Add("Email LIKE @kw");
                conditions.Add("CCCD LIKE @kw");
                parameters.Add(new SqlParameter("@kw", "%" + keyword + "%"));

                if (DateTime.TryParse(keyword, out DateTime ngaySinh))
                {
                    conditions.Add("(NgaySinh >= @NgaySinhStart AND NgaySinh < @NgaySinhEnd)");
                    parameters.Add(new SqlParameter("@NgaySinhStart", ngaySinh.Date));
                    parameters.Add(new SqlParameter("@NgaySinhEnd", ngaySinh.Date.AddDays(1)));
                }

                sql += " WHERE " + string.Join(" OR ", conditions);
            }

            var dataTable = connDb.ExecuteQuery(sql, parameters.ToArray());

            foreach (DataRow row in dataTable.Rows)
            {
                KhachHangModel khachHang = new KhachHangModel
                {
                    MaKH = row["MaKH"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaKH"]),
                    HoTen = row["HoTen"]?.ToString(),
                    GioiTinh = row["GioiTinh"]?.ToString(),
                    NgaySinh = row["NgaySinh"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgaySinh"]),
                    SoDienThoai = row["SoDienThoai"]?.ToString(),
                    Email = row["Email"]?.ToString(),
                    CCCD = row["CCCD"]?.ToString(),
                    NgayTao = row["NgayTao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayTao"])
                };
                listKhachHang.Add(khachHang);
            }

            return listKhachHang;
        }

        public KhachHangModel GetById(int maKH)
        {
            string query = "SELECT * FROM KhachHang WHERE MaKH = @MaKH";
            var parameter = new SqlParameter("@MaKH", maKH);
            var table = connDb.ExecuteQuery(query, new[] { parameter });

            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            return new KhachHangModel
            {
                MaKH = row["MaKH"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaKH"]),
                HoTen = row["HoTen"]?.ToString(),
                GioiTinh = row["GioiTinh"]?.ToString(),
                NgaySinh = row["NgaySinh"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgaySinh"]),
                SoDienThoai = row["SoDienThoai"]?.ToString(),
                Email = row["Email"]?.ToString(),
                CCCD = row["CCCD"]?.ToString(),
                NgayTao = row["NgayTao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayTao"])
            };
        }
    }
}
