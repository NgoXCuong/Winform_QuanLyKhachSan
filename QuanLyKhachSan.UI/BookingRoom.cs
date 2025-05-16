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
            dgvChonDichVu.DataSource = bookingRoomService.GetAllDichVu();
            dgvChonDichVu.Columns["MaDichVu"].HeaderText = "Mã Dịch Vụ";
            dgvChonDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            dgvChonDichVu.Columns["SoPhong"].HeaderText = "Số Phòng";
            dgvChonDichVu.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvChonDichVu.Columns["SoLuong"].HeaderText = "Số Lượng";


            dgvChonDichVu.Columns["NgayDat"].Visible = false;
            dgvChonDichVu.Columns["NgayNhan"].Visible = false;
            dgvChonDichVu.Columns["NgayTra"].Visible = false;
            dgvChonDichVu.Columns["MaPhong"].Visible = false;
            dgvChonDichVu.Columns["MaDatPhong"].Visible = false;
            dgvChonDichVu.Columns["GiaPhong"].Visible = false;
            dgvChonDichVu.Columns["MaLoaiPhong"].Visible = false;
            dgvChonDichVu.Columns["TenLoaiPhong"].Visible = false;
            dgvChonDichVu.Columns["TrangThai"].Visible = false;
        }
        private void LoadDichVuTheoPhong(int soPhong)
        {
            var danhSach = bookingRoomService.GetAllDichVu()
                .Where(dv => dv.SoPhong == soPhong)
                .ToList();

            dgvChonDichVu.DataSource = danhSach;

            dgvChonDichVu.Columns["MaDichVu"].HeaderText = "Mã Dịch Vụ";
            dgvChonDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            dgvChonDichVu.Columns["SoPhong"].HeaderText = "Số Phòng";
            dgvChonDichVu.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvChonDichVu.Columns["SoLuong"].HeaderText = "Số Lượng";

            // Ẩn các cột không cần hiển thị
            dgvChonDichVu.Columns["NgayDat"].Visible = false;
            dgvChonDichVu.Columns["NgayNhan"].Visible = false;
            dgvChonDichVu.Columns["NgayTra"].Visible = false;
            dgvChonDichVu.Columns["MaPhong"].Visible = false;
            dgvChonDichVu.Columns["MaDatPhong"].Visible = false;
            dgvChonDichVu.Columns["GiaPhong"].Visible = false;

            // Tính tổng tiền dịch vụ
            decimal tongTienDichVu = 0;
            foreach (var dv in danhSach)
            {
                tongTienDichVu += dv.DonGia * dv.SoLuong;
            }

            // Lấy GiaPhong từ bảng LoaiPhong dựa theo SoPhong
            decimal giaPhong = bookingRoomService.GetGiaPhongTheoSoPhong(soPhong);

            decimal tongTienAll = tongTienDichVu + giaPhong;

            lbTienDV.Text = tongTienDichVu.ToString("N0") + " VNĐ";
            lbTongTienAll.Text = tongTienAll.ToString("N0") + " VNĐ";

        }

        public void LoadDatPhong()
        {
            dgvListDatPhong.DataSource = bookingRoomService.GetAllPhongDat();
            dgvListDatPhong.Columns["MaPhong"].HeaderText = "Mã Phòng";
            dgvListDatPhong.Columns["SoPhong"].HeaderText = "Số Phòng";
            dgvListDatPhong.Columns["MaLoaiPhong"].HeaderText = "Mã Loại Phòng";
            dgvListDatPhong.Columns["TenLoaiPhong"].HeaderText = "Tên Loại Phòng";
            dgvListDatPhong.Columns["GiaPhong"].HeaderText = "Giá Phòng";
            dgvListDatPhong.Columns["TrangThai"].HeaderText = "Trạng Thái";

            dgvListDatPhong.Columns["MaDatPhong"].Visible = false;
            dgvListDatPhong.Columns["MaDichVu"].Visible = false;
            dgvListDatPhong.Columns["TenDichVu"].Visible = false;
            dgvListDatPhong.Columns["DonGia"].Visible = false;
            dgvListDatPhong.Columns["SoLuong"].Visible = false;
            dgvListDatPhong.Columns["NgayDat"].Visible = false;
            dgvListDatPhong.Columns["NgayNhan"].Visible = false;
            dgvListDatPhong.Columns["NgayTra"].Visible = false;
            dgvListDatPhong.Columns["MaKH"].Visible = false;
            dgvListDatPhong.Columns["MaNV"].Visible = false;
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

                    LoadDichVuTheoPhong(phong.SoPhong);
                }
            }
        }

        private void BookingRoom_Load(object sender, EventArgs e)
        {
            LoadPhong();
            loadChonDichVu();
            LoadDatPhong();
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
                MaDichVu = Convert.ToInt32(cbDichVu.Text),
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
                bookingRoomService.CapNhatTrangThaiPhong_DaDat(bookingRoom);
                LoadPhong();
                loadChonDichVu();
            }
            else
            {
                MessageBox.Show("Đặt phòng thất bại!");
            }
        }
        private void btnHuyDat_Click(object sender, EventArgs e)
        {
            if (lvPhong.SelectedItems.Count > 0)
            {
                var item = lvPhong.SelectedItems[0];
                PhongModel phong = item.Tag as PhongModel;

                if (phong != null)
                {
                    BookingRoomModel booking = new BookingRoomModel
                    {
                        SoPhong = phong.SoPhong
                        // Có thể gán thêm MaPhong hoặc MaDatPhong nếu đã có
                    };

                    DialogResult result = MessageBox.Show("Bạn có chắc muốn hủy đặt phòng này?", "Xác nhận", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        bool thanhCong = bookingRoomService.HuyDatPhong(booking);
                        if (thanhCong)
                        {
                            MessageBox.Show("Hủy đặt phòng thành công.");
                            LoadPhong(); // Cập nhật giao diện
                            txtTenDatPhong.Text = "";
                            lbTienDV.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Không thể hủy vì không tìm thấy dữ liệu.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng cần hủy.");
            }
        }

        private void btnHuyDV_Click(object sender, EventArgs e)
        {
            if (dgvChonDichVu.CurrentRow != null)
            {
                BookingRoomModel booking = new BookingRoomModel
                {
                    SoPhong = Convert.ToInt32(dgvChonDichVu.CurrentRow.Cells["SoPhong"].Value),
                    MaDichVu = Convert.ToInt32(dgvChonDichVu.CurrentRow.Cells["MaDichVu"].Value)
                };

                bool ketQua = bookingRoomService.HuyDichVuTheoSoPhongVaMaDV(booking);
                if (ketQua)
                {
                    MessageBox.Show("Đã hủy dịch vụ thành công!");
                    LoadDichVuTheoPhong(booking.SoPhong);
                }
                else
                {
                    MessageBox.Show("Hủy dịch vụ thất bại!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng dịch vụ để hủy.");
            }
        }

        private void btnThemDV_Click(object sender, EventArgs e)
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
                MaDichVu = Convert.ToInt32(cbDichVu.Text),
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
                MessageBox.Show("Thêm dịch vụ thành công!");
                LoadPhong();
                loadChonDichVu();
            }
            else
            {
                MessageBox.Show("Thêm dịch vụ thất bại!");
            }

        }
    }
}
