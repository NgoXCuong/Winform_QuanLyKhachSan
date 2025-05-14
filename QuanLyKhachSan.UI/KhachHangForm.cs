using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
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

        private void KhachHangForm_Load(object sender, EventArgs e)
        {
            LoadListKhachHang();
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow != null)
            {
                // Xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int maKH = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["MaKH"].Value);

                    bool xoaThanhCong = khachHangService.XoaKhachHang(maKH);

                    if (xoaThanhCong)
                    {
                        MessageBox.Show("Xóa khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadListKhachHang();
                        ResetKhachHang();
                    }
                    else
                    {
                        MessageBox.Show("Xóa khách hàng thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Workbook|*.xlsx";
                saveFileDialog.Title = "Lưu file Excel";
                saveFileDialog.FileName = "DanhSachKhachHang.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Khách Hàng");

                        // Header
                        for (int i = 0; i < dgvKhachHang.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgvKhachHang.Columns[i].HeaderText;
                            worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                            worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }

                        // Dữ liệu
                        for (int i = 0; i < dgvKhachHang.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvKhachHang.Columns.Count; j++)
                            {
                                object value = dgvKhachHang.Rows[i].Cells[j].Value;
                                worksheet.Cell(i + 2, j + 1).Value = value?.ToString();

                                // Căn giữa, bo viền
                                worksheet.Cell(i + 2, j + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(i + 2, j + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                        }

                        // Tự động điều chỉnh độ rộng
                        worksheet.Columns().AdjustToContents();

                        // Lưu file
                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadListKhachHang();
            ResetKhachHang();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTim.Text.Trim();

            List<KhachHangModel> result = khachHangService.TimKiemKhachHang(keyword);

            if (result.Count > 0)
            {
                dgvKhachHang.DataSource = result;
            }
            else
            {
                MessageBox.Show($"Không tìm thấy khách hàng nào với từ khóa {keyword}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvKhachHang.DataSource = null;
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtTenKhachHang.Text = row.Cells["HoTen"].Value.ToString();
                rbNam.Checked = row.Cells["GioiTinh"].Value.ToString() == "Nam";
                rbNu.Checked = row.Cells["GioiTinh"].Value.ToString() == "Nữ";
                dtNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtCMND.Text = row.Cells["CMND"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();

            }
        }
    }
}
