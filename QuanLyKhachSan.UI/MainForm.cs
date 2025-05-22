using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.UI
{
    public partial class MainForm : Form
    {
        private string tenDangNhap;
        private Button currentButton = null;

        public MainForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo MainForm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public MainForm(string userName)
        {
            try
            {
                InitializeComponent();
                this.tenDangNhap = userName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo MainForm với username: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                lbDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
                lbUser.Text = tenDangNhap;

                OpenChildForm(new TrangChuForm());
                HighlightButton(btnTrangChu);
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
            try
            {
                // Khôi phục màu của nút trước đó nếu có
                if (currentButton != null)
                {
                    currentButton.BackColor = Color.White;
                    currentButton.ForeColor = Color.Black;
                }

                // Cập nhật màu cho nút mới
                btn.BackColor = Color.DarkBlue;
                btn.ForeColor = Color.White;

                // Cập nhật lại nút đang được chọn
                currentButton = btn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi làm nổi bật nút: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
