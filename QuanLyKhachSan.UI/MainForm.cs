using System;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace QuanLyKhachSan.UI
{
    public partial class MainForm : Form
    {
        private string tenDangNhap;
        private Button currentButton = null;
        private System.Windows.Forms.Timer clockTimer; // Timer cho đồng hồ

        public MainForm()
        {
            try
            {
                InitializeComponent();
                InitializeClock();
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
                InitializeClock();
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
                lbUser.Text = tenDangNhap;
                OpenChildForm(new TrangChuForm());
                HighlightButton(btnTrangChu);
                UpdateClock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải MainForm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================== CLOCK ======================
        private void InitializeClock()
        {
            // Đặt ngôn ngữ tiếng Việt
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");

            clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = 1000; // 1 giây
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            lbDate.Text = DateTime.Now.ToString("dddd, 'ngày' dd 'tháng' MM 'năm' yyyy - HH:mm:ss");
        }

        // ====================== FORM HIỂN THỊ ======================
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

        // ====================== BUTTON ======================
        private void HighlightButton(Button btn)
        {
            try
            {
                // Khôi phục màu nút cũ
                if (currentButton != null)
                {
                    currentButton.BackColor = Color.FromArgb(38, 40, 52);
                    currentButton.ForeColor = Color.FromArgb(220, 220, 220);
                }

                // Làm nổi nút hiện tại
                btn.BackColor = Color.FromArgb(33, 150, 243);
                btn.ForeColor = Color.White;

                currentButton = btn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi làm nổi bật nút: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================== SỰ KIỆN NÚT ======================
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn đăng xuất?",
                    "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    this.Close();
                    new LoginForm().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thoát: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TrangChuForm());
            HighlightButton(btnTrangChu);
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PhongForm());
            HighlightButton(btnPhong);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NhanVienForm());
            HighlightButton(btnNhanVien);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TestHoaDon());
            HighlightButton(btnHoaDon);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DichVuForm());
            HighlightButton(btnDichVu);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KhachHangForm());
            HighlightButton(btnKhachHang);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongKeForm());
            HighlightButton(btnThongKe);
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DatPhongForm());
            HighlightButton(btnBooking);
        }

        // ====================== HIỆU ỨNG FADE-IN ======================
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Opacity = 0;
            var fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 10;
            fadeTimer.Tick += (s, args) =>
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.05;
                else
                    fadeTimer.Stop();
            };
            fadeTimer.Start();
        }

        private void label2_Click(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}
