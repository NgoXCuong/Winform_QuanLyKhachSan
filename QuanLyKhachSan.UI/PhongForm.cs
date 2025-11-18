using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ClosedXML.Excel;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.UI
{
    public partial class PhongForm : Form
    {
        private readonly PhongService phongService = new PhongService();
        private bool isEditing = false; // false = thêm mới, true = đang sửa

        public PhongForm()
        {
            InitializeComponent();
        }

        private void PhongForm_Load(object sender, EventArgs e)
        {
            LoadDanhSachPhong();
            ClearFormPhong();
        }

        #region ======== PHÒNG ========

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDanhSachPhong();
            ClearFormPhong();
        }

        private void LoadDanhSachPhong()
        {
            dgvListPhong.DataSource = phongService.GetAllPhong();
            dgvListPhong.Columns["MaPhong"].HeaderText = "Mã Phòng";
            dgvListPhong.Columns["SoPhong"].HeaderText = "Số Phòng";
            dgvListPhong.Columns["TenLoaiPhong"].HeaderText = "Loại Phòng";
            dgvListPhong.Columns["GiaPhong"].HeaderText = "Giá Phòng";
            dgvListPhong.Columns["SucChuaToiDa"].HeaderText = "Sức Chứa";
            dgvListPhong.Columns["Tang"].HeaderText = "Tầng";
            dgvListPhong.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvListPhong.Columns["MoTa"].HeaderText = "Mô Tả";
            dgvListPhong.Columns["Anh"].HeaderText = "Ảnh Phòng";
        }

        private void ClearFormPhong()
        {
            txtSoPhong.Clear();
            txtTenLoaiPhong.Clear();
            txtGiaPhong.Clear();
            txtSoNguoi.Clear();
            numTang.Value = 0;
            cbTrangThai.SelectedIndex = -1;
            txtMoTa.Clear();
            picAnhPhong.Image = null;
            isEditing = false;
        }

        private bool ValidatePhongInput(out string soPhong, out string tenLoai, out decimal gia, out int sucChua, out int tang, out string trangThai)
        {
            soPhong = txtSoPhong.Text.Trim();
            tenLoai = txtTenLoaiPhong.Text.Trim();
            gia = decimal.TryParse(txtGiaPhong.Text.Trim(), out decimal g) ? g : 0;
            sucChua = int.TryParse(txtSoNguoi.Text.Trim(), out int sc) ? sc : 0;
            tang = (int)numTang.Value;
            trangThai = cbTrangThai.Text;

            if (string.IsNullOrWhiteSpace(soPhong) || string.IsNullOrWhiteSpace(tenLoai) || gia <= 0 || sucChua <= 0 || string.IsNullOrWhiteSpace(trangThai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và hợp lệ thông tin phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidatePhongInput(out string soPhong, out string tenLoai, out decimal gia, out int sucChua, out int tang, out string trangThai))
                return;

            if (phongService.KiemTraSoPhongTonTai(soPhong))
            {
                MessageBox.Show("Số phòng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            byte[] imageBytes = picAnhPhong.Image != null ? ImageToByte(picAnhPhong.Image) : null;

            PhongModel phong = new PhongModel
            {
                SoPhong = soPhong,
                TenLoaiPhong = tenLoai,
                GiaPhong = gia,
                SucChuaToiDa = sucChua,
                Tang = tang,
                TrangThai = trangThai,
                Anh = imageBytes,
                MoTa = txtMoTa.Text
            };

            bool result = phongService.ThemPhong(phong);
            MessageBox.Show(result ? "Thêm phòng thành công!" : "Thêm phòng thất bại!", "Thông báo", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result)
            {
                LoadDanhSachPhong();
                ClearFormPhong();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvListPhong.SelectedRows.Count == 0) { MessageBox.Show("Chọn phòng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (!ValidatePhongInput(out string soPhong, out string tenLoai, out decimal gia, out int sucChua, out int tang, out string trangThai))
                return;

            DataGridViewRow row = dgvListPhong.SelectedRows[0];
            int maPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);

            PhongModel phong = new PhongModel
            {
                MaPhong = maPhong,
                SoPhong = soPhong,
                TenLoaiPhong = tenLoai,
                GiaPhong = gia,
                SucChuaToiDa = sucChua,
                Tang = tang,
                TrangThai = trangThai,
                Anh = picAnhPhong.Image != null ? ImageToByte(picAnhPhong.Image) : null,
                MoTa = txtMoTa.Text
            };

            bool result = phongService.SuaPhong(phong);
            MessageBox.Show(result ? "Sửa phòng thành công!" : "Sửa phòng thất bại!", "Thông báo", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result)
            {
                LoadDanhSachPhong();
                ClearFormPhong();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvListPhong.SelectedRows.Count == 0) { MessageBox.Show("Chọn phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            int maPhong = Convert.ToInt32(dgvListPhong.SelectedRows[0].Cells["MaPhong"].Value);

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool result = phongService.XoaPhong(maPhong);
                MessageBox.Show(result ? "Xóa phòng thành công!" : "Xóa phòng thất bại!", "Thông báo", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result) { LoadDanhSachPhong(); ClearFormPhong(); }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTim.Text.Trim();
            List<PhongModel> result = phongService.TimPhong(keyword);
            dgvListPhong.DataSource = result.Count > 0 ? result : null;
            if (result.Count == 0)
                MessageBox.Show($"Không tìm thấy phòng với từ khóa '{keyword}'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvListPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            isEditing = true;

            DataGridViewRow row = dgvListPhong.Rows[e.RowIndex];
            txtSoPhong.Text = row.Cells["SoPhong"].Value?.ToString();
            txtTenLoaiPhong.Text = row.Cells["TenLoaiPhong"].Value?.ToString();
            txtGiaPhong.Text = row.Cells["GiaPhong"].Value?.ToString();
            txtSoNguoi.Text = row.Cells["SucChuaToiDa"].Value?.ToString();
            numTang.Value = Convert.ToDecimal(row.Cells["Tang"].Value);
            cbTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();

            string base64 = phongService.LayAnhPhong(Convert.ToInt32(row.Cells["MaPhong"].Value));
            picAnhPhong.Image = !string.IsNullOrEmpty(base64) ? Base64ToImage(base64) : null;
            picAnhPhong.SizeMode = PictureBoxSizeMode.Zoom;
        }

        public Image Base64ToImage(string base64String)
        {
            try
            {
                base64String = base64String.Trim(); // Loại bỏ khoảng trắng thừa
                base64String = base64String.Replace(" ", "+"); // Thay thế dấu cách bằng dấu "+" nếu có

                // Thêm padding nếu thiếu
                int mod4 = base64String.Length % 4;
                if (mod4 > 0)
                {
                    base64String = base64String.PadRight(base64String.Length + (4 - mod4), '=');
                }

                // Giải mã chuỗi Base64 thành mảng byte
                byte[] imageBytes = Convert.FromBase64String(base64String);

                using (var ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Chuỗi Base64 không hợp lệ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Trả về null nếu không thể giải mã
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuyển đổi ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private byte[] ImageToByte(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }


        bool IsValidBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch
            {
                return false;
            }
        }

       


        //private bool ValidatePhongInput(out string soPhong, out int maLoaiPhong, out int tang)
        //{
        //    soPhong = txtSoPhong.Text.Trim();
        //    maLoaiPhong = 0;
        //    tang = 0;

        //    if (string.IsNullOrWhiteSpace(soPhong) ||
        //        cbLoaiPhong.SelectedIndex == -1 ||
        //        string.IsNullOrWhiteSpace(numTang.Text) ||
        //        string.IsNullOrWhiteSpace(cbTrangThai.Text))
        //    {
        //        MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }

        //    if (!int.TryParse(numTang.Text, out tang))
        //    {
        //        MessageBox.Show("Tầng phải là số hợp lệ!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }

        //    if (cbLoaiPhong.SelectedValue == null)
        //    {
        //        MessageBox.Show("Vui lòng chọn loại phòng hợp lệ!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }

        //    maLoaiPhong = (int)cbLoaiPhong.SelectedValue;
        //    return true;
        //}

        


        private void linkThemAnh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Chọn ảnh phòng";
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Image image = Image.FromFile(openFileDialog.FileName);
                        picAnhPhong.Image = image;
                        picAnhPhong.SizeMode = PictureBoxSizeMode.Zoom;

                        if (isEditing)
                        {
                            // ✅ Chỉ cho sửa ảnh khi đang ở chế độ sửa (đã chọn dịch vụ)
                            int maPhong = Convert.ToInt32(dgvListPhong.CurrentRow.Cells["MaPhong"].Value);
                            if (MessageBox.Show("Bạn có muốn cập nhật ảnh cho phòng đang chọn không?",
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                bool result = phongService.CapNhatAnh(maPhong, image);
                                if (result)
                                    MessageBox.Show("Cập nhật ảnh phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Không thể cập nhật ảnh phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // ✅ Khi đang thêm mới → chỉ hiển thị ảnh
                            MessageBox.Show("Ảnh đã được chọn, bạn có thể bấm 'Thêm' để lưu cùng phòng mới!",
                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkXoaAnh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvListPhong.CurrentRow != null)
                {
                    int maPhong = Convert.ToInt32(dgvListPhong.CurrentRow.Cells["MaPhong"].Value);

                    if (MessageBox.Show("Bạn có chắc muốn xóa ảnh của phòng này?",
                                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool result = phongService.XoaAnhPhong(maPhong);
                        if (result)
                        {
                            picAnhPhong.Image = null;
                            MessageBox.Show("Đã xóa ảnh phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa ảnh phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn phòng cần xóa ảnh.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa ảnh phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region ======== HELPER ========

        private void ExportDataGridViewToExcel(DataGridView dgv, string defaultFileName, string sheetName)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Workbook|*.xlsx";
                saveFileDialog.Title = "Lưu file Excel";
                saveFileDialog.FileName = defaultFileName;

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(sheetName);

                    // Header
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightSteelBlue;
                        worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    }

                    // Data
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = dgv.Rows[i].Cells[j].Value?.ToString();
                            worksheet.Cell(i + 2, j + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                            worksheet.Cell(i + 2, j + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                        }
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion
        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (dgvListPhong.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu để xuất!"); return; }

            using (SaveFileDialog save = new SaveFileDialog { Filter = "Excel Workbook|*.xlsx", FileName = "Phong.xlsx" })
            {
                if (save.ShowDialog() != DialogResult.OK) return;

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Phong");
                    for (int i = 0; i < dgvListPhong.Columns.Count; i++)
                        ws.Cell(1, i + 1).Value = dgvListPhong.Columns[i].HeaderText;

                    for (int i = 0; i < dgvListPhong.Rows.Count; i++)
                        for (int j = 0; j < dgvListPhong.Columns.Count; j++)
                            ws.Cell(i + 2, j + 1).Value = dgvListPhong.Rows[i].Cells[j].Value?.ToString();

                    ws.Columns().AdjustToContents();
                    wb.SaveAs(save.FileName);
                    MessageBox.Show("Xuất Excel thành công!");
                }
            }
        }
    }
}
