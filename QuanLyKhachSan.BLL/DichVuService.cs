    using QuanLyKhachSan.DAL;
    using QuanLyKhachSan.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    namespace QuanLyKhachSan.BLL
    {
        public class DichVuService
        {
            private readonly DichVuRepository dichVuRepository;
            private readonly HoaDonRepository hoaDonRepo;

            public DichVuService()
            {
                dichVuRepository = new DichVuRepository();
                hoaDonRepo = new HoaDonRepository();
            }

            // Lấy toàn bộ danh sách dịch vụ
            public List<DichVuModel> GetAllDichVu()
            {
                return dichVuRepository.GetAllDichVu();
            }

            // Thêm dịch vụ mới
            public bool ThemDichVu(DichVuModel dv)
            {
                return dichVuRepository.ThemDichVu(dv);
            }

            // Sửa thông tin dịch vụ
            public bool SuaDichVu(DichVuModel dv)
            {
                return dichVuRepository.SuaDichVu(dv);
            }

            // Xóa dịch vụ
            public bool XoaDichVu(int maDV)
            {
                return dichVuRepository.XoaDichVu(maDV);
            }

            // Tìm kiếm dịch vụ
            public List<DichVuModel> TimKiemDichVu(string keyword)
            {
                return dichVuRepository.TimDichVu(keyword);
            }

            // Tính tổng tiền theo mã đặt phòng (dành cho hóa đơn)
            public decimal TinhTongTienTheoMaDatPhong(int maDatPhong)
            {
                return hoaDonRepo.TinhTongTienTheoMaDatPhong(maDatPhong);
            }

            public bool CapNhatAnh(int maDV, Image image)
            {
                if (image == null) return false;

                string base64Image = ConvertImageToBase64(image);
                return dichVuRepository.CapNhatAnh(maDV, base64Image);
            }

            public string LayAnhDichVu(int maDV)
            {
                byte[] imageBytes = dichVuRepository.LayAnhDichVu(maDV);
                if (imageBytes != null && imageBytes.Length > 0)
                    return Convert.ToBase64String(imageBytes);  // Base64 từ byte[]
                else
                    return null;
            }

            public bool XoaAnhDichVu(int maDV)
            {
                return dichVuRepository.XoaAnhDichVu(maDV);
            }

            // chuyển đổi một đối tượng hình ảnh (Image) thành chuỗi Base64
            private string ConvertImageToBase64(Image image)
            {
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
