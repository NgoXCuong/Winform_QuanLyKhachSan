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
    public partial class NhanVienForm : Form
    {
        public NhanVienForm()
        {
            InitializeComponent();
        }

        private void NhanVienForm_Load(object sender, EventArgs e)
        {
            NhanVienService service = new NhanVienService();
            dgvListNhanVien.DataSource = service.GetAllNhanVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                (!rbNam.Checked && rbNu.Checked) ||
                string.IsNullOrWhiteSpace(txtChucVu.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
                // dtNgaySinh không bao giồ null
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string gioiTinh = rbNam.Checked ? "Nam" : "Nữ";

            NhanVienModel nv = new NhanVienModel
            {
                HoTen = txtHoTen.Text,
                GioiTinh = gioiTinh,
                NgaySinh = dtNgaySinh.Value,
                ChucVu = txtChucVu.Text,
                SoDienThoai = txtSDT.Text,
                Email = txtEmail.Text
            };

            NhanVienService nhanVienService = new NhanVienService();
            bool ketQua = nhanVienService.ThemNhanVien(nv);

            if (ketQua)
            {
                MessageBox.Show("Thêm nhân viên thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListNhanVien();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadListNhanVien()
        {
            NhanVienService service = new NhanVienService();
            dgvListNhanVien.DataSource = service.GetAllNhanVien();
        }


        private void ClearForm()
        {
            txtHoTen.Clear();
            rbNam.Checked = false;
            rbNu.Checked = false;
            dtNgaySinh.Value = DateTime.Now;
            txtChucVu.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
        }

        
    }
}
