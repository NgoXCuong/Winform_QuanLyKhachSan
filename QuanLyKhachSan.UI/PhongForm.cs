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
    public partial class PhongForm : Form
    {
        public PhongForm()
        {
            InitializeComponent();
        }

        private void PhongForm_Load(object sender, EventArgs e)
        {
            PhongService phongService = new PhongService();
            dgvListPhong.DataSource = phongService.GetAllPhong();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text) ||
                string.IsNullOrWhiteSpace(cbLoaiPhong.Text) ||
                string.IsNullOrWhiteSpace(cbTrangThai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(cbLoaiPhong.Text, out int loaiPhong))
            {
                MessageBox.Show("Loại phòng không hợp lệ", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }if (!int.TryParse(cbLoaiPhong.Text, out int soPhong))
            {
                MessageBox.Show("Loại phòng không hợp lệ", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PhongModel phong = new PhongModel
            {
                SoPhong = int.Parse(txtSoPhong.Text),
                LoaiPhong = int.Parse(cbLoaiPhong.Text),
                TrangThai = cbTrangThai.Text
            };
            PhongService phongService = new PhongService();
            bool ketQua = phongService.ThemPhong(phong);
            if (ketQua)
            {
                MessageBox.Show("Thêm phòng thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListPhong();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm phòng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadListPhong()
        {
            PhongService phongService = new PhongService();
            dgvListPhong.DataSource = phongService.GetAllPhong();
        }

        private void ClearForm()
        {
            txtSoPhong.Clear();
            cbLoaiPhong.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvListPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng để sửa", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvListPhong.SelectedRows[0];

            try
            {
                int maPhong = Convert.ToInt32(selectedRow.Cells["MaPhong"].Value);
                int soPhong = Convert.ToInt32(selectedRow.Cells["SoPhong"].Value);
                int loaiPhong = Convert.ToInt32(selectedRow.Cells["LoaiPhong"].Value);
                string trangThai = selectedRow.Cells["TrangThai"].Value?.ToString();

                PhongModel phong = new PhongModel
                {
                    MaPhong = maPhong,
                    SoPhong = soPhong,
                    LoaiPhong = loaiPhong,
                    TrangThai = trangThai
                };

                PhongService phongService = new PhongService();
                bool ketQua = phongService.SuaPhong(phong);

                if (ketQua)
                {
                    MessageBox.Show("Sửa phòng thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListPhong();
                }
                else
                {
                    MessageBox.Show("Sửa phòng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvListPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng để xóa", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int maPhong = (int)dgvListPhong.SelectedRows[0].Cells["MaPhong"].Value;
            PhongService phongService = new PhongService();
            bool ketQua = phongService.XoaPhong(maPhong);
            if (ketQua)
            {
                MessageBox.Show("Xóa phòng thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListPhong();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Xóa phòng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            PhongService phongService = new PhongService();
            dgvListPhong.DataSource = phongService.TimPhong(keyword);
        }

        private void dvgListPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvListPhong.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvListPhong.SelectedRows[0];
                //txtMaPhong.Text = row.Cells["MaPhong"].Value.ToString();
                txtSoPhong.Text = row.Cells["SoPhong"].Value.ToString();
                cbLoaiPhong.Text = row.Cells["LoaiPhong"].Value.ToString();
                cbTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng để xem thông tin", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }   






        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
