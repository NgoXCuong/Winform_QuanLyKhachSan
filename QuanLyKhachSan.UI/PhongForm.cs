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
        private PhongService phongService = new PhongService();
        private LoaiPhongService loaiPhongService = new LoaiPhongService();
        private bool isEditing = false;  // ✅ false = thêm mới, true = đang sửa


        public PhongForm()
        {
            InitializeComponent();
        }

        private void PhongForm_Load(object sender, EventArgs e)
        {
            LoadLoaiPhongToComboBox();
            LoadDanhSachPhong();
            ClearFormPhong();


            LoadLoaiPhong();
            ClearFormLoaiPhong();
        }

        #region ======== PHÒNG ========

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (dgvListPhong.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = "Phong.xlsx"
            })
            {
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("LoaiPhong");

                    // Header
                    for (int i = 0; i < dgvListPhong.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dgvListPhong.Columns[i].HeaderText;
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    }

                    // Data
                    for (int i = 0; i < dgvListPhong.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvListPhong.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = dgvListPhong.Rows[i].Cells[j].Value?.ToString();
                        }
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachPhong();
                ClearFormPhong();
                isEditing = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi làm mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidatePhongInput(out string soPhong, out int maLoaiPhong, out int tang)) return;

                if (phongService.KiemTraSoPhongTonTai(soPhong))
                {
                    MessageBox.Show("Số phòng đã tồn tại!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] imageBytes = null;
                if (picAnhPhong.Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        picAnhPhong.Image.Save(ms, picAnhPhong.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }
                }

                // ✅ Gán luôn ảnh vào model
                PhongModel phong = new PhongModel
                {
                    SoPhong = soPhong,
                    MaLoaiPhong = maLoaiPhong,
                    Tang = tang,
                    TrangThai = cbTrangThai.Text,
                    Anh = imageBytes  // ✅ thêm dòng này
                };

                bool ketQua = phongService.ThemPhong(phong);

                MessageBox.Show(ketQua ? "Thêm phòng thành công!" : "Thêm phòng thất bại!",
                    Text,
                    MessageBoxButtons.OK,
                    ketQua ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (ketQua)
                {
                    LoadDanhSachPhong();
                    ClearFormPhong();
                    isEditing = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListPhong.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn phòng để sửa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidatePhongInput(out string soPhong, out int maLoaiPhong, out int tang)) return;

                DataGridViewRow row = dgvListPhong.SelectedRows[0];
                PhongModel phong = new PhongModel
                {
                    MaPhong = Convert.ToInt32(row.Cells["MaPhong"].Value),
                    SoPhong = soPhong,
                    MaLoaiPhong = maLoaiPhong,
                    Tang = tang,
                    TrangThai = cbTrangThai.Text
                };


                bool success = phongService.SuaPhong(phong);
                MessageBox.Show(success ? "Sửa phòng thành công!" : "Sửa phòng thất bại!", Text,
                    MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                LoadDanhSachPhong();
                ClearFormPhong();
                isEditing = false;
                picAnhPhong.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListPhong.CurrentRow != null)
                {
                    // Xác nhận trước khi xóa
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int maPhong = Convert.ToInt32(dgvListPhong.SelectedRows[0].Cells["MaPhong"].Value);
                        bool ketQua = phongService.XoaPhong(maPhong);
                        MessageBox.Show(ketQua ? "Xóa phòng thành công!" : "Xóa phòng thất bại!", Text,
                            MessageBoxButtons.OK, ketQua ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        LoadDanhSachPhong();
                        ClearFormPhong();
                        isEditing = false;
                        picAnhPhong.Image = null;
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một phòng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text.Trim();
                List<PhongModel> result = phongService.TimPhong(keyword);

                if (result.Count > 0)
                    dgvListPhong.DataSource = result;
                else
                {
                    MessageBox.Show($"Không tìm thấy phòng với từ khóa {keyword}!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvListPhong.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dgvListPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    isEditing = true;

                    DataGridViewRow row = dgvListPhong.Rows[e.RowIndex];
                    txtSoPhong.Text = row.Cells["SoPhong"].Value?.ToString();
                    cbLoaiPhong.SelectedValue = Convert.ToInt32(row.Cells["MaLoaiPhong"].Value);
                    numTang.Text = row.Cells["Tang"].Value?.ToString();
                    cbTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();

                    int maPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);

                    string base64Anh = phongService.LayAnhPhong(maPhong);

                    if (!string.IsNullOrEmpty(base64Anh) && IsValidBase64(base64Anh))
                    {
                        picAnhPhong.Image = Base64ToImage(base64Anh);
                        picAnhPhong.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        picAnhPhong.Image = null;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Lỗi khi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        private void LoadDanhSachPhong()
        {
            dgvListPhong.DataSource = phongService.GetAllPhong();

            dgvListPhong.Columns["MaPhong"].HeaderText = "Mã Phòng";
            dgvListPhong.Columns["SoPhong"].HeaderText = "Số Phòng";
            dgvListPhong.Columns["MaLoaiPhong"].Visible = false; // ẩn nếu không cần
            dgvListPhong.Columns["TenLoaiPhong"].HeaderText = "Loại Phòng";
            dgvListPhong.Columns["Tang"].HeaderText = "Tầng";
            dgvListPhong.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvListPhong.Columns["Anh"].HeaderText = "Ảnh Phòng";
        }


        private void ClearFormPhong()
        {
            txtSoPhong.Clear();
            cbLoaiPhong.SelectedIndex = -1;
            cbTrangThai.SelectedIndex = -1;
            numTang.Value = 0; // nếu dùng NumericUpDown
            picAnhPhong.Image = null; // ✅ chỉ xóa ảnh, không xóa control
        }


        private bool ValidatePhongInput(out string soPhong, out int maLoaiPhong, out int tang)
        {
            soPhong = txtSoPhong.Text.Trim();
            maLoaiPhong = 0;
            tang = 0;

            if (string.IsNullOrWhiteSpace(soPhong) ||
                cbLoaiPhong.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(numTang.Text) ||
                string.IsNullOrWhiteSpace(cbTrangThai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(numTang.Text, out tang))
            {
                MessageBox.Show("Tầng phải là số hợp lệ!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cbLoaiPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng hợp lệ!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            maLoaiPhong = (int)cbLoaiPhong.SelectedValue;
            return true;
        }

        private void LoadLoaiPhongToComboBox()
        {
            var danhSachLoaiPhong = loaiPhongService.GetLoaiPhongByIdName();
            cbLoaiPhong.DataSource = danhSachLoaiPhong;
            cbLoaiPhong.DisplayMember = "HienThiMaVaTen";
            cbLoaiPhong.ValueMember = "MaLoaiPhong";
        }


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

        private void LoadLoaiPhong()
        {
            dgvListLoaiPhong.DataSource = loaiPhongService.GetAllLoaiPhong();

            if (dgvListLoaiPhong.Columns.Contains("HienThiMaVaTen"))
                dgvListLoaiPhong.Columns["HienThiMaVaTen"].Visible = false; // ✅ Ẩn cột tính toán

            dgvListLoaiPhong.Columns["MaLoaiPhong"].HeaderText = "Mã loại phòng";
            dgvListLoaiPhong.Columns["TenLoaiPhong"].HeaderText = "Tên loại phòng";
            dgvListLoaiPhong.Columns["GiaCoBan"].HeaderText = "Giá cơ bản";
            dgvListLoaiPhong.Columns["SucChuaToiDa"].HeaderText = "Sức chứa tối đa";
            dgvListLoaiPhong.Columns["MoTa"].HeaderText = "Mô tả";
        }


        private void ClearFormLoaiPhong()
        {
            txtTenLoaiPhong.Clear();
            txtGiaPhong.Clear();
            txtSoNguoi.Clear();
            txtMoTa.Clear();
            txtTim.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                LoaiPhongModel lp = new LoaiPhongModel
                {
                    TenLoaiPhong = txtTenLoaiPhong.Text,
                    GiaCoBan = decimal.TryParse(txtGiaPhong.Text, out decimal gia) ? gia : 0,
                    SucChuaToiDa = int.TryParse(txtSoNguoi.Text, out int sc) ? sc : 0,
                    MoTa = txtMoTa.Text
                };

                if (loaiPhongService.ThemLoaiPhong(lp))
                {
                    MessageBox.Show("Thêm loại phòng thành công!");
                    LoadLoaiPhong(); // Hàm load lại DataGridView
                    ClearFormLoaiPhong();
                }
                else
                {
                    MessageBox.Show("Thêm loại phòng thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvListLoaiPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn loại phòng để sửa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvListLoaiPhong.SelectedRows[0];
            int maLoai = Convert.ToInt32(row.Cells["MaLoaiPhong"].Value);

            LoaiPhongModel lp = new LoaiPhongModel
            {
                MaLoaiPhong = maLoai,
                TenLoaiPhong = txtTenLoaiPhong.Text,
                GiaCoBan = decimal.TryParse(txtGiaPhong.Text, out decimal gia) ? gia : 0,
                SucChuaToiDa = int.TryParse(txtSoNguoi.Text, out int sc) ? sc : 0,
                MoTa = txtMoTa.Text
            };

            bool success = loaiPhongService.SuaLoaiPhong(lp);
            MessageBox.Show(success ? "Cập nhật thành công!" : "Cập nhật thất bại!",
                            Text, MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            LoadLoaiPhong();
            ClearFormLoaiPhong();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvListLoaiPhong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn loại phòng để xóa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maLoai = Convert.ToInt32(dgvListLoaiPhong.SelectedRows[0].Cells["MaLoaiPhong"].Value);
            var result = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bool success = loaiPhongService.XoaLoaiPhong(maLoai);
                MessageBox.Show(success ? "Xóa thành công!" : "Xóa thất bại!",
                                Text, MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                LoadLoaiPhong();
                ClearFormLoaiPhong();
            }
        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvListLoaiPhong.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = "LoaiPhong.xlsx"
            })
            {
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                using (var workbook = new ClosedXML.Excel.XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("LoaiPhong");

                    // Header
                    for (int i = 0; i < dgvListLoaiPhong.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dgvListLoaiPhong.Columns[i].HeaderText;
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    }

                    // Data
                    for (int i = 0; i < dgvListLoaiPhong.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvListLoaiPhong.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = dgvListLoaiPhong.Rows[i].Cells[j].Value?.ToString();
                        }
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFormLoaiPhong();
            LoadLoaiPhong();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            var list = loaiPhongService.TimLoaiPhong(keyword);

            if (list.Count > 0)
            {
                dgvListLoaiPhong.DataSource = list;
            }
            else
            {
                MessageBox.Show($"Không tìm thấy loại phòng với từ khóa '{keyword}'!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvListLoaiPhong.DataSource = null;
            }
        }

        private void dgvListLoaiPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvListLoaiPhong.Rows[e.RowIndex];
            // txtMaLoaiPhong bỏ đi
            txtTenLoaiPhong.Text = row.Cells["TenLoaiPhong"].Value?.ToString();
            txtGiaPhong.Text = row.Cells["GiaCoBan"].Value?.ToString();
            txtSoNguoi.Text = row.Cells["SucChuaToiDa"].Value?.ToString();
            txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
        }


    }
}
