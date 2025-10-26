using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.UI
{
    public partial class QuenMatKhauForm : Form
    {
        public QuenMatKhauForm()
        {
            InitializeComponent();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            string emailNhan = txtUsername.Text.Trim();
            if (string.IsNullOrEmpty(emailNhan))
            {
                MessageBox.Show("Vui lòng nhập email!");
                return;
            }

            TaiKhoanService taiKhoanService = new TaiKhoanService();
            TaiKhoanModel tk = taiKhoanService.GetTaiKhoanByEmail(emailNhan);

            if (tk == null)
            {
                MessageBox.Show("Email không tồn tại!");
                return;
            }

            try
            {
                string fromEmail = "20232175@eaut.edu.vn";
                string fromPassword = "prqc xpyc omce yfyj"; // App password Gmail

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(tk.Email);
                mail.Subject = "Lấy lại mật khẩu hệ thống quản lý khách sạn";
                mail.Body = $"Tên đăng nhập: {tk.TenDangNhap}\nMật khẩu: {tk.MatKhau}";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);

                MessageBox.Show("Đã gửi email thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gửi email thất bại: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
