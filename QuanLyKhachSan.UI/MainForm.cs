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
    public partial class MainForm : Form
    {
        private string tenDangNhap;
        private string quyen;

        private List<Button> allowedButtons = new List<Button>();

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string userName)
        {
            InitializeComponent();
            this.tenDangNhap = userName;
        }

        public MainForm(string tenDangNhap, string quyen)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            this.quyen = quyen;

            PhanQuyenNguoiDung();
        }

        private void PhanQuyenNguoiDung()
        {
            allowedButtons.Clear();

            if (quyen == "Admin")

            {
                // Admin được quyền tất cả các nút
                allowedButtons.AddRange(new[]
                {
                    btnTrangChu, btnPhong, btnNhanVien,
                    btnHoaDon, btnKhachHang, btnDichVu, 
                    btnThongKe, btnBooking
                });
            }
            else
            {
                // Nhân viên chỉ được quyền vài nút
                DisableButton(btnPhong);
                DisableButton(btnNhanVien);
                DisableButton(btnHoaDon);
                DisableButton(btnKhachHang);
                DisableButton(btnDichVu);

                // Các nút được phép
                allowedButtons.AddRange(new[]
                {
                    btnTrangChu, btnThongKe, btnBooking
                });
            }
        }

        private void DisableButton(Button btn)
        {
            btn.BackColor = Color.Gray;
            btn.ForeColor = Color.LightGray;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                lbDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
                lbUser.Text = tenDangNhap;

                OpenChildForm(new TrangChuForm());
                HighlightButton(btnTrangChu);

                PhanQuyenNguoiDung();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải MainForm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenChildForm(Form childForm)
        {
            try
            {
                pnPage.Controls.Clear();

                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                pnPage.Controls.Add(childForm);
                pnPage.Tag = childForm;

                childForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở form con: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HighlightButton(Button btn)
        {
            foreach (var b in allowedButtons)
            {
                b.BackColor = Color.White;
                b.ForeColor = Color.Black;
            }

            btn.BackColor = Color.DarkBlue;
            btn.ForeColor = Color.White;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                new LoginForm().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thoát: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChildForm(new TrangChuForm());
                HighlightButton(btnTrangChu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Trang chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            try
            {
                if (quyen != "Admin")
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OpenChildForm(new PhongForm());
                HighlightButton(btnPhong);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (quyen != "Admin")
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OpenChildForm(new NhanVienForm());
                HighlightButton(btnNhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                if (quyen != "Admin")
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OpenChildForm(new HoaDonForm());
                HighlightButton(btnHoaDon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                if (quyen != "Admin")
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OpenChildForm(new DichVuForm());
                HighlightButton(btnDichVu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            try
            {
                if (quyen != "Admin")
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OpenChildForm(new KhachHangForm());
                HighlightButton(btnKhachHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChildForm(new ThongKeForm());
                HighlightButton(btnThongKe);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            try
            {
                OpenChildForm(new BookingRoom());
                HighlightButton(btnBooking);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở Booking: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
