using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan.UI
{
    public partial class BookingRoom : Form
    {
        private readonly PhongService phongService = new PhongService();

        public BookingRoom()
        {
            InitializeComponent();
        }

        private void LoadPhong(string trangThaiLoc = "")
        {
            lvPhong.View = View.LargeIcon;
            lvPhong.LargeImageList = imageList1;
            lvPhong.Items.Clear();

            List<PhongModel> danhSachPhong = phongService.GetAllPhong(); // Danh sách phòng từ CSDL

            foreach (var phong in danhSachPhong)
            {
                string trangThai = phong.TrangThai?.Trim().ToLower();

                // Bỏ qua nếu lọc và trạng thái không khớp
                if (!string.IsNullOrEmpty(trangThaiLoc) && trangThai != trangThaiLoc.ToLower())
                {
                    continue;
                }

                ListViewItem item = new ListViewItem($"Phòng {phong.SoPhong} ({phong.TrangThai})");

                // Chọn hình theo trạng thái
                switch (trangThai)
                {
                    case "trống":
                        item.ImageIndex = 0; // ảnh phòng trống
                        break;
                    case "đã đặt":
                        item.ImageIndex = 1; // ảnh phòng đã đặt
                        break;
                    case "đang ở":
                        item.ImageIndex = 2; // ảnh phòng đang ở
                        break;
                    case "bảo trì":
                        item.ImageIndex = 3; // ảnh bảo trì
                        break;
                    default:
                        item.ImageIndex = 4; // ảnh mặc định hoặc báo lỗi
                        break;
                }

                lvPhong.Items.Add(item);
            }
        }


        private void BookingRoom_Load(object sender, EventArgs e)
        {
            LoadPhong();
        }

        private void rbTrong_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTrong.Checked)
            {
                LoadPhong("trống"); // Chỉ hiển thị phòng trống
            }
        }

        private void rbDaDat_CheckedChanged(object sender, EventArgs e)
        {
            if(rbDaDat.Checked)
            {
                LoadPhong("đã đặt"); // Chỉ hiển thị phòng đã đặt
            }
        }

        private void rbTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if(rbTatCa.Checked)
            {
                LoadPhong(); // Hiển thị tất cả phòng
            }
        }

        private void rbDangO_CheckedChanged(object sender, EventArgs e)
        {
            if( rbDangO.Checked)
            {
                LoadPhong("đang ở"); // Chỉ hiển thị phòng đang ở
            }
        }

        private void rbBaoTri_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBaoTri.Checked)
            {
                LoadPhong("bảo trì"); // Chỉ hiển thị phòng bảo trì
            }
        }
    }
}
