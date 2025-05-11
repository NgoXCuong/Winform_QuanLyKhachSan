using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models
{
    public class NhanVienModel
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public byte[] Anh { get; set; }

        // Thuộc tính này để hiển thị mã nhân viên và tên nhân viên
        public string HienThiMaVaTen
        {
            get
            {
                return $"{MaNV} - {HoTen}";
            }
        }
    }

}
