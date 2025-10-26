using System;

namespace QuanLyKhachSan.Models
{
    public class HoaDonModel
    {
        public int MaHD { get; set; }
        public int MaDatPhong { get; set; }
        public int MaKH { get; set; }
        public decimal TienPhong { get; set; }
        public decimal TongTienThanhToan { get; set; }
        public string TrangThaiThanhToan { get; set; } // "Chưa thanh toán", "Đã thanh toán", ...
        public string PhuongThucThanhToan { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
