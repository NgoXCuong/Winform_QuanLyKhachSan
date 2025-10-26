using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                    hd.MaHD, hd.MaDatPhong, hd.MaKH, hd.TienPhong, hd.TongTienThanhToan,
                    hd.TrangThaiThanhToan, hd.PhuongThucThanhToan, hd.NgayThanhToan, hd.GhiChu, hd.NgayTao,
                    kh.HoTen AS TenKhachHang,
                    dp.MaNV AS MaNhanVien, nv.HoTen AS TenNhanVien
                FROM HoaDon hd
                JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
                JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                LEFT JOIN NhanVien nv ON dp.MaNV = nv.MaNV";

            var table = connDb.ExecuteQuery(query);
            foreach (DataRow row in table.Rows)
            {
                list.Add(new HoaDonModel
                {
                    MaHD = Convert.ToInt32(row["MaHD"]),
                    MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                    MaKH = Convert.ToInt32(row["MaKH"]),
                    TienPhong = row["TienPhong"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TienPhong"]),
                    TongTienThanhToan = row["TongTienThanhToan"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongTienThanhToan"]),
                    TrangThaiThanhToan = row["TrangThaiThanhToan"]?.ToString(),
                    PhuongThucThanhToan = row["PhuongThucThanhToan"]?.ToString(),
                    NgayThanhToan = row["NgayThanhToan"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgayThanhToan"]),
                    GhiChu = row["GhiChu"]?.ToString(),
                    NgayTao = Convert.ToDateTime(row["NgayTao"])
                });
            }
            return list;
        }

        public HoaDonModel GetById(int maHD)
        {
            string query = @"
                SELECT 
                    hd.MaHD, hd.MaDatPhong, hd.MaKH, hd.TienPhong, hd.TongTienThanhToan,
                    hd.TrangThaiThanhToan, hd.PhuongThucThanhToan, hd.NgayThanhToan, hd.GhiChu, hd.NgayTao,
                    kh.HoTen AS TenKhachHang,
                    dp.MaNV AS MaNhanVien, nv.HoTen AS TenNhanVien
                FROM HoaDon hd
                JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
                JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                LEFT JOIN NhanVien nv ON dp.MaNV = nv.MaNV
                WHERE hd.MaHD = @MaHD";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHD", maHD)
            };

            var table = connDb.ExecuteQuery(query, parameters);
            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            return new HoaDonModel
            {
                MaHD = Convert.ToInt32(row["MaHD"]),
                MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                MaKH = Convert.ToInt32(row["MaKH"]),
                TienPhong = row["TienPhong"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TienPhong"]),
                TongTienThanhToan = row["TongTienThanhToan"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongTienThanhToan"]),
                TrangThaiThanhToan = row["TrangThaiThanhToan"]?.ToString(),
                PhuongThucThanhToan = row["PhuongThucThanhToan"]?.ToString(),
                NgayThanhToan = row["NgayThanhToan"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NgayThanhToan"]),
                GhiChu = row["GhiChu"]?.ToString(),
                NgayTao = Convert.ToDateTime(row["NgayTao"])
            };
        }

        public bool Add(HoaDonModel model)
        {
            string query = @"INSERT INTO HoaDon 
                             (MaDatPhong, MaKH, TienPhong, TongTienThanhToan, TrangThaiThanhToan, PhuongThucThanhToan, NgayThanhToan, GhiChu)
                             VALUES (@MaDatPhong, @MaKH, @TienPhong, @TongTienThanhToan, @TrangThaiThanhToan, @PhuongThucThanhToan, @NgayThanhToan, @GhiChu)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDatPhong", model.MaDatPhong),
                new SqlParameter("@MaKH", model.MaKH),
                new SqlParameter("@TienPhong", model.TienPhong),
                new SqlParameter("@TongTienThanhToan", model.TongTienThanhToan),
                new SqlParameter("@TrangThaiThanhToan", model.TrangThaiThanhToan ?? (object)DBNull.Value),
                new SqlParameter("@PhuongThucThanhToan", model.PhuongThucThanhToan ?? (object)DBNull.Value),
                new SqlParameter("@NgayThanhToan", model.NgayThanhToan.HasValue ? (object)model.NgayThanhToan.Value : DBNull.Value),
                new SqlParameter("@GhiChu", model.GhiChu ?? (object)DBNull.Value)
            };
            return connDb.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Update(HoaDonModel model)
        {
            string query = @"UPDATE HoaDon
                             SET MaDatPhong = @MaDatPhong,
                                 MaKH = @MaKH,
                                 TienPhong = @TienPhong,
                                 TongTienThanhToan = @TongTienThanhToan,
                                 TrangThaiThanhToan = @TrangThaiThanhToan,
                                 PhuongThucThanhToan = @PhuongThucThanhToan,
                                 NgayThanhToan = @NgayThanhToan,
                                 GhiChu = @GhiChu
                             WHERE MaHD = @MaHD";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHD", model.MaHD),
                new SqlParameter("@MaDatPhong", model.MaDatPhong),
                new SqlParameter("@MaKH", model.MaKH),
                new SqlParameter("@TienPhong", model.TienPhong),
                new SqlParameter("@TongTienThanhToan", model.TongTienThanhToan),
                new SqlParameter("@TrangThaiThanhToan", model.TrangThaiThanhToan ?? (object)DBNull.Value),
                new SqlParameter("@PhuongThucThanhToan", model.PhuongThucThanhToan ?? (object)DBNull.Value),
                new SqlParameter("@NgayThanhToan", model.NgayThanhToan.HasValue ? (object)model.NgayThanhToan.Value : DBNull.Value),
                new SqlParameter("@GhiChu", model.GhiChu ?? (object)DBNull.Value)
            };
            return connDb.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int maHD)
        {
            string query = "DELETE FROM HoaDon WHERE MaHD = @MaHD";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHD", maHD)
            };
            return connDb.ExecuteNonQuery(query, parameters) > 0;
        }

        public List<KeyValuePair<int, string>> GetDanhSachMaDatPhong()
        {
            var list = new List<KeyValuePair<int, string>>();
            string query = @"
                SELECT dp.MaDatPhong, kh.HoTen AS TenKhachHang, dp.NgayNhanPhong
                FROM DatPhong dp
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH";

            var table = connDb.ExecuteQuery(query);
            foreach (DataRow row in table.Rows)
            {
                int maDatPhong = (int)row["MaDatPhong"];
                string display = $"{maDatPhong} - {row["TenKhachHang"]} ({Convert.ToDateTime(row["NgayNhanPhong"]).ToString("dd/MM/yyyy")})";
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

        // Tính tổng tiền dịch vụ + tiền phòng cho 1 đặt phòng
        public decimal TinhTongTienTheoMaDatPhong(int maDatPhong)
        {
            // Tổng tiền dịch vụ
            string queryDV = @"SELECT ISNULL(SUM(DonGia * SoLuong), 0) 
                               FROM DatPhong_DichVu 
                               WHERE MaDatPhong = @MaDatPhong";
            var param = new SqlParameter("@MaDatPhong", maDatPhong);
            var resultDV = connDb.ExecuteQuery(queryDV, new[] { param });
            decimal tongDichVu = resultDV.Rows.Count > 0 ? Convert.ToDecimal(resultDV.Rows[0][0]) : 0;

            // Lấy tiền phòng từ DatPhong + LoaiPhong
            string queryPhong = @"
                SELECT lp.GiaCoBan
                FROM DatPhong dp
                JOIN Phong p ON dp.MaPhong = p.MaPhong
                JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
                WHERE dp.MaDatPhong = @MaDatPhong";
            var resultPhong = connDb.ExecuteQuery(queryPhong, new[] { param });
            decimal tienPhong = resultPhong.Rows.Count > 0 ? Convert.ToDecimal(resultPhong.Rows[0][0]) : 0;

            return tongDichVu + tienPhong;
        }
    }
}
