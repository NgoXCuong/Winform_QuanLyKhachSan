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
    }
}
