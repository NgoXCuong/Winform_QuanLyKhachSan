using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class DichVuService
    {
        private DichVuRepository dichVuRepository = new DichVuRepository();
        private HoaDonRepository hoaDonRepo = new HoaDonRepository(); // Add this field to resolve the error

        public List<DichVuModel> GetAllDichVu()
        {
            return dichVuRepository.GetAllDichVu();
        }

        public bool ThemDichVu(DichVuModel dv)
        {
            return dichVuRepository.ThemDichVu(dv);
        }

        public bool SuaDichVu(DichVuModel dv)
        {
            return dichVuRepository.SuaDichVu(dv);
        }

        public bool XoaDichVu(int maDV)
        {
            return dichVuRepository.XoaDichVu(maDV);
        }

        public List<DichVuModel> TimKiemDichVu(string keyword)
        {
            return dichVuRepository.TimDichVu(keyword);
        }

        public decimal TinhTongTienTheoMaDatPhong(int maDatPhong)
        {
            return hoaDonRepo.TinhTongTienTheoMaDatPhong(maDatPhong);
        }
    }
}
