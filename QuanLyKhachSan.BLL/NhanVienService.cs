using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.BLL
{
    public class NhanVienService
    {
        private readonly NhanVienRepository nhanVienRepository;

        public NhanVienService()
        {
            nhanVienRepository = new NhanVienRepository();
        }

        public List<NhanVienModel> GetAllNhanVien()
        {
            return nhanVienRepository.getAllNhanVien();
        }

        public bool ThemNhanVien(NhanVienModel nv)
        {
            return nhanVienRepository.ThemNhanVien(nv);
        }

        public bool XoaNhanVien(int maNV)
        {
            return nhanVienRepository.XoaNhanVien(maNV);
        }

        public bool SuaNhanVien(NhanVienModel nv)
        {
            return nhanVienRepository.SuaNhanVien(nv);
        }

        public List<NhanVienModel> TimNhanVien(string keyword)
        {
            return nhanVienRepository.TimNhanVien(keyword);
        }
    }
}
