using System;

namespace QuanLyKhachSan.Models
{
    public class LoaiPhongModel
    {
        public int MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public decimal GiaCoBan { get; set; }
        public int SucChuaToiDa { get; set; }
        public string MoTa { get; set; }

        public string HienThiMaVaTen => $"{MaLoaiPhong} - {TenLoaiPhong}";
    }
}
