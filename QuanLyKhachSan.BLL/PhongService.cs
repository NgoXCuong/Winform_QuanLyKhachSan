using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class PhongService
    {
        private readonly PhongRepository phongRepository;

        public PhongService()
        {
            phongRepository = new PhongRepository();
        }

        public List<PhongModel> GetAllPhong()
        {
            return phongRepository.getAllPhong();
        }

        public bool ThemPhong(PhongModel phong)
        {
            return phongRepository.ThemPhong(phong);
        }

        public bool SuaPhong(PhongModel phong)
        {
            return phongRepository.SuaPhong(phong);
        }

        public bool XoaPhong(int maPhong)
        {
            return phongRepository.XoaPhong(maPhong);
        }

        public List<PhongModel> TimPhong(string keyword)
        {
            return phongRepository.TimPhong(keyword);
        }

        public bool KiemTraSoPhongTonTai(int soPhong)
        {
            return phongRepository.KiemTraSoPhongTonTai(soPhong);
        }
    }
}
