using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace QuanLyKhachSan.BLL
{
    public class NhanVienService
    {
        private readonly NhanVienRepository nhanVienRepository;

        public NhanVienService()
        {
            nhanVienRepository = new NhanVienRepository();
        }

        public List<NhanVienModel> GetAllNhanVien()
        {
            return nhanVienRepository.GetAllNhanVien();
        }

        public bool ThemNhanVien(NhanVienModel nv)
        {
            return nhanVienRepository.ThemNhanVien(nv);
        }

        public bool XoaNhanVien(int maNV)
        {
            return nhanVienRepository.XoaNhanVien(maNV);
        }

        public bool SuaNhanVien(NhanVienModel nv)
        {
            return nhanVienRepository.SuaNhanVien(nv);
        }

        public List<NhanVienModel> TimNhanVien(string keyword)
        {
            return nhanVienRepository.TimNhanVien(keyword);
        }

        public bool CapNhatAnhNhanVien(int maNV, Image image)
        {
            if (image == null) return false;

            string base64Image = ConvertImageToBase64(image);
            return nhanVienRepository.CapNhatAnh(maNV, base64Image);
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

        public string LayAnhNhanVien(int maNV)
        {
            byte[] imageBytes = nhanVienRepository.LayAnhNhanVien(maNV);
            if (imageBytes != null && imageBytes.Length > 0)
                return Convert.ToBase64String(imageBytes);  // Base64 từ byte[]
            else
                return null;
        }

        public bool XoaAnhNhanVien(int maNV)
        {
            return nhanVienRepository.XoaAnhNhanVien(maNV);
        }

        public List<NhanVienModel> GetNhanVienByIdName()
        {
            return nhanVienRepository.GetNhanVienByIdName();
        }

        public string GetChucVuByMaNV(int maNV)
        {
            return nhanVienRepository.GetChucVuByMaNV(maNV);
        }

        public NhanVienModel LayThongTinNhanVien(int maNV)
        {
            return nhanVienRepository.GetById(maNV);
        }
    }
}
