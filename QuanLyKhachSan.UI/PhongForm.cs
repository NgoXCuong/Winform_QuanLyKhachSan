﻿using System;
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
    public partial class PhongForm : Form
    {
        private PhongService phongService = new PhongService();
        private LoaiPhongService loaiPhongService = new LoaiPhongService();

        public PhongForm()
        {
            InitializeComponent();
        }

        private void PhongForm_Load(object sender, EventArgs e)
        {
            LoadListPhong();
            ClearFormPhong();

            LoadListLoaiPhong();
            ClearFormLoaiPhong();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSoPhong.Text) ||
                    cbLoaiPhong.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(cbTrangThai.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSoPhong.Text, out int soPhong))
                {
                    MessageBox.Show("Số phòng không hợp lệ", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbLoaiPhong.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn loại phòng hợp lệ", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int loaiPhong = (int)cbLoaiPhong.SelectedValue;

                // Nếu có phương thức kiểm tra số phòng tồn tại, có thể thêm ở đây (nếu không có thì bỏ qua)
                if (phongService.KiemTraSoPhongTonTai(soPhong))
                {
                    MessageBox.Show("Số phòng đã tồn tại, vui lòng nhập số khác", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PhongModel phong = new PhongModel
                {
                    SoPhong = soPhong,
                    LoaiPhong = loaiPhong,
                    TrangThai = cbTrangThai.Text
                };

                bool ketQua = phongService.ThemPhong(phong);

                if (ketQua)
                {
                    MessageBox.Show("Thêm phòng thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListPhong();
                    ClearFormPhong();
                }
                else
                {
                    MessageBox.Show("Thêm phòng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
            {
                if (dgvListPhong.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn phòng để xóa", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int maPhong = (int)dgvListPhong.SelectedRows[0].Cells["MaPhong"].Value;
                bool ketQua = phongService.XoaPhong(maPhong);
                if (ketQua)
                {
                    MessageBox.Show("Xóa phòng thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListPhong();
                    ClearFormPhong();
                }
                else
                {
                    MessageBox.Show("Xóa phòng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox

                List<PhongModel> result = phongService.TimPhong(keyword);

                if (result.Count > 0)
                {
                    dgvListPhong.DataSource = result;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy phòng với từ khóa {keyword}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvListPhong.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListPhong.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Workbook|*.xlsx";
                    saveFileDialog.Title = "Lưu file Excel";
                    saveFileDialog.FileName = "DanhSachPhong.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Loại Phòng");

                            // Header
                            for (int i = 0; i < dgvListPhong.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvListPhong.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                                worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }

                            // Dữ liệu
                            for (int i = 0; i < dgvListPhong.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvListPhong.Columns.Count; j++)
                                {
                                    object value = dgvListPhong.Rows[i].Cells[j].Value;
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadListPhong();
                ClearFormPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại danh sách phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvListPhong.Rows[e.RowIndex];

                    txtSoPhong.Text = row.Cells["SoPhong"].Value?.ToString();

                    // Nếu cột "LoaiPhong" là mã loại phòng (int), dùng SelectedValue
                    if (int.TryParse(row.Cells["LoaiPhong"].Value?.ToString(), out int maLoaiPhong))
                    {
                        cbLoaiPhong.SelectedValue = maLoaiPhong;
                    }

                    cbTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadListPhong()
        {
            dgvListPhong.DataSource = phongService.GetAllPhong();
            dgvListPhong.Columns["MaPhong"].HeaderText = "Mã Phòng";
            dgvListPhong.Columns["SoPhong"].HeaderText = "Số Phòng";
            dgvListPhong.Columns["LoaiPhong"].HeaderText = "Loại Phòng";
            dgvListPhong.Columns["TrangThai"].HeaderText = "Trạng Thái";

            var danhSachLoaiPhong = loaiPhongService.GetLoaiPhongByIdName();
            cbLoaiPhong.DataSource = danhSachLoaiPhong;
            cbLoaiPhong.DisplayMember = "HienThiMaVaTen";  // hiển thị "Mã - Tên"
            cbLoaiPhong.ValueMember = "MaLoaiPhong";        // chọn -> lấy mã
        }

        private void ClearFormPhong()
        {
            txtSoPhong.Clear();
            cbLoaiPhong.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
        }

        ////  LOẠI PHÒNG
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenLoaiPhong.Text) ||
                string.IsNullOrWhiteSpace(txtMoTa.Text) ||
                string.IsNullOrWhiteSpace(txtGiaPhong.Text) ||
                string.IsNullOrWhiteSpace(txtSoNguoi.Text))
                // dtNgaySinh không bao giồ null
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin loại phòng", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                LoaiPhongModel loaiPhong = new LoaiPhongModel
                {
                    TenLoaiPhong = txtTenLoaiPhong.Text,
                    MoTa = txtMoTa.Text,
                    GiaPhong = decimal.Parse(txtGiaPhong.Text),
                    SoNguoiToiDa = int.Parse(txtSoNguoi.Text)
                };

                bool ketQua = loaiPhongService.ThemLoaiPhong(loaiPhong);

                if (ketQua)
                {
                    MessageBox.Show("Thêm loại phòng thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListLoaiPhong();
                    ClearFormLoaiPhong();
                }
                else
                {
                    MessageBox.Show("Thêm loại phòng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm loại phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListLoaiPhong.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại phòng này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int maLoaiPhong = Convert.ToInt32(dgvListLoaiPhong.CurrentRow.Cells["MaLoaiPhong"].Value);

                        bool xoaThanhCong = loaiPhongService.XoaLoaiPhong(maLoaiPhong);

                        if (xoaThanhCong)
                        {
                            MessageBox.Show("Xóa loại phòng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadListLoaiPhong();
                            ClearFormLoaiPhong();
                        }
                        else
                        {
                            MessageBox.Show("Xóa loại phòng thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một loại phòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa loại phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvListLoaiPhong.Rows[e.RowIndex];

                    txtTenLoaiPhong.Text = row.Cells["TenLoaiPhong"].Value.ToString();
                    txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
                    txtGiaPhong.Text = row.Cells["GiaPhong"].Value.ToString();
                    txtSoNguoi.Text = row.Cells["SoNguoiToiDa"].Value.ToString();

                    int maLoaiPhong = Convert.ToInt32(row.Cells["MaLoaiPhong"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn loại phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListLoaiPhong.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn loại phòng cần sửa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenLoaiPhong.Text) ||
                    string.IsNullOrWhiteSpace(txtMoTa.Text) ||
                    string.IsNullOrWhiteSpace(txtGiaPhong.Text) ||
                    string.IsNullOrWhiteSpace(txtSoNguoi.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maLoaiPhong = Convert.ToInt32(dgvListLoaiPhong.CurrentRow.Cells["MaLoaiPhong"].Value);

                LoaiPhongModel loaiPhong = new LoaiPhongModel
                {
                    MaLoaiPhong = maLoaiPhong,
                    TenLoaiPhong = txtTenLoaiPhong.Text,
                    MoTa = txtMoTa.Text,
                    GiaPhong = decimal.Parse(txtGiaPhong.Text),
                    SoNguoiToiDa = int.Parse(txtSoNguoi.Text)
                };

                bool result = loaiPhongService.SuaLoaiPhong(loaiPhong);

                if (result)
                {
                    MessageBox.Show("Cập nhật loại phòng thành công!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListLoaiPhong();
                    ClearFormLoaiPhong();
                }
                else
                {
                    MessageBox.Show("Cập nhật loại phòng thất bại!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật loại phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListLoaiPhong.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Workbook|*.xlsx";
                    saveFileDialog.Title = "Lưu file Excel";
                    saveFileDialog.FileName = "DanhSachLoaiPhong.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Loại Phòng");

                            // Header
                            for (int i = 0; i < dgvListLoaiPhong.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvListLoaiPhong.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                                worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }

                            // Dữ liệu
                            for (int i = 0; i < dgvListLoaiPhong.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvListLoaiPhong.Columns.Count; j++)
                                {
                                    object value = dgvListLoaiPhong.Rows[i].Cells[j].Value;
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadListLoaiPhong();
                ClearFormLoaiPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại danh sách loại phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox

                List<LoaiPhongModel> result = loaiPhongService.TimLoaiPhong(keyword);

                if (result.Count > 0)
                {
                    dgvListLoaiPhong.DataSource = result;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy loại phòng với từ khóa {keyword}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvListLoaiPhong.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm loại phòng: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadListLoaiPhong()
        {
            loaiPhongService = new LoaiPhongService();
            dgvListLoaiPhong.DataSource = loaiPhongService.GetAllLoaiPhong();
            dgvListLoaiPhong.Columns["MaLoaiPhong"].HeaderText = "Mã Loại Phòng";
            dgvListLoaiPhong.Columns["TenLoaiPhong"].HeaderText = "Tên Loại Phòng";
            dgvListLoaiPhong.Columns["MoTa"].HeaderText = "Mô Tả";
            dgvListLoaiPhong.Columns["GiaPhong"].HeaderText = "Giá Phòng";
            dgvListLoaiPhong.Columns["SoNguoiToiDa"].HeaderText = "Số Người Tối Đa";

            dgvListLoaiPhong.Columns["HienThiMaVaTen"].Visible = false; // Ẩn cột này nếu không cần thiết
        }

        private void ClearFormLoaiPhong()
        {
            txtTenLoaiPhong.Clear();
            txtMoTa.Clear();
            txtGiaPhong.Clear();
            txtSoNguoi.Clear();
            cbLoaiPhong.SelectedIndex = -1;
        }
    }
}
