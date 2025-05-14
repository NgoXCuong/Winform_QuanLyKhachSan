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
                    btnDatPhong, btnThongKe, btnBooking
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
                DisableButton(btnDatPhong);

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

        private void EnableButton(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lbDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
            lbUser.Text = tenDangNhap;

            OpenChildForm(new TrangChuForm());
            HighlightButton(btnTrangChu);

            PhanQuyenNguoiDung();
        }

        private void OpenChildForm(Form childForm)
        {
            pnPage.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnPage.Controls.Add(childForm);
            pnPage.Tag = childForm;

            childForm.Show();
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
            this.Close();
            new LoginForm().Show();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TrangChuForm());
            HighlightButton(btnTrangChu);
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            if (quyen != "Admin")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new PhongForm());
            HighlightButton(btnPhong);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            if (quyen != "Admin")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new NhanVienForm());
            HighlightButton(btnNhanVien);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            if (quyen != "Admin")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new HoaDonForm());
            HighlightButton(btnHoaDon);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            if (quyen != "Admin")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new DichVuForm());
            HighlightButton(btnDichVu);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            if (quyen != "Admin")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new KhachHangForm());
            HighlightButton(btnKhachHang);
        }

        //private void btnDatPhong_Click(object sender, EventArgs e)
        //{
        //    OpenChildForm(new DatPhongForm());
        //    HighlightButton(btnDatPhong);
        //}

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ThongKeForm());
            HighlightButton(btnThongKe);
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BookingRoom());
            HighlightButton(btnBooking);
        }
    }
}
