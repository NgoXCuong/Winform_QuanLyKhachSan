using System;
using System.Collections.Generic;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class DatPhongService
    {
        private readonly DatPhongRepository datPhongRepository;

        public DatPhongService()
        {
            datPhongRepository = new DatPhongRepository();
        }

        // Lấy tất cả đặt phòng
        public List<DatPhongModel> GetAllDatPhong()
        {
            return datPhongRepository.GetAllDatPhong();
        }

        // Lấy đặt phòng theo ID
        public DatPhongModel GetDatPhongById(int maDatPhong)
        {
            return datPhongRepository.GetDatPhongById(maDatPhong);
        }

        // Thêm mới đặt phòng
        public bool InsertDatPhong(DatPhongModel dp)
        {
            return datPhongRepository.InsertDatPhong(dp);
        }

        // Cập nhật thông tin đặt phòng
        public bool UpdateDatPhong(DatPhongModel dp)
        {
            return datPhongRepository.UpdateDatPhong(dp);
        }

        // Xóa đặt phòng
        public bool DeleteDatPhong(int maDatPhong)
        {
            return datPhongRepository.DeleteDatPhong(maDatPhong);
        }
    }
}
