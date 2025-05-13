using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<DatPhongModel> GetAllDatPhong()
        {
            return datPhongRepository.GetAllDatPhong();
        }

    }
}
