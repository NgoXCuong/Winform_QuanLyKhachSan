using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models; // Sử dụng Namespace chứa Model mới

namespace QuanLyKhachSan.BLL
{
    public class HoaDonService
    {
        private readonly HoaDonRepository _repo = new HoaDonRepository();

        public List<HoaDonModel> GetAll() => _repo.GetAll();

        // Thêm vào trong class HoaDonService
        public HoaDonModel TimHoaDonTheoMa(int maHD)
        {
            // Gọi hàm GetById vừa viết ở trên
            return _repo.GetById(maHD);
        }
        public ThongTinThanhToanDTO TinhToanHoaDon(int maDatPhong) => _repo.GetThongTinTuDatPhong(maDatPhong);
        public DataTable GetDichVuSuDung(int maDatPhong) => _repo.GetDichVuByDatPhong(maDatPhong);
        public bool Add(HoaDonModel hd) => _repo.Add(hd);
        public bool Update(HoaDonModel hd) => _repo.Update(hd);
        public bool Delete(int maHD) => _repo.Delete(maHD);
        // Thêm vào class HoaDonService
        public DataTable LayDanhSachDatPhong()
        {
            return _repo.GetDanhSachDatPhongChoComboBox();
        }
    }
}