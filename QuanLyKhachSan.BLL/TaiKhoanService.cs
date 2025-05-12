using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return taiKhoanRepository.getAllTaiKhoan();
        }
        public List<TaiKhoanModel> GetTaiKhoanLogin()
        {
            return taiKhoanRepository.getTaiKhoanLogin();
        }

        public bool ThemTaiKhoan(TaiKhoanModel tk)
        {
            return taiKhoanRepository.ThemTaiKhoan(tk);
        }

        public bool XoaTaiKhoan(int maNhanVien)
        {
            return taiKhoanRepository.XoaTaiKhoan(maNhanVien);
        }

        public bool SuaTaiKhoan(TaiKhoanModel tk)
        {
            return taiKhoanRepository.SuaTaiKhoan(tk);
        }

        public bool KiemTraTonTaiMaNV(int maNV)
        {
            return taiKhoanRepository.KiemTraTonTaiMaNV(maNV);
        }

    }
}
