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
        public bool InsertBookingRoom(BookingRoomModel bookingRoom)
        {
            return bookingRoomRepository.insertBookingRoom(bookingRoom);
        }
    }
}
