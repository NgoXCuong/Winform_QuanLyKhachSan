using System;

namespace QuanLyKhachSan.Models
{
    public class DatPhongDichVuModel
    {
        public int MaDPDV { get; set; }
        public int MaDatPhong { get; set; }
        public int MaDV { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public DateTime NgaySuDung { get; set; }
    }
}
