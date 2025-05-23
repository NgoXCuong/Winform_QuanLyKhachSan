using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models
{
    public class HoaDonModel
    {
        public int MaHoaDon { get; set; }
        public int MaDatPhong { get; set; }
        public DateTime NgayLap { get; set; }
        public string KhachHang { get; set; }
        public string NhanVien { get; set; }
        public decimal TongTien { get; set; }
        public object MaKhachHang { get; set; }
        public object MaNhanVien { get; set; }
    }
}
