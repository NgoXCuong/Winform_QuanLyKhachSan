namespace QuanLyKhachSan.Models
{
    public class TaiKhoanModel
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        // Email được lấy từ bảng NhanVien qua JOIN
        public int MaNV { get; set; }
        public string VaiTro { get; set; } // "Quản trị" / "Nhân viên"
        public string TrangThai { get; set; } // "Hoạt động" / "Ngưng hoạt động"
        // Thuộc tính để lưu email từ JOIN (không lưu trong DB)
        public string Email { get; set; }
    }
}