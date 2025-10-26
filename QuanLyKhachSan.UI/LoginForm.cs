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
    public partial class LoginForm : Form
    {
        private TaiKhoanService taiKhoanService = new TaiKhoanService();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TaiKhoanModel tk = taiKhoanService.GetTaiKhoanByTenDangNhap(username, password);

                if (tk != null)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();

                    // Truyền quyền vào MainForm để phân quyền
                    MainForm mainForm = new MainForm(tk.TenDangNhap);
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbAnPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAnPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void linkQuenMatkhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Tạo form Quên mật khẩu mới
            QuenMatKhauForm quenMatKhauForm = new QuenMatKhauForm();

            // Mở form dưới dạng cửa sổ độc lập
            quenMatKhauForm.Show();

            // Nếu muốn mở dưới dạng modal (không cho thao tác với form trước), dùng:
            // quenMatKhauForm.ShowDialog();
        }

    }
}
