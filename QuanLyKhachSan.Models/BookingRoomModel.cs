using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models
{
    public class BookingRoomModel
    {
        public int MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public int SoPhong { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTra { get; set; }

        public int MaKH { get; set; }
        public int MaNV { get; set; }
        public int MaDatPhong { get; set; }
        public int MaPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public string TrangThai { get; set; }
        public decimal GiaPhong { get; set; }
    }

}
