using System;
using System.Collections.Generic;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class DatPhongService
    {
        private readonly DatPhongRepository _repository;

        public DatPhongService()
        {
            _repository = new DatPhongRepository();
        }

        // =============================
        // LẤY DỮ LIỆU
        // =============================
        public List<DatPhongModel> GetAllDatPhong() => _repository.GetAllDatPhong();

        public DatPhongModel GetDatPhongById(int maDatPhong)
        {
            if (maDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");
            return _repository.GetDatPhongById(maDatPhong);
        }

        // =============================
        // THÊM ĐẶT PHÒNG
        // =============================
        public int ThemDatPhong(DatPhongModel dp, List<DatPhongDichVuModel> dsDichVu = null)
        {
            ValidateDatPhong(dp);

            dp.TongTien = TinhTongTien(dp);
            dp.TrangThai = string.IsNullOrEmpty(dp.TrangThai) ? "Đang đặt" : dp.TrangThai;
            dp.NgayTao = DateTime.Now;

            int maDatPhongMoi = _repository.InsertDatPhong(dp);

            if (maDatPhongMoi > 0)
            {
                if (dsDichVu != null && dsDichVu.Count > 0)
                {
                    foreach (var dv in dsDichVu)
                    {
                        dv.MaDatPhong = maDatPhongMoi;
                        _repository.InsertChiTietDichVu(dv);
                    }
                }

                _repository.CapNhatTrangThaiPhong(dp.MaPhong, "Đang thuê");
            }

            return maDatPhongMoi;
        }

        // =============================
        // SỬA ĐẶT PHÒNG
        // =============================
        public bool SuaDatPhong(DatPhongModel dp)
        {
            if (dp == null || dp.MaDatPhong <= 0)
                throw new ArgumentException("Đặt phòng không hợp lệ.");

            ValidateDatPhong(dp); // Kiểm tra các trường hợp hợp lệ

            // Cập nhật dữ liệu trong DB
            return _repository.UpdateDatPhong(dp);
        }

        // =============================
        // XÓA ĐẶT PHÒNG (tự động xóa chi tiết dịch vụ)
        // =============================
        public bool XoaDatPhong(int maDatPhong)
        {
            if (maDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");

            // Xóa chi tiết dịch vụ trước
            var dsDichVu = _repository.GetDichVuByDatPhong(maDatPhong);
            foreach (var dv in dsDichVu)
            {
                _repository.DeleteChiTietDichVu(dv.MaDPDV);
            }

            // Xóa đặt phòng chính
            return _repository.DeleteDatPhong(maDatPhong);
        }


        // =============================
        // THÊM CHI TIẾT DỊCH VỤ
        // =============================
        public bool ThemChiTietDichVu(DatPhongDichVuModel ctdv)
        {
            if (ctdv.MaDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");

            if (ctdv.MaDV <= 0)
                throw new ArgumentException("Chưa chọn dịch vụ.");

            if (ctdv.SoLuong <= 0)
                throw new ArgumentException("Số lượng dịch vụ phải lớn hơn 0.");

            return _repository.InsertChiTietDichVu(ctdv);
        }

        // =============================
        // CẬP NHẬT TRẠNG THÁI ĐẶT PHÒNG
        // =============================
        public bool CapNhatTrangThai(int maDatPhong, string trangThaiMoi)
        {
            if (maDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");

            if (string.IsNullOrWhiteSpace(trangThaiMoi))
                throw new ArgumentException("Trạng thái mới không hợp lệ.");

            return _repository.UpdateTrangThai(maDatPhong, trangThaiMoi);
        }

        // =============================
        // TÍNH TỔNG TIỀN
        // =============================
        private decimal TinhTongTien(DatPhongModel dp)
        {
            decimal giaPhong = _repository.GetGiaPhong(dp.MaPhong);
            int soNgay = (int)(dp.NgayTraPhong - dp.NgayNhanPhong).TotalDays;
            if (soNgay <= 0) soNgay = 1;

            decimal tongTien = giaPhong * soNgay;

            // Phụ thu nếu nhiều hơn 2 người
            if (dp.SoNguoi > 2)
                tongTien += (dp.SoNguoi - 2) * 100000;

            return tongTien;
        }

        // =============================
        // KIỂM TRA HỢP LỆ DỮ LIỆU
        // =============================
        private void ValidateDatPhong(DatPhongModel dp)
        {
            if (dp == null)
                throw new ArgumentNullException(nameof(dp), "Đối tượng đặt phòng không được null.");

            if (dp.MaKH <= 0)
                throw new ArgumentException("Chưa chọn khách hàng.");

            if (dp.MaPhong <= 0)
                throw new ArgumentException("Chưa chọn phòng.");

            if (dp.NgayTraPhong <= dp.NgayNhanPhong)
                throw new ArgumentException("Ngày trả phòng phải sau ngày nhận phòng.");

            if (dp.SoNguoi <= 0)
                throw new ArgumentException("Số người phải lớn hơn 0.");
        }
    }
}
