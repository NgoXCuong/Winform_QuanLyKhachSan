using System;

namespace QuanLyKhachSan.Models
{
    public class PhongModel
    {
        //public int MaPhong { get; set; }
        //public string SoPhong { get; set; }
        //public int MaLoaiPhong { get; set; }
        //public string TenLoaiPhong { get; set; }
        //public int Tang { get; set; }
        //public string TrangThai { get; set; } // "Trống", "Có khách", "Bảo trì"
        //public byte[] Anh { get; set; }
        public int MaPhong { get; set; }
        public string SoPhong { get; set; }
        public int MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public decimal GiaPhong { get; set; }
        public int SucChuaToiDa { get; set; }
        public int Tang { get; set; }
        public string TrangThai { get; set; }
        public byte[] Anh { get; set; }

    }
}
