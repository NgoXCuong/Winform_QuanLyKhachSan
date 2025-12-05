using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using QuanLyKhachSan.Models; // Dùng để truy cập SessionInfo

namespace QuanLyKhachSan.UI
{
    public partial class MainForm : Form
    {
        // Biến để quản lý trạng thái giao diện
        private Button currentButton;
        private Form activeForm;
        private System.Windows.Forms.Timer clockTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeClock();
        }

        // Constructor cũ (để tương thích ngược nếu cần, nhưng khuyên dùng SessionInfo)
        public MainForm(string userName) : this()
        {
            // Có thể lưu userName vào biến local nếu muốn, 
            // nhưng SessionInfo là cách tốt nhất hiện tại.
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Hiển thị thông tin người dùng
                DisplayUserInfo();

                // 2. Áp dụng phân quyền
                ApplyPermissions();

                // 3. Mở trang chủ mặc định
                OpenChildForm(new TrangChuForm(), btnTrangChu);

                // 4. Cập nhật đồng hồ ngay lập tức
                UpdateClock();

                // 5. Hiệu ứng Fade-in khi mở form
                StartFadeInEffect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động ứng dụng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 1. Quản lý Người dùng & Phân quyền

        private void DisplayUserInfo()
        {
            if (SessionInfo.CurrentUser != null)
            {
                // Hiển thị: Tên đăng nhập (Vai trò)
                // Ví dụ: admin (Quản trị)
                string displayName = SessionInfo.CurrentUser.TenDangNhap;

                lbUser.Text = $"{displayName}";
            }
            else
            {
                lbUser.Text = "Khách (Chưa đăng nhập)";
            }
        }

        private void ApplyPermissions()
        {
            // Tất cả nút đều hiển thị, phân quyền sẽ kiểm tra trong các form con
            // Không ẩn nút nào ở đây để giao diện nhất quán
        }

        #endregion

        #region 2. Điều hướng Form con (Navigation)

        private void OpenChildForm(Form childForm, object btnSender)
        {
            try
            {
                if (activeForm != null)
                    activeForm.Close();

                HighlightButton((Button)btnSender);

                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                this.pnPage.Controls.Add(childForm);
                this.pnPage.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();

                // Cập nhật tiêu đề form chính theo form con (Tùy chọn)
                // lblTitle.Text = childForm.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở form: " + ex.Message);
            }
        }

        private void HighlightButton(Button btn)
        {
            if (btn == null) return;

            // Reset màu nút cũ
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(38, 40, 52); // Màu nền mặc định
                currentButton.ForeColor = Color.Gainsboro;            // Màu chữ mặc định
            }

            // Set màu nút mới
            currentButton = btn;
            currentButton.BackColor = Color.FromArgb(33, 150, 243);   // Màu xanh nổi bật (Blue)
            currentButton.ForeColor = Color.White;
        }

        // --- Các sự kiện Click ---

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TrangChuForm(), sender);
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PhongForm(), sender);
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BookingRoom(), sender);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HoaDonForm(), sender);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KhachHangForm(), sender);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DichVuForm(), sender);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            // Kiểm tra phân quyền
            var currentUser = SessionInfo.CurrentUser;
            if (currentUser == null || (currentUser.VaiTro != "Quản trị" && currentUser.VaiTro != "Admin"))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenChildForm(new NhanVienForm(), sender);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // Kiểm tra phân quyền
            var currentUser = SessionInfo.CurrentUser;
            if (currentUser == null || (currentUser.VaiTro != "Quản trị" && currentUser.VaiTro != "Admin"))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenChildForm(new ThongKeForm(), sender);
        }

        #endregion

        #region 3. Đồng hồ & Hệ thống

        private void InitializeClock()
        {
            // Đặt Culture tiếng Việt để hiển thị ngày tháng đúng định dạng VN
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");

            clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = 1000; // 1 giây
            clockTimer.Tick += (s, e) => UpdateClock();
            clockTimer.Start();
        }

        private void UpdateClock()
        {
            // Hiển thị: Thứ Tư, ngày 20 tháng 11 năm 2024 - 14:30:05
            lbDate.Text = DateTime.Now.ToString("dddd, 'ngày' dd 'tháng' MM 'năm' yyyy - HH:mm:ss");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Xóa session
                SessionInfo.CurrentUser = null;

                this.Close();

                // Mở lại màn hình đăng nhập (Cần đảm bảo Thread/Application context được xử lý đúng ở Program.cs)
                // Cách đơn giản nhất là chỉ Close form này nếu LoginForm mở nó bằng ShowDialog()
                // Hoặc:
                new LoginForm().Show();
            }
        }

        private void StartFadeInEffect()
        {
            this.Opacity = 0;
            var fadeTimer = new System.Windows.Forms.Timer { Interval = 15 };
            fadeTimer.Tick += (s, args) =>
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.05;
                else
                    ((System.Windows.Forms.Timer)s).Stop();
            };
            fadeTimer.Start();
        }

        #endregion
    }
}

