using System;

namespace QuanLyKhachSan.Models
{
    public class DatPhongModel
    {
        public int MaDatPhong { get; set; }
        public int MaKH { get; set; }
        public int MaPhong { get; set; }
        public int? MaNV { get; set; }
        public DateTime NgayNhanPhong { get; set; }
        public DateTime NgayTraPhong { get; set; }
        public int SoNguoi { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; } // "Chờ xác nhận", "Đã xác nhận", ...
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
