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

        public List<DatPhongModel> GetAllDatPhong() => _repository.GetAllDatPhong();

        public DatPhongModel GetDatPhongById(int maDatPhong)
        {
            if (maDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");
            return _repository.GetDatPhongById(maDatPhong);
        }

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

        public bool SuaDatPhong(DatPhongModel dp, List<DatPhongDichVuModel> dsDichVu = null)
        {
            if (dp == null || dp.MaDatPhong <= 0)
                throw new ArgumentException("Dữ liệu đặt phòng không hợp lệ!");

            try
            {
                Console.WriteLine("🧩 B1: Validate dữ liệu...");
                ValidateDatPhong(dp);

                Console.WriteLine("🧩 B2: Gọi UpdateDatPhong...");
                bool result = _repository.UpdateDatPhong(dp);
                Console.WriteLine("🔍 Kết quả UpdateDatPhong = " + result);

                if (!result)
                    return false;

                if (dsDichVu != null)
                {
                    Console.WriteLine("🧩 B3: Cập nhật danh sách dịch vụ...");
                    _repository.CapNhatDichVuChoDatPhong(dp.MaDatPhong, dsDichVu);
                }

                Console.WriteLine("🧩 B4: Cập nhật tổng tiền...");
                dp.TongTien = TinhTongTien(dp);
                _repository.UpdateTongTien(dp.MaDatPhong, dp.TongTien);

                if (!string.IsNullOrWhiteSpace(dp.TrangThai))
                {
                    Console.WriteLine("🧩 B5: Cập nhật trạng thái phòng...");
                    _repository.CapNhatTrangThaiPhong(dp.MaPhong, dp.TrangThai);
                }

                Console.WriteLine("✅ B6: Hoàn tất cập nhật!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 Lỗi trong SuaDatPhong: " + ex.Message);
                throw; // hoặc return false;
            }
        }

        public bool CapNhatDichVu(int maDatPhong, List<DatPhongDichVuModel> danhSachDichVu)
        {
            return _repository.CapNhatDichVuChoDatPhong(maDatPhong, danhSachDichVu);
        }

        public bool XoaDatPhong(int maDatPhong)
        {
            if (maDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");

            var dsDichVu = _repository.GetDichVuByDatPhong(maDatPhong);
            foreach (var dv in dsDichVu)
            {
                _repository.DeleteChiTietDichVu(dv.MaDPDV);
            }

            return _repository.DeleteDatPhong(maDatPhong);
        }

        public bool ThemChiTietDichVu(DatPhongDichVuModel ctdv)
        {
            if (ctdv.MaDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");

            if (ctdv.MaDV <= 0)
                throw new ArgumentException("Chưa chọn dịch vụ.");

            if (ctdv.SoLuong <= 0)
                throw new ArgumentException("Số lượng dịch vụ phải lớn hơn 0.");

            bool result = _repository.InsertChiTietDichVu(ctdv);

            if (result)
            {
                // Sau khi thêm dịch vụ, cập nhật lại tổng tiền đặt phòng
                var dp = _repository.GetDatPhongById(ctdv.MaDatPhong);
                dp.TongTien = TinhTongTien(dp);
                _repository.UpdateTongTien(dp.MaDatPhong, dp.TongTien);
            }

            return result;
        }

        public bool CapNhatTrangThai(int maDatPhong, string trangThaiMoi)
        {
            if (maDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");

            if (string.IsNullOrWhiteSpace(trangThaiMoi))
                throw new ArgumentException("Trạng thái mới không hợp lệ.");

            return _repository.UpdateTrangThai(maDatPhong, trangThaiMoi);
        }


        private decimal TinhTongTien(DatPhongModel dp)
        {
            decimal giaPhong = _repository.GetGiaPhong(dp.MaPhong);
            int soNgay = (int)(dp.NgayTraPhong - dp.NgayNhanPhong).TotalDays;
            if (soNgay <= 0) soNgay = 1;

            decimal tongTien = giaPhong * soNgay;

            if (dp.SoNguoi > 2)
                tongTien += (dp.SoNguoi - 2) * 100000;

            var danhSachDichVu = _repository.GetDichVuByDatPhong(dp.MaDatPhong);

            if (danhSachDichVu != null && danhSachDichVu.Count > 0)
            {
                foreach (var dv in danhSachDichVu)
                {
                    // mỗi DatPhongDichVuModel có DonGia và SoLuong
                    tongTien += dv.DonGia * dv.SoLuong;
                }
            }
            return tongTien;
        }

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

        public List<DatPhongDichVuModel> GetDichVuByDatPhong(int maDatPhong)
        {
            if (maDatPhong <= 0)
                throw new ArgumentException("Mã đặt phòng không hợp lệ.");
            return _repository.GetDichVuByDatPhong(maDatPhong);
        }
    }
}
