using System;

namespace QuanLyKhachSan.Models
{
    public class KhachHangModel
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; } // Địa chỉ khách hàng
        public string CCCD { get; set; } // Thay CMND bằng CCCD
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
