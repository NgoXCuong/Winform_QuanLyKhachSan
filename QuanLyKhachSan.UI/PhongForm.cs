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
            LoadListLoaiPhong();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text) ||
                cbLoaiPhong.SelectedIndex == -1 ||  

                string.IsNullOrWhiteSpace(cbTrangThai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int loaiPhong = (int)cbLoaiPhong.SelectedValue;
            if (!int.TryParse(txtSoPhong.Text, out int soPhong))
            {
                MessageBox.Show("Số phòng không hợp lệ", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PhongModel phong = new PhongModel
            {
                SoPhong = soPhong,
                LoaiPhong = loaiPhong,  // Sử dụng mã loại phòng lấy từ ComboBox
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox

            // Gọi phương thức tìm kiếm từ service
            List<PhongModel> result = phongService.TimPhong(keyword);

            // Hiển thị kết quả lên DataGridView
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
                LoadListLoaiPhong(); // Refresh danh sách
                ClearFormLoaiPhong();        // Xóa thông tin trên form
            }
            else
            {
                MessageBox.Show("Thêm loại phòng thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvListLoaiPhong.CurrentRow != null)
            {
                // Xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại phòng này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int maLoaiPhong = Convert.ToInt32(dgvListLoaiPhong.CurrentRow.Cells["MaLoaiPhong"].Value);

                    bool xoaThanhCong = loaiPhongService.XoaLoaiPhong(maLoaiPhong);

                    if (xoaThanhCong)
                    {
                        MessageBox.Show("Xóa loại phòng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadListLoaiPhong(); // Refresh danh sách
                        ClearFormLoaiPhong();        // Xóa thông tin trên form
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

        private void dgvListLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void btnUpdate_Click(object sender, EventArgs e)
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

        private void btnExcel_Click(object sender, EventArgs e)
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadListLoaiPhong();
            ClearFormLoaiPhong();
        }

        private void btnSearch_Click(object sender, EventArgs e)
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

        private void LoadListLoaiPhong()
        {
            loaiPhongService = new LoaiPhongService();
            dgvListLoaiPhong.DataSource = loaiPhongService.GetAllLoaiPhong();
            dgvListLoaiPhong.Columns["MaLoaiPhong"].HeaderText = "Mã Loại Phòng";
            dgvListLoaiPhong.Columns["TenLoaiPhong"].HeaderText = "Tên Loại Phòng";
            dgvListLoaiPhong.Columns["MoTa"].HeaderText = "Mô Tả";
            dgvListLoaiPhong.Columns["GiaPhong"].HeaderText = "Giá Phòng";
            dgvListLoaiPhong.Columns["SoNguoiToiDa"].HeaderText = "Số Người Tối Đa";
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
