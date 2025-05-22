using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class HoaDonService
    {
        private readonly HoaDonRepository hoaDonRepo = new HoaDonRepository();
        private readonly KhachHangRepository khachHangRepo = new KhachHangRepository();
        private readonly NhanVienRepository nhanVienRepo = new NhanVienRepository();

        // Lấy tất cả hóa đơn
        public List<HoaDonModel> LayTatCaHoaDon()
        {
            return hoaDonRepo.GetAll();
        }

        // Tìm hóa đơn theo mã
        public HoaDonModel TimHoaDonTheoMa(int maHoaDon)
        {
            return hoaDonRepo.GetById(maHoaDon);
        }

        // Thêm hóa đơn mới
        public void ThemHoaDon(HoaDonModel hoaDon)
        {
            hoaDonRepo.Add(hoaDon);
        }

        // Cập nhật hóa đơn
        public void CapNhatHoaDon(HoaDonModel hoaDon)
        {
            hoaDonRepo.Update(hoaDon);
        }

        // Xóa hóa đơn
        public void XoaHoaDon(int maHoaDon)
        {
            hoaDonRepo.Delete(maHoaDon);
        }

        // Lấy danh sách khách hàng (dạng KeyValuePair)
        public List<KeyValuePair<string, string>> LayDanhSachKhachHang()
        {
            return khachHangRepo.GetAllKhachHang()
                .Select(kh => new KeyValuePair<string, string>(kh.MaKH.ToString(), kh.HoTen))
                .ToList();
        }

        // Lấy danh sách nhân viên (dạng KeyValuePair)
        public List<KeyValuePair<string, string>> LayDanhSachNhanVien()
        {
            return nhanVienRepo.getAllNhanVien()
                .Select(nv => new KeyValuePair<string, string>(nv.MaNV.ToString(), nv.HoTen))
                .ToList();
        }
    }
}
