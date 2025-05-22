using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class HoaDonService
    {
        private HoaDonRepository repository = new HoaDonRepository();

        public List<HoaDonModel> LayTatCaHoaDon()
        {
            return repository.GetAll();
        }

        public HoaDonModel TimHoaDon(int maHoaDon)
        {
            return repository.GetById(maHoaDon);
        }

        public void Them(HoaDonModel model)
        {
            repository.Add(model);
        }

        public void CapNhat(HoaDonModel model)
        {
            repository.Update(model);
        }

        public void Xoa(int maHoaDon)
        {
            repository.Delete(maHoaDon);
        }


        // Trong HoaDonService.cs (BLL)

        public List<KeyValuePair<string, string>> GetDanhSachKhachHang()
        {
            var repo = new KhachHangRepository();
            return repo.GetAllKhachHang().Select(kh =>
                new KeyValuePair<string, string>(kh.MaKH.ToString(), kh.HoTen)).ToList();
        }

        public List<KeyValuePair<string, string>> GetDanhSachNhanVien()
        {
            var repo = new NhanVienRepository();
            return repo.getAllNhanVien().Select(nv =>
                new KeyValuePair<string, string>(nv.MaNV.ToString(), nv.HoTen)).ToList();
        }
    }


}

