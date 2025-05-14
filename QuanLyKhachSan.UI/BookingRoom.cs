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
        private readonly BookingRoomService bookingRoomService = new BookingRoomService();

        public BookingRoom()
        {
            InitializeComponent();
            this.lvPhong.Click += new System.EventHandler(this.lvPhong_Click);
            

        }
        private void LoadPhong(string trangThaiLoc = "")
        {
            lvPhong.View = View.LargeIcon;
            lvPhong.LargeImageList = imageList1;
            lvPhong.Items.Clear();

            List<PhongModel> danhSachPhong = phongService.GetAllPhong(); // Lấy từ database

            foreach (var phong in danhSachPhong)
            {
                string trangThai = phong.TrangThai?.Trim().ToLower();

                // Lọc theo trạng thái nếu có
                if (!string.IsNullOrEmpty(trangThaiLoc) && trangThai != trangThaiLoc.ToLower())
                {
                    continue;
                }

                // Tạo item cho ListView
                ListViewItem item = new ListViewItem($"Phòng {phong.SoPhong} ({phong.TrangThai})");

                // Gán hình theo trạng thái
                switch (trangThai)
                {
                    case "trống":
                        item.ImageIndex = 0;
                        break;
                    case "đã đặt":
                        item.ImageIndex = 1;
                        break;
                    case "đang ở":
                        item.ImageIndex = 2;
                        break;
                    case "bảo trì":
                        item.ImageIndex = 3;
                        break;
                    default:
                        item.ImageIndex = 4; // Hình mặc định
                        break;
                }

                // Gán đối tượng phòng vào Tag để dùng sau
                item.Tag = phong;

                // Thêm vào ListView
                lvPhong.Items.Add(item);
            }
        }

        public void loadChonDichVu()
        {
            //dgvChonDichVu.DataSource = bookingRoomService.GetAllDichVu();
            //dgvChonDichVu.Columns["MaDichVu"].HeaderText = "Mã Dịch Vụ";
            //dgvChonDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            //dgvChonDichVu.Columns["DonGia"].HeaderText = "Đơn Giá";
            //dgvChonDichVu.Columns["MaPhong"].HeaderText = "Mã Phòng";
            //dgvChonDichVu.Columns["SoPhong"].HeaderText = "Số Phòng";
            //dgvChonDichVu.Columns["SoLuong"].HeaderText = "Số Lượng";

        }


        private void lvPhong_Click(object sender, EventArgs e)
        {
            if (lvPhong.SelectedItems.Count > 0)
            {
                var item = lvPhong.SelectedItems[0];
                PhongModel phong = item.Tag as PhongModel;

                if (phong != null)
                {
                    txtTenDatPhong.Text = phong.SoPhong.ToString();     // Hiện số phòng
                    //txtMaPhong.Text = phong.MaPhong.ToString();         // (nếu có textbox MaPhong)
                    //txtTrangThai.Text = phong.TrangThai;                // (nếu có textbox Trạng Thái)
                }
            }
        }




        private void BookingRoom_Load(object sender, EventArgs e)
        {
            LoadPhong();
            loadChonDichVu();
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

                RadioButton rb = new RadioButton();
                rb.CheckedChanged += Rb_CheckedChanged;
                //flowPhong.Controls.Add(rb);
            }
        }
        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                DataRow row = rb.Tag as DataRow;
                if (row != null)
                {
                    txtTenDatPhong.Text = row["SoPhong"].ToString(); // Hiện số phòng
                }
            }
        }
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            // Kiểm tra ngày tháng
            if (dtNgayDat.Value < new DateTime(1753, 1, 1) ||
                dtNgayNhan.Value < new DateTime(1753, 1, 1) ||
                dtNgayTra.Value < new DateTime(1753, 1, 1))
            {
                MessageBox.Show("Ngày chọn không hợp lệ. Vui lòng kiểm tra lại.");
                return;
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(txtTenDatPhong.Text))
            {
                MessageBox.Show("Vui lòng chọn phòng trước khi đặt!");
                return;
            }
            else if (string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng dịch vụ!");
                return;
            }
            else if (string.IsNullOrEmpty(cbDichVu.Text))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ!");
                return;
            }

            // Kiểm tra Mã Khách Hàng có hợp lệ không
            if (string.IsNullOrEmpty(txtMaKH.Text) || txtMaKH.Text == "0")
            {
                MessageBox.Show("Vui lòng chọn khách hàng hợp lệ.");
                return;
            }

            // Kiểm tra Mã Nhân Viên có hợp lệ không
            if (string.IsNullOrEmpty(txtMaNV.Text) || txtMaNV.Text == "0")
            {
                MessageBox.Show("Vui lòng chọn nhân viên hợp lệ.");
                return;
            }

            // Tạo đối tượng BookingRoomModel
            BookingRoomModel bookingRoom = new BookingRoomModel
            {
                MaDichVu = Convert.ToInt32(cbDichVu.SelectedValue),
                SoPhong = Convert.ToInt32(txtTenDatPhong.Text),
                SoLuong = Convert.ToInt32(txtSoLuong.Text),
                MaKH = Convert.ToInt32(txtMaKH.Text),  // Gán MaKH từ TextBox
                MaNV = Convert.ToInt32(txtMaNV.Text),  // Gán MaNV từ TextBox
                NgayDat = dtNgayDat.Value,
                NgayNhan = dtNgayNhan.Value,
                NgayTra = dtNgayTra.Value
            };

            // Thực hiện gọi Insert
            bool ketqua = bookingRoomService.InsertBookingRoom(bookingRoom);
            if (ketqua)
            {
                MessageBox.Show("Đặt phòng thành công!");
                LoadPhong();
                loadChonDichVu();
            }
            else
            {
                MessageBox.Show("Đặt phòng thất bại!");
            }
        }


    }
}
