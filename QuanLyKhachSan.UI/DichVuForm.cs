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
    public partial class DichVuForm : Form
    {
        private DichVuService dichVuService = new DichVuService();

        public DichVuForm()
        {
            InitializeComponent();
        }

        private void LoadListDichVu()
        {
            dgvListDichVu.DataSource = dichVuService.GetAllDichVu();
            dgvListDichVu.Columns["MaDV"].HeaderText = "Mã Dịch Vụ";
            dgvListDichVu.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";
            dgvListDichVu.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvListDichVu.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
        }

        private void ClearFormDichVu()
        {
            txtTenDichVu.Clear();
            txtDonGia.Clear();
            txtDonViTinh.Clear();
        }

        private void DichVuForm_Load(object sender, EventArgs e)
        {
            LoadListDichVu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenDichVu.Text) ||
                    string.IsNullOrWhiteSpace(txtDonGia.Text) ||
                    string.IsNullOrWhiteSpace(txtDonViTinh.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin dịch vụ", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDonGia.Text, out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DichVuModel dv = new DichVuModel
                {
                    TenDV = txtTenDichVu.Text,
                    DonGia = donGia,
                    DonViTinh = txtDonViTinh.Text
                };

                bool ketQua = dichVuService.ThemDichVu(dv);
                if (ketQua)
                {
                    MessageBox.Show("Thêm dịch vụ thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListDichVu();
                    ClearFormDichVu();
                }
                else
                {
                    MessageBox.Show("Thêm dịch vụ thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListDichVu.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần sửa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenDichVu.Text) ||
                    string.IsNullOrWhiteSpace(txtDonGia.Text) ||
                    string.IsNullOrWhiteSpace(txtDonViTinh.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDonGia.Text, out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maDV = Convert.ToInt32(dgvListDichVu.CurrentRow.Cells["MaDV"].Value);

                DichVuModel dv = new DichVuModel
                {
                    MaDV = maDV,
                    TenDV = txtTenDichVu.Text,
                    DonGia = donGia,
                    DonViTinh = txtDonViTinh.Text
                };

                bool result = dichVuService.SuaDichVu(dv);

                if (result)
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListDichVu();
                    ClearFormDichVu();
                }
                else
                {
                    MessageBox.Show("Cập nhật dịch vụ thất bại!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListDichVu.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int maDV = Convert.ToInt32(dgvListDichVu.CurrentRow.Cells["MaDV"].Value);

                        bool xoaThanhCong = dichVuService.XoaDichVu(maDV);

                        if (xoaThanhCong)
                        {
                            MessageBox.Show("Xóa dịch vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadListDichVu();
                            ClearFormDichVu();
                        }
                        else
                        {
                            MessageBox.Show("Xóa dịch vụ thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một dịch vụ để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListDichVu.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Workbook|*.xlsx";
                    saveFileDialog.Title = "Lưu file Excel";
                    saveFileDialog.FileName = "DanhSachDichVu.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Dịch Vụ");

                            for (int i = 0; i < dgvListDichVu.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvListDichVu.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                                worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }

                            for (int i = 0; i < dgvListDichVu.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvListDichVu.Columns.Count; j++)
                                {
                                    object value = dgvListDichVu.Rows[i].Cells[j].Value;
                                    worksheet.Cell(i + 2, j + 1).Value = value?.ToString();
                                    worksheet.Cell(i + 2, j + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    worksheet.Cell(i + 2, j + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                }
                            }

                            worksheet.Columns().AdjustToContents();
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xuất Excel thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadListDichVu();
                ClearFormDichVu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải danh sách dịch vụ!\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvListDichVu.Rows.Count)
                {
                    DataGridViewRow row = dgvListDichVu.Rows[e.RowIndex];
                    txtTenDichVu.Text = row.Cells["TenDV"].Value?.ToString();
                    txtDonGia.Text = row.Cells["DonGia"].Value?.ToString();
                    txtDonViTinh.Text = row.Cells["DonViTinh"].Value?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi chọn dòng trong danh sách!\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text.Trim();

                List<DichVuModel> result = dichVuService.TimKiemDichVu(keyword);

                if (result.Count > 0)
                {
                    dgvListDichVu.DataSource = result;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy dịch vụ nào có từ khóa {keyword}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvListDichVu.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tìm kiếm dịch vụ!\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
