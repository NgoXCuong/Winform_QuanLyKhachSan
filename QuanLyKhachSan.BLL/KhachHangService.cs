using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class KhachHangService
    {
        private readonly KhachHangRepository khachHangRepository;

        public KhachHangService()
        {
            khachHangRepository = new KhachHangRepository();
        }

        public List<KhachHangModel> GetAllKhachHang()
        {
            return khachHangRepository.GetAllKhachHang();
        }

        public bool ThemKhachHang(KhachHangModel kh)
        {
            return khachHangRepository.ThemKhachHang(kh);
        }

        public bool SuaKhachHang(KhachHangModel kh)
        {
            return khachHangRepository.SuaKhachHang(kh);
        }

        public bool XoaKhachHang(int maKH)
        {
            return khachHangRepository.XoaKhachHang(maKH);
        }

        public List<KhachHangModel> TimKiemKhachHang(string keyword)
        {
            return khachHangRepository.TimKhachHang(keyword);
        }

        public KhachHangModel LayThongTinKhachHang(int maKH)
        {
            return khachHangRepository.GetById(maKH);
        }
    }
}
