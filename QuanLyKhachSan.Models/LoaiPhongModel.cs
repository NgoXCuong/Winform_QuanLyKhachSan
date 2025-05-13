using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models
{
    public class LoaiPhongModel
    {
        public int MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public string MoTa { get; set; }
        public decimal GiaPhong { get; set; }
        public int SoNguoiToiDa { get; set; }

        // Thêm thuộc tính này để kết hợp mã và tên loại phòng
        public string HienThiMaVaTen
        {
            get
            {
                return $"{MaLoaiPhong} - {TenLoaiPhong}";
            }
            set
            {
            }
        }
    }

}
