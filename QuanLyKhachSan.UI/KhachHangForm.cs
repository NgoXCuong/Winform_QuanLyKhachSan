using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.UI
{
    public partial class KhachHangForm : Form
    {
        private readonly KhachHangService khachHangService = new KhachHangService();
        public int MaKhachHangMoi { get; private set; }


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

            if (dgvKhachHang.Columns.Contains("MaKH"))
                dgvKhachHang.Columns["MaKH"].HeaderText = "Mã Khách Hàng";

            if (dgvKhachHang.Columns.Contains("HoTen"))
                dgvKhachHang.Columns["HoTen"].HeaderText = "Họ Tên";

            if (dgvKhachHang.Columns.Contains("GioiTinh"))
                dgvKhachHang.Columns["GioiTinh"].HeaderText = "Giới Tính";

            if (dgvKhachHang.Columns.Contains("NgaySinh"))
                dgvKhachHang.Columns["NgaySinh"].HeaderText = "Ngày Sinh";

            if (dgvKhachHang.Columns.Contains("CCCD"))
                dgvKhachHang.Columns["CCCD"].HeaderText = "CCCD";

            if (dgvKhachHang.Columns.Contains("SoDienThoai"))
                dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";

            if (dgvKhachHang.Columns.Contains("Email"))
                dgvKhachHang.Columns["Email"].HeaderText = "Email";

            if (dgvKhachHang.Columns.Contains("NgayTao"))
                dgvKhachHang.Columns["NgayTao"].HeaderText = "Ngày Tạo";
        }

        private void ResetKhachHang()
        {
            txtTenKhachHang.Clear();
            txtCCCD.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            rbNam.Checked = false;
            rbNu.Checked = false;
            dtNgaySinh.Value = DateTime.Now;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text) ||
                    (!rbNam.Checked && !rbNu.Checked) ||
                    string.IsNullOrWhiteSpace(txtCCCD.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KhachHangModel kh = new KhachHangModel
                {
                    HoTen = txtTenKhachHang.Text.Trim(),
                    GioiTinh = rbNam.Checked ? "Nam" : "Nữ",
                    NgaySinh = dtNgaySinh.Value,
                    CCCD = txtCCCD.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    NgayTao = DateTime.Now
                };

                bool ketQua = khachHangService.ThemKhachHang(kh);

                if (ketQua)
                {
                    // 🟢 Sau khi thêm thành công, lấy lại mã khách hàng mới
                    var khMoi = khachHangService.GetAllKhachHang()
                        .FirstOrDefault(x => x.CCCD == kh.CCCD || x.Email == kh.Email);

                    if (khMoi != null)
                    {
                        MaKhachHangMoi = khMoi.MaKH; // 🔹 Gán mã để BookingRoom biết
                    }

                    MessageBox.Show("Thêm khách hàng thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK; // 🔹 Báo cho BookingRoom biết form hoàn tất
                    this.Close(); // 🔹 Đóng form
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm khách hàng!\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvKhachHang.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text) ||
                    (!rbNam.Checked && !rbNu.Checked) ||
                    string.IsNullOrWhiteSpace(txtCCCD.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maKH = Convert.ToInt32(dgvKhachHang.CurrentRow.Cells["MaKH"].Value);

                KhachHangModel kh = new KhachHangModel
                {
                    MaKH = maKH,
                    HoTen = txtTenKhachHang.Text,
                    GioiTinh = rbNam.Checked ? "Nam" : "Nữ",
                    NgaySinh = dtNgaySinh.Value,
                    CCCD = txtCCCD.Text,
                    SoDienThoai = txtSDT.Text,
                    Email = txtEmail.Text,
                    NgayTao = DateTime.Now
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
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật khách hàng!\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvKhachHang.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa khách hàng!\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text.Trim();
                List<KhachHangModel> result = khachHangService.TimKiemKhachHang(keyword);

                if (result.Count > 0)
                {
                    dgvKhachHang.DataSource = result;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy khách hàng nào với từ khóa '{keyword}'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvKhachHang.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tìm kiếm khách hàng!\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtTenKhachHang.Text = row.Cells["HoTen"].Value?.ToString();
                rbNam.Checked = row.Cells["GioiTinh"].Value?.ToString() == "Nam";
                rbNu.Checked = row.Cells["GioiTinh"].Value?.ToString() == "Nữ";
                dtNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtCCCD.Text = row.Cells["CCCD"].Value?.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadListKhachHang();
            ResetKhachHang();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
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

                            // Data
                            for (int i = 0; i < dgvKhachHang.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvKhachHang.Columns.Count; j++)
                                {
                                    worksheet.Cell(i + 2, j + 1).Value = dgvKhachHang.Rows[i].Cells[j].Value?.ToString();
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
                MessageBox.Show("Đã xảy ra lỗi khi xuất Excel!\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
