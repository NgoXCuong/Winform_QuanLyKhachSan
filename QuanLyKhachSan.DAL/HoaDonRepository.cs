using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class HoaDonRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<HoaDonModel> GetAll()
        {
            var list = new List<HoaDonModel>();
            string query = @"
                SELECT 
                    hd.MaHoaDon, hd.MaDatPhong, hd.NgayLap, hd.TongTien,
                    kh.HoTen AS TenKhachHang, nv.HoTen AS TenNhanVien
                FROM HoaDon hd
                JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                JOIN NhanVien nv ON dp.MaNV = nv.MaNV";

            var table = connDb.ExecuteQuery(query);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new HoaDonModel
                {
                    MaHoaDon = (int)row["MaHoaDon"],
                    MaDatPhong = (int)row["MaDatPhong"],
                    NgayLap = Convert.ToDateTime(row["NgayLap"]),
                    TongTien = (decimal)row["TongTien"],
                    KhachHang = row["TenKhachHang"].ToString(),
                    NhanVien = row["TenNhanVien"].ToString()
                });
            }

            return list;
        }

        public HoaDonModel GetById(int maHoaDon)
        {
            string query = @"
                SELECT hd.MaHoaDon, hd.MaDatPhong, hd.NgayLap, hd.TongTien,
                       kh.HoTen AS TenKhachHang, nv.HoTen AS TenNhanVien
                FROM HoaDon hd
                JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                JOIN NhanVien nv ON dp.MaNV = nv.MaNV
                WHERE hd.MaHoaDon = @MaHoaDon";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHoaDon", maHoaDon)
            };

            var table = connDb.ExecuteQuery(query, parameters);
            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            return new HoaDonModel
            {
                MaHoaDon = (int)row["MaHoaDon"],
                MaDatPhong = (int)row["MaDatPhong"],
                NgayLap = Convert.ToDateTime(row["NgayLap"]),
                TongTien = (decimal)row["TongTien"],
                KhachHang = row["TenKhachHang"].ToString(),
                NhanVien = row["TenNhanVien"].ToString()
            };
        }

        public bool Add(HoaDonModel model)
        {
            string query = @"INSERT INTO HoaDon (MaDatPhong, NgayLap, TongTien)
                             VALUES (@MaDatPhong, @NgayLap, @TongTien)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDatPhong", model.MaDatPhong),
                new SqlParameter("@NgayLap", model.NgayLap),
                new SqlParameter("@TongTien", model.TongTien)
            };

            return connDb.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Update(HoaDonModel model)
        {
            string query = @"UPDATE HoaDon 
                             SET MaDatPhong = @MaDatPhong, NgayLap = @NgayLap, TongTien = @TongTien
                             WHERE MaHoaDon = @MaHoaDon";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHoaDon", model.MaHoaDon),
                new SqlParameter("@MaDatPhong", model.MaDatPhong),
                new SqlParameter("@NgayLap", model.NgayLap),
                new SqlParameter("@TongTien", model.TongTien)
            };

            return connDb.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int maHoaDon)
        {
            string query = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHoaDon", maHoaDon)
            };

            return connDb.ExecuteNonQuery(query, parameters) > 0;
        }

        public List<KeyValuePair<int, string>> GetDanhSachMaDatPhong()
        {
            var list = new List<KeyValuePair<int, string>>();
            string query = @"
                SELECT dp.MaDatPhong, kh.HoTen AS TenKhachHang, dp.NgayDat
                FROM DatPhong dp
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH";

            var table = connDb.ExecuteQuery(query);
            foreach (DataRow row in table.Rows)
            {
                int maDatPhong = (int)row["MaDatPhong"];
                string display = $"{row["TenKhachHang"]} - {Convert.ToDateTime(row["NgayDat"]).ToString("dd/MM/yyyy")}";
                list.Add(new KeyValuePair<int, string>(maDatPhong, display));
            }

            return list;
        }

        public List<KeyValuePair<int, string>> GetDanhSachKhachHang()
        {
            var list = new List<KeyValuePair<int, string>>();
            string query = "SELECT MaKH, HoTen FROM KhachHang";
            var table = connDb.ExecuteQuery(query);
            foreach (DataRow row in table.Rows)
            {
                int maKH = (int)row["MaKH"];
                string hoTen = row["HoTen"].ToString();
                list.Add(new KeyValuePair<int, string>(maKH, hoTen));
            }

            return list;
        }

        public List<KeyValuePair<int, string>> GetDanhSachNhanVien()
        {
            var list = new List<KeyValuePair<int, string>>();
            string query = "SELECT MaNV, HoTen FROM NhanVien";
            var table = connDb.ExecuteQuery(query);
            foreach (DataRow row in table.Rows)
            {
                int maNV = (int)row["MaNV"];
                string hoTen = row["HoTen"].ToString();
                list.Add(new KeyValuePair<int, string>(maNV, hoTen));
            }

            return list;
        }
    }
}
