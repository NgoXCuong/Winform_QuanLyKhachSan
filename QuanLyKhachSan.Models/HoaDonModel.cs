using System;

namespace QuanLyKhachSan.Models
{
    public class HoaDonModel
    {
        //public int MaHD { get; set; }
        //public int MaDatPhong { get; set; }
        //public int MaKH { get; set; }
        //public decimal TienPhong { get; set; }
        //public decimal TongTienThanhToan { get; set; }
        //public string TrangThaiThanhToan { get; set; } // "Chưa thanh toán", "Đã thanh toán", ...
        //public string PhuongThucThanhToan { get; set; }
        //public DateTime? NgayThanhToan { get; set; }
        //public string GhiChu { get; set; }
        //public DateTime NgayTao { get; set; }
        // Thuộc tính gốc từ bảng HoaDon
        public int MaHD { get; set; }
        public int MaDatPhong { get; set; }
        public int MaKH { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TongTienThanhToan { get; set; }
        public string TrangThaiThanhToan { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }

        // Thuộc tính hiển thị (Lấy từ bảng JOIN)
        public string TenKhachHang { get; set; }
        public string SoPhong { get; set; }
        public decimal TienDichVu { get; set; } // Được tính toán
    }

    // Class DTO phụ dùng để trả về thông tin khi nhập Mã Đặt Phòng
    public class ThongTinThanhToanDTO
    {
        public int MaKH { get; set; }
        public string TenKhachHang { get; set; }
        public string SoPhong { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TienDichVu { get; set; }
        public decimal TongTien { get; set; }
    }
}
