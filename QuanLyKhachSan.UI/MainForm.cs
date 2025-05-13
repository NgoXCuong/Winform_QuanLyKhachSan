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
        private string userName;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lbDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
            lbUser.Text = userName;

            OpenChildForm(new TrangChuForm());
            HighlightButton(btnTrangChu);
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
            // Reset tất cả các nút về màu mặc định
            btnTrangChu.BackColor = Color.White;
            btnTrangChu.ForeColor = Color.Black;

            btnPhong.BackColor = Color.White;
            btnPhong.ForeColor = Color.Black;

            btnNhanVien.BackColor = Color.White;
            btnNhanVien.ForeColor = Color.Black;

            btnHoaDon.BackColor = Color.White;
            btnHoaDon.ForeColor = Color.Black;

            btnKhachHang.BackColor = Color.White;
            btnKhachHang.ForeColor = Color.Black;

            btnDichVu.BackColor = Color.White;
            btnDichVu.ForeColor = Color.Black;

            btnDatPhong.BackColor = Color.White;
            btnDatPhong.ForeColor = Color.Black;

            btnThongKe.BackColor = Color.White;
            btnThongKe.ForeColor = Color.Black;

            btnBooking.BackColor = Color.White;
            btnBooking.ForeColor = Color.Black;

            // Chỉ đổi màu nút được click
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
            OpenChildForm(new HoaDonForm());
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

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DatPhongForm());
            HighlightButton(btnDatPhong);
        }

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
