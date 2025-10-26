using System;

namespace QuanLyKhachSan.Models
{
    public class KhachHangModel
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CCCD { get; set; } // Thay CMND bằng CCCD theo database
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
