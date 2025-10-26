using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private bool isEditing = false;  // ✅ false = thêm mới, true = đang sửa


        public DichVuForm()
        {
            InitializeComponent();
        }

        private void LoadListDichVu()
        {
            var list = dichVuService.GetAllDichVu();

            dgvListDichVu.DataSource = null;
            dgvListDichVu.DataSource = list;

            dgvListDichVu.Columns["MaDV"].HeaderText = "Mã Dịch Vụ";
            dgvListDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            dgvListDichVu.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvListDichVu.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
            dgvListDichVu.Columns["MoTa"].HeaderText = "Mô tả";
            dgvListDichVu.Columns["Anh"].HeaderText = "Ảnh"; // Ẩn cột ảnh nếu không cần hiển thị

        }



        private void ClearFormDichVu()
        {
            txtTenDichVu.Clear();
            txtDonGia.Clear();
            txtMoTa.Clear();
            txtDonViTinh.Clear();
            picAnh.Image = null;
        }

        private void DichVuForm_Load(object sender, EventArgs e)
        {
            LoadListDichVu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                isEditing = false; // ✅ Đang thêm mới


                if (string.IsNullOrWhiteSpace(txtTenDichVu.Text) ||
                    string.IsNullOrWhiteSpace(txtDonGia.Text) ||
                    string.IsNullOrWhiteSpace(txtMoTa.Text) ||
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

                // ✅ Nếu có ảnh được chọn sẵn
                byte[] imageBytes = null;
                if (picAnh.Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        picAnh.Image.Save(ms, picAnh.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }
                }

                DichVuModel dv = new DichVuModel
                {
                    TenDichVu = txtTenDichVu.Text,
                    DonGia = donGia,
                    MoTa = txtMoTa.Text,
                    DonViTinh = txtDonViTinh.Text,
                    Anh = imageBytes
                };

                bool ketQua = dichVuService.ThemDichVu(dv);
                if (ketQua)
                {
                    MessageBox.Show("Thêm dịch vụ thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListDichVu();
                    ClearFormDichVu();
                    isEditing = false;

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
                    string.IsNullOrWhiteSpace(txtMoTa.Text))
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
                    TenDichVu = txtTenDichVu.Text,
                    DonGia = donGia,
                    MoTa = txtMoTa.Text,
                    DonViTinh = txtDonViTinh.Text
                };

                // Cập nhật ảnh (nếu có)
                if (picAnh.Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        picAnh.Image.Save(ms, picAnh.Image.RawFormat);
                        dv.Anh = ms.ToArray();  // ✅ byte[]
                    }
                }


                bool result = dichVuService.SuaDichVu(dv);

                if (result)
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListDichVu();
                    ClearFormDichVu();
                    isEditing = false;

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
                            isEditing = false;

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
                isEditing = false;

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
                if (e.RowIndex >= 0)
                {
                    isEditing = true; // ✅ Đánh dấu đang sửa

                    DataGridViewRow row = dgvListDichVu.Rows[e.RowIndex];
                    int maDV = Convert.ToInt32(row.Cells["MaDV"].Value);

                    txtTenDichVu.Text = row.Cells["TenDichVu"].Value?.ToString();
                    txtDonGia.Text = row.Cells["DonGia"].Value?.ToString();
                    txtMoTa.Text = row.Cells["MoTa"].Value?.ToString();
                    txtDonViTinh.Text = row.Cells["DonViTinh"].Value?.ToString();

                    // 🔽 Hiển thị ảnh dịch vụ
                    string base64Anh = dichVuService.LayAnhDichVu(maDV);
                    if (!string.IsNullOrEmpty(base64Anh) && IsValidBase64(base64Anh))
                    {
                        picAnh.Image = Base64ToImage(base64Anh);
                        picAnh.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        picAnh.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi chọn dòng trong danh sách!\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void linkThemAnh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Chọn ảnh dịch vụ";
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Image image = Image.FromFile(openFileDialog.FileName);
                        picAnh.Image = image;
                        picAnh.SizeMode = PictureBoxSizeMode.Zoom;

                        if (isEditing)
                        {
                            // ✅ Chỉ cho sửa ảnh khi đang ở chế độ sửa (đã chọn dịch vụ)
                            int maDV = Convert.ToInt32(dgvListDichVu.CurrentRow.Cells["MaDV"].Value);
                            if (MessageBox.Show("Bạn có muốn cập nhật ảnh cho dịch vụ đang chọn không?",
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                bool result = dichVuService.CapNhatAnh(maDV, image);
                                if (result)
                                    MessageBox.Show("Cập nhật ảnh dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Không thể cập nhật ảnh dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // ✅ Khi đang thêm mới → chỉ hiển thị ảnh
                            MessageBox.Show("Ảnh đã được chọn, bạn có thể bấm 'Thêm' để lưu cùng dịch vụ mới!",
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
                if (dgvListDichVu.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần xóa ảnh!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maDV = Convert.ToInt32(dgvListDichVu.CurrentRow.Cells["MaDV"].Value);

                if (MessageBox.Show("Bạn có chắc muốn xóa ảnh dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dichVuService.XoaAnhDichVu(maDV))
                    {
                        picAnh.Image = null;
                        MessageBox.Show("Đã xóa ảnh dịch vụ!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa ảnh!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
