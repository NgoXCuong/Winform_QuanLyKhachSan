using System;

namespace QuanLyKhachSan.Models
{
    public class NhanVienModel
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public byte[] Anh { get; set; }
        public string TrangThai { get; set; } // "Đang làm" / "Nghỉ việc"

        public string HienThiMaVaTen => $"{MaNV} - {HoTen}";
    }
}
