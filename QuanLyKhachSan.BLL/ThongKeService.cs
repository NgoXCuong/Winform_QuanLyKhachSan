
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
        public Dictionary<DateTime, decimal> GetDoanhThuTheoNgay()
        {
            return thongKeRepository.GetDoanhThuTheoNgay();
        }

        public Dictionary<int, decimal> GetDoanhThuTheoThang()
        {
            return thongKeRepository.GetDoanhThuTheoThang();
        }
     
        public Dictionary<int, decimal> GetDoanhThuTheoNam()
        {
            return thongKeRepository.GetDoanhThuTheoNam();
        }

        // Trong class ThongKeService
        public decimal GetTongTienPhong()
        {
            return thongKeRepository.GetTongTienPhong(); // _repo là đối tượng ThongKeRepository đã khai báo
        }

        public decimal GetTongTienDichVu()
        {
            return thongKeRepository.GetTongTienDichVu();
        }
    }
}
