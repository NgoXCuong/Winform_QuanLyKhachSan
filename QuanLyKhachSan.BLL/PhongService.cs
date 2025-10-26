using System;
using System.Collections.Generic;
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

        // Lấy tất cả phòng
        public List<PhongModel> GetAllPhong()
        {
            return phongRepository.GetAllPhong();
        }

        // Thêm phòng
        public bool ThemPhong(PhongModel phong)
        {
            return phongRepository.ThemPhong(phong);
        }

        // Sửa phòng
        public bool SuaPhong(PhongModel phong)
        {
            return phongRepository.SuaPhong(phong);
        }

        // Xóa phòng
        public bool XoaPhong(int maPhong)
        {
            return phongRepository.XoaPhong(maPhong);
        }

        // Tìm phòng theo từ khóa
        public List<PhongModel> TimPhong(string keyword)
        {
            return phongRepository.TimPhong(keyword);
        }

        // Kiểm tra số phòng tồn tại
        public bool KiemTraSoPhongTonTai(string soPhong)
        {
            return phongRepository.KiemTraSoPhongTonTai(soPhong);
        }
    }
}
