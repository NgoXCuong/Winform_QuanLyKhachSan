using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class HoaDonRepository
    {
        private readonly string connectionString = @"Data Source=LAPTOP-1FBOO2FR;Initial Catalog=BTL_QuanLyKhachSan;Integrated Security=True";

        public List<HoaDonModel> GetAll()
        {
            var list = new List<HoaDonModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
    SELECT 
        hd.MaHoaDon, 
        hd.MaDatPhong, 
        hd.NgayLap, 
        hd.TongTien,
        kh.HoTen AS TenKhachHang,
        nv.HoTen AS TenNhanVien
    FROM HoaDon hd
    JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    JOIN NhanVien nv ON dp.MaNV = nv.MaNV";


                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine(reader.GetName(i)); // hoặc log ra ListBox nếu dùng WinForms
                }

                while (reader.Read())
                {
                    list.Add(new HoaDonModel
                    {
                        MaHoaDon = Convert.ToInt32(reader["MaHoaDon"]),
                        MaDatPhong = Convert.ToInt32(reader["MaDatPhong"]),
                        NgayLap = Convert.ToDateTime(reader["NgayLap"]),
                        KhachHang = reader["TenKhachHang"].ToString(),
                        NhanVien = reader["TenNhanVien"].ToString(),
                        TongTien = Convert.ToDecimal(reader["TongTien"])
                    });
                }
            }
            return list;
        }

        public HoaDonModel GetById(int maHoaDon)
        {
            HoaDonModel hd = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT hd.MaHoaDon, hd.MaDatPhong, hd.NgayLap, 
                       kh.HoTen AS TenKhachHang, nv.HoTen, hd.TongTien
                FROM HoaDon hd
                JOIN DatPhong dp ON hd.MaDatPhong = dp.MaDatPhong
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                JOIN NhanVien nv ON dp.MaNV = nv.MaNV
                WHERE hd.MaHoaDon = @MaHoaDon";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hd = new HoaDonModel
                    {
                        MaHoaDon = Convert.ToInt32(reader["MaHoaDon"]),
                        MaDatPhong = Convert.ToInt32(reader["MaDatPhong"]),
                        NgayLap = Convert.ToDateTime(reader["NgayLap"]),
                        KhachHang = reader["HoTen"].ToString(),
                        NhanVien = reader["HoTen"].ToString(),
                        TongTien = Convert.ToDecimal(reader["TongTien"])
                    };
                }
            }
            return hd;
        }


        public void Add(HoaDonModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO HoaDon (MaDatPhong, NgayLap, TongTien)
                                     VALUES (@MaDatPhong, @NgayLap, @TongTien)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDatPhong", model.MaDatPhong);
                cmd.Parameters.AddWithValue("@NgayLap", model.NgayLap);
                cmd.Parameters.AddWithValue("@TongTien", model.TongTien);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(HoaDonModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE HoaDon 
                                     SET MaDatPhong = @MaDatPhong, NgayLap = @NgayLap, TongTien = @TongTien
                                     WHERE MaHoaDon = @MaHoaDon";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", model.MaHoaDon);
                cmd.Parameters.AddWithValue("@MaDatPhong", model.MaDatPhong);
                cmd.Parameters.AddWithValue("@NgayLap", model.NgayLap);
                cmd.Parameters.AddWithValue("@TongTien", model.TongTien);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int maHoaDon)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<KeyValuePair<int, string>> GetDanSachMaDatPhong()
        {
            var list = new List<KeyValuePair<int, string>>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    dp.MaDatPhong, 
                    kh.HoTen AS TenKhachHang, 
                    dp.NgayDat 
                FROM DatPhong dp
                JOIN KhachHang kh ON dp.MaKH = kh.MaKH";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int maDatPhong = Convert.ToInt32(reader["MaDatPhong"]);
                    string display = $"{reader["TenKhachHang"]} - {Convert.ToDateTime(reader["NgayDat"]).ToString("dd/MM/yyyy")}";
                    list.Add(new KeyValuePair<int, string>(maDatPhong, display));
                }
            }
            return list;
        }
        // Trả về List<KeyValuePair<int, string>>
        public List<KeyValuePair<int, string>> GetDanhSachMaDatPhong()
        {
            var list = new List<KeyValuePair<int, string>>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT dp.MaDatPhong, kh.HoTen AS TenKhachHang, dp.NgayDat
            FROM DatPhong dp
            JOIN KhachHang kh ON dp.MaKH = kh.MaKH";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int maDatPhong = Convert.ToInt32(reader["MaDatPhong"]);
                    string display = $"{reader["TenKhachHang"]} - {Convert.ToDateTime(reader["NgayDat"]).ToString("dd/MM/yyyy")}";
                    list.Add(new KeyValuePair<int, string>(maDatPhong, display));
                }
            }
            return list;
        }
        public List<KeyValuePair<int, string>> GetDanhSachKhachHang()
        {
            var list = new List<KeyValuePair<int, string>>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaKH, HoTen FROM KhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int maKH = Convert.ToInt32(reader["MaKH"]);
                    string hoTen = reader["HoTen"].ToString();
                    list.Add(new KeyValuePair<int, string>(maKH, hoTen));
                }
            }
            return list;
        }
        public List<KeyValuePair<int, string>> GetDanhSachNhanVien()
        {
            var list = new List<KeyValuePair<int, string>>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaNV, HoTen FROM NhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int maNV = Convert.ToInt32(reader["MaNV"]);
                    string hoTen = reader["HoTen"].ToString();
                    list.Add(new KeyValuePair<int, string>(maNV, hoTen));
                }
            }
            return list;
        }
    }
}
