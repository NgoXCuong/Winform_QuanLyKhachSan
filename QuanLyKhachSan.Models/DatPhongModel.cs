using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models
{
    public class DatPhongModel
    {
        public int MaDatPhong { get; set; }
        public int MaKhachHang { get; set; }
        public int MaPhong { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTra { get; set; }
        public string TrangThai { get; set; }
    }
}
