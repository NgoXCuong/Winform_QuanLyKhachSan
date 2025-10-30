using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class PhongService
    {
        private readonly PhongRepository phongRepository;

        public PhongService()
        {
            phongRepository = new PhongRepository();
        }

        // 🧩 Lấy tất cả phòng
        public List<PhongModel> GetAllPhong()
        {
            return phongRepository.GetAllPhong();
        }

        // ➕ Thêm phòng
        public bool ThemPhong(PhongModel phong)
        {
            return phongRepository.ThemPhong(phong);
        }

        // ✏️ Sửa phòng
        public bool SuaPhong(PhongModel phong)
        {
            return phongRepository.SuaPhong(phong);
        }

        // 🗑️ Xóa phòng
        public bool XoaPhong(int maPhong)
        {
            return phongRepository.XoaPhong(maPhong);
        }

        // 🔍 Tìm phòng theo từ khóa
        public List<PhongModel> TimPhong(string keyword)
        {
            return phongRepository.TimPhong(keyword);
        }

        // 🏷️ Cập nhật trạng thái phòng (Trống / Đã đặt / Đang thuê)
        public bool CapNhatTrangThaiPhong(int maPhong, string trangThai)
        {
            if (maPhong <= 0 || string.IsNullOrWhiteSpace(trangThai))
                throw new ArgumentException("Mã phòng hoặc trạng thái không hợp lệ!");

            return phongRepository.CapNhatTrangThai(maPhong, trangThai);
        }


        // ✅ Kiểm tra số phòng tồn tại
        public bool KiemTraSoPhongTonTai(string soPhong)
        {
            return phongRepository.KiemTraSoPhongTonTai(soPhong);
        }

        // 🔎 Lấy phòng theo mã
        public PhongModel GetById(int maPhong)
        {
            return phongRepository.GetById(maPhong);
        }

        // 🖼️ Cập nhật ảnh phòng (base64 string)
        public bool CapNhatAnh(int maPhong, Image image)
        {
            if (image == null) return false;

            string base64Image = ConvertImageToBase64(image);
            return phongRepository.CapNhatAnh(maPhong, base64Image);
        }

        private string ConvertImageToBase64(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        // 🧾 Lấy ảnh phòng (trả về byte[])
        public string LayAnhPhong(int maPhong)
        {
            byte[] imageBytes = phongRepository.LayAnhPhong(maPhong);
            if (imageBytes != null && imageBytes.Length > 0)
                return Convert.ToBase64String(imageBytes);  // Base64 từ byte[]
            else
                return null;
        }

        // ❌ Xóa ảnh phòng
        public bool XoaAnhPhong(int maPhong)
        {
            return phongRepository.XoaAnhPhong(maPhong);
        }
    }
}
