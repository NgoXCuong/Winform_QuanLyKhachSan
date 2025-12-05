using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyKhachSan.BLL
{
    public class TaiKhoanService
    {
        private readonly TaiKhoanRepository taiKhoanRepository;

        public TaiKhoanService()
        {
            taiKhoanRepository = new TaiKhoanRepository();
        }

        public List<TaiKhoanModel> GetAllTaiKhoan()
        {
            return taiKhoanRepository.GetAllTaiKhoan();
        }

        public List<TaiKhoanModel> GetTaiKhoanAdmin()
        {
            return taiKhoanRepository.GetTaiKhoanAdmin();
        }

        public bool ThemTaiKhoan(TaiKhoanModel tk)
        {
            return taiKhoanRepository.ThemTaiKhoan(tk);
        }

        public bool XoaTaiKhoan(string tenDangNhap)
        {
            return taiKhoanRepository.XoaTaiKhoan(tenDangNhap);
        }

        public bool SuaTaiKhoan(TaiKhoanModel tk)
        {
            return taiKhoanRepository.SuaTaiKhoan(tk);
        }

        public List<TaiKhoanModel> TimKiemTaiKhoan(string keyword)
        {
            return taiKhoanRepository.TimKiemTaiKhoan(keyword);
        }

        public bool KiemTraTonTaiMaNV(int maNV)
        {
            return taiKhoanRepository.KiemTraTonTaiMaNV(maNV);
        }

        // ✅ Sửa lại đăng nhập
        public TaiKhoanModel GetTaiKhoanByTenDangNhap(string tenDangNhap, string matKhau)
        {
            // Gọi trực tiếp phương thức DangNhap từ repository
            return taiKhoanRepository.DangNhap(tenDangNhap, matKhau);
        }

        // ✅ Lấy tài khoản theo email
        //public TaiKhoanModel GetTaiKhoanByEmail(string email)
        //{
        //    return taiKhoanRepository.GetTaiKhoanByEmail(email);
        //}

    }
}
