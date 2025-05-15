using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.BLL
{
    public class BookingRoomService
    {
        private readonly BookingRoomRepository bookingRoomRepository;

        public BookingRoomService()
        {
            bookingRoomRepository = new BookingRoomRepository();
        }
        public List<BookingRoomModel> GetAllDichVu()
        {
            return bookingRoomRepository.getDichVu();
        }

        public List<BookingRoomModel> GetAllPhongDat()
        {
            return bookingRoomRepository.GetAllPhongDat();
        }
        public decimal GetGiaPhongTheoSoPhong(int soPhong)
        {
            return bookingRoomRepository.GetGiaPhongTheoSoPhong(soPhong);
        }

        public bool InsertBookingRoom(BookingRoomModel bookingRoom)
        {
            return bookingRoomRepository.insertBookingRoom(bookingRoom);
        }
        public bool CapNhatTrangThaiPhong_DaDat(BookingRoomModel bookingRoom)
        {
            return bookingRoomRepository.CapNhatTrangThaiPhong_DaDat(bookingRoom);
        }
        public bool CapNhatTrangThaiPhong_Trong(BookingRoomModel bookingRoom)
        {
            return bookingRoomRepository.CapNhatTrangThaiPhong_Trong(bookingRoom);
        }

        public bool HuyDatPhong(BookingRoomModel bookingRoom)
        {
            return bookingRoomRepository.HuyDatPhong(bookingRoom);
        }

        public bool HuyDichVuTheoSoPhongVaMaDV(BookingRoomModel bookingRoom)
        {
            return bookingRoomRepository.HuyDichVuTheoSoPhongVaMaDV(bookingRoom);
        }


    }
}
