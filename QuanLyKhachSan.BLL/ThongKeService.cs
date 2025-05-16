
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DAL;

namespace QuanLyKhachSan.BLL
{
    public class ThongKeService
    {
        private readonly ThongKeRepository thongKeRepository = new ThongKeRepository();
        
        public decimal GetTongDoanhThu()
        {
            return thongKeRepository.GetTongDoanhThu();
        }
        public int GetSoKhach()
        {
            return thongKeRepository.GetSoKhach();
        }
        public int GetSoPhong()
        {
            return thongKeRepository.GetSoPhong();
        }
        public int GetSoNhanVien()
        {
            return thongKeRepository.GetSoNhanVien();
        }
    }
}
