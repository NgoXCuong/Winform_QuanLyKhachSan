using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.UI
{
    public partial class KhachHangForm : Form
    {
        private readonly KhachHangService khachHangService = new KhachHangService();
        public KhachHangForm()
        {
            InitializeComponent();
        }

        private void LoadListKhachHang()
        {
            dgvKhachHang.DataSource = khachHangService.GetAllKhachHang();
            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvKhachHang.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvKhachHang.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvKhachHang.Columns["CMND"].HeaderText = "CMND";
            dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dgvKhachHang.Columns["Email"].HeaderText = "Email";
            dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
        }

        private void ResetKhachHang()
        {
            txtTenKhachHang.Clear();
            rbNam.Checked = false;
            rbNu.Checked = false;
            dtNgaySinh.Value = DateTime.Now;
            txtCMND.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text) ||
                (!rbNam.Checked && !rbNu.Checked) ||
                string.IsNullOrWhiteSpace(txtCMND.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text)||
                string.IsNullOrWhiteSpace(txtDiaChi.Text))
            // dtNgaySinh không bao giồ null
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string gioiTinh = rbNam.Checked ? "Nam" : "Nữ";

            KhachHangModel kh = new KhachHangModel
            {
                HoTen = txtTenKhachHang.Text,
                GioiTinh = gioiTinh,
                NgaySinh = dtNgaySinh.Value,
                CMND = txtCMND.Text,
                SoDienThoai = txtSDT.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text
            };

            bool ketQua = khachHangService.ThemKhachHang(kh);

            if (ketQua)
            {
                MessageBox.Show("Thêm khách hàng thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListKhachHang();
                ResetKhachHang();
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text) ||
                (!rbNam.Checked && !rbNu.Checked) ||
                string.IsNullOrWhiteSpace(txtCMND.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maKH = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["MaKH"].Value);
            string gioiTinh = rbNam.Checked ? "Nam" : "Nữ";

            KhachHangModel kh = new KhachHangModel()
            {
                MaKH = maKH,
                HoTen = txtTenKhachHang.Text,
                GioiTinh = gioiTinh,
                NgaySinh = dtNgaySinh.Value,
                CMND = txtCMND.Text,
                SoDienThoai = txtSDT.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text
            };

            bool result = khachHangService.SuaKhachHang(kh);

            if (result)
            {
                MessageBox.Show("Cập nhật khách hàng thành công!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListKhachHang();
                ResetKhachHang();
            }
            else
            {
                MessageBox.Show("Cập nhật khách hàng thất bại!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
