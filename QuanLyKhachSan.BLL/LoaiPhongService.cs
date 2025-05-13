using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class LoaiPhongService
    {
        private readonly LoaiPhongRepository loaiPhongRepository;

        public LoaiPhongService()
        {
            loaiPhongRepository = new LoaiPhongRepository();
        }

        public List<LoaiPhongModel> GetAllLoaiPhong()
        {
            return loaiPhongRepository.GetAllLoaiPhong();
        }

        public List<LoaiPhongModel> GetLoaiPhongByIdName()
        {
            return loaiPhongRepository.GetLoaiPhongByIdName();
        }

        public bool ThemLoaiPhong(LoaiPhongModel loaiPhong)
        {
            return loaiPhongRepository.ThemLoaiPhong(loaiPhong);
        }

        public bool XoaLoaiPhong(int maLoaiPhong)
        {
            return loaiPhongRepository.XoaLoaiPhong(maLoaiPhong);
        }

        public bool SuaLoaiPhong(LoaiPhongModel loaiPhong)
        {
            return loaiPhongRepository.SuaLoaiPhong(loaiPhong);
        }

        public List<LoaiPhongModel> TimLoaiPhong(string keyword)
        {
            return loaiPhongRepository.TimLoaiPhong(keyword);
        }
    }
}
