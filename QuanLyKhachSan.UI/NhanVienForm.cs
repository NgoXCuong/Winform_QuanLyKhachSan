using ClosedXML.Excel;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;
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

namespace QuanLyKhachSan.UI
{
    public partial class NhanVienForm : Form
    {
        private NhanVienService nhanVienService = new NhanVienService();
        private TaiKhoanService taiKhoanService = new TaiKhoanService();
        private bool isEditing = false;  // ✅ false = thêm mới, true = đang sửa


        public NhanVienForm()
        {
            InitializeComponent();
        }

        private void NhanVienForm_Load(object sender, EventArgs e)
        {
            //  Tab Nhan Vien
            LoadListNhanVien();
            ResetNhanVien();

            //  Tab Tai Khoan
            LoadDanhSachTaiKhoan();
            ResetTaiKhoan();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                (!rbNam.Checked && !rbNu.Checked) ||
                string.IsNullOrWhiteSpace(txtChucVu.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string gioiTinh = rbNam.Checked ? "Nam" : "Nữ";

                NhanVienModel nv = new NhanVienModel
                {
                    HoTen = txtHoTen.Text,
                    GioiTinh = gioiTinh,
                    NgaySinh = dtNgaySinh.Value,
                    ChucVu = txtChucVu.Text,
                    SoDienThoai = txtSDT.Text,
                    Email = txtEmail.Text
                };

                byte[] imageBytes = null;
                if (picAnhNhanVien.Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        picAnhNhanVien.Image.Save(ms, picAnhNhanVien.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }
                }

                bool ketQua = nhanVienService.ThemNhanVien(nv);

                if (ketQua)
                {
                    MessageBox.Show("Thêm nhân viên thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListNhanVien();
                    ResetNhanVien();
                    isEditing = false;
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListNhanVien.CurrentRow != null)
                {
                    // Xác nhận trước khi xóa
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int maNV = Convert.ToInt32(dgvListNhanVien.CurrentRow.Cells["MaNV"].Value);

                        bool xoaThanhCong = nhanVienService.XoaNhanVien(maNV);

                        if (xoaThanhCong)
                        {
                            MessageBox.Show("Xóa nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadListNhanVien();
                            ResetNhanVien();
                            isEditing = false;
                            picAnhNhanVien.Image = null;
                        }
                        else
                        {
                            MessageBox.Show("Xóa nhân viên thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListNhanVien.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    (!rbNam.Checked && !rbNu.Checked) ||
                    string.IsNullOrWhiteSpace(txtChucVu.Text) ||
                    string.IsNullOrWhiteSpace(txtSDT.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maNV = Convert.ToInt32(dgvListNhanVien.CurrentRow.Cells["MaNV"].Value);
                string gioiTinh = rbNam.Checked ? "Nam" : "Nữ";

                NhanVienModel nv = new NhanVienModel
                {
                    MaNV = maNV,
                    HoTen = txtHoTen.Text,
                    GioiTinh = gioiTinh,
                    NgaySinh = dtNgaySinh.Value,
                    ChucVu = txtChucVu.Text,
                    SoDienThoai = txtSDT.Text,
                    Email = txtEmail.Text
                };

                bool result = nhanVienService.SuaNhanVien(nv);

                if (result)
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListNhanVien();
                    ResetNhanVien();
                    isEditing = false;
                    picAnhNhanVien.Image = null;
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListNhanVien.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Workbook|*.xlsx";
                    saveFileDialog.Title = "Lưu file Excel";
                    saveFileDialog.FileName = "DanhSachNhanVien.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Nhân viên");

                            // Header
                            for (int i = 0; i < dgvListNhanVien.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvListNhanVien.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                                worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }

                            // Dữ liệu
                            for (int i = 0; i < dgvListNhanVien.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvListNhanVien.Columns.Count; j++)
                                {
                                    object value = dgvListNhanVien.Rows[i].Cells[j].Value;
                                    // Nếu là ảnh hoặc byte[], hiển thị là "[Ảnh]"
                                    if (value is byte[])
                                    {
                                        worksheet.Cell(i + 2, j + 1).Value = "[Ảnh]";
                                    }
                                    else
                                    {
                                        worksheet.Cell(i + 2, j + 1).Value = value?.ToString() ?? "";
                                    }
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
                MessageBox.Show("Đã xảy ra lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadListNhanVien();
                ResetNhanVien();
                isEditing = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi làm mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text.Trim();

                List<NhanVienModel> result = nhanVienService.TimNhanVien(keyword);

                if (result.Count > 0)
                {
                    dgvListNhanVien.DataSource = result;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy nhân viên nào với từ khóa {keyword}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvListNhanVien.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadListNhanVien()
        {
            dgvListNhanVien.DataSource = nhanVienService.GetAllNhanVien();
            dgvListNhanVien.Columns["MaNV"].HeaderText = "Mã nhân viên";
            dgvListNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
            dgvListNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
            dgvListNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dgvListNhanVien.Columns["ChucVu"].HeaderText = "Chức vụ";
            dgvListNhanVien.Columns["SoDienThoai"].HeaderText = "SĐT";
            dgvListNhanVien.Columns["Email"].HeaderText = "Email";
            dgvListNhanVien.Columns["Anh"].HeaderText = "Ảnh nhân viên";
            dgvListNhanVien.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvListNhanVien.Columns["HienThiMaVaTen"].Visible = false; // Ẩn cột này nếu không cần thiết
        }

        private void ResetNhanVien()
        {
            txtHoTen.Clear();
            rbNam.Checked = false;
            rbNu.Checked = false;
            dtNgaySinh.Value = DateTime.Now;
            txtChucVu.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            picAnhNhanVien.Image = null; // Xóa ảnh
        }

        private void linkThemAnh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Chọn ảnh nhân viên";
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Image image = Image.FromFile(openFileDialog.FileName);
                        picAnhNhanVien.Image = image;
                        picAnhNhanVien.SizeMode = PictureBoxSizeMode.Zoom;

                        if (isEditing)
                        {
                            // ✅ Chỉ cho sửa ảnh khi đang ở chế độ sửa (đã chọn dịch vụ)
                            int maNV = Convert.ToInt32(dgvListNhanVien.CurrentRow.Cells["MaNV"].Value);
                            if (MessageBox.Show("Bạn có muốn cập nhật ảnh cho nhân viên đang chọn không?",
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                bool result = nhanVienService.CapNhatAnhNhanVien(maNV, image);
                                if (result)
                                    MessageBox.Show("Cập nhật ảnh nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Không thể cập nhật ảnh nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // ✅ Khi đang thêm mới → chỉ hiển thị ảnh
                            MessageBox.Show("Ảnh đã được chọn, bạn có thể bấm 'Thêm' để lưu cùng nhân viên mới!",
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



        private void dgvListNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    isEditing = true; // ✅ Đánh dấu đang sửa

                    DataGridViewRow row = dgvListNhanVien.Rows[e.RowIndex];

                    txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                    rbNam.Checked = row.Cells["GioiTinh"].Value.ToString() == "Nam";
                    rbNu.Checked = row.Cells["GioiTinh"].Value.ToString() == "Nữ";
                    dtNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    txtChucVu.Text = row.Cells["ChucVu"].Value.ToString();
                    txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();

                    int maNV = Convert.ToInt32(row.Cells["MaNV"].Value);

                    string base64Anh = nhanVienService.LayAnhNhanVien(maNV);

                    if (!string.IsNullOrEmpty(base64Anh) && IsValidBase64(base64Anh))
                    {
                        picAnhNhanVien.Image = Base64ToImage(base64Anh);
                        picAnhNhanVien.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        picAnhNhanVien.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
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

        private void linkXoaAnh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvListNhanVien.CurrentRow != null)
                {
                    int maNV = Convert.ToInt32(dgvListNhanVien.CurrentRow.Cells["MaNV"].Value);

                    bool result = nhanVienService.XoaAnhNhanVien(maNV);

                    if (result)
                    {
                        picAnhNhanVien.Image = null; // Xóa ảnh trên giao diện
                        MessageBox.Show("Đã xóa ảnh nhân viên.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa ảnh thất bại.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa ảnh.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa ảnh nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  TAI KHOAN NHAN VIEN
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                    string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                    string.IsNullOrWhiteSpace(cbQuyen.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbMaNhanVien.SelectedItem == null || cbMaNhanVien.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!rbKichHoat.Checked && !rbChuaKichHoat.Checked)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái hoạt động", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maNV;
                if (!int.TryParse(cbMaNhanVien.SelectedValue.ToString(), out maNV))
                {
                    MessageBox.Show("Giá trị mã nhân viên không hợp lệ.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TaiKhoanModel tk = new TaiKhoanModel
                {
                    TenDangNhap = txtTenDangNhap.Text.Trim(),
                    MatKhau = txtMatKhau.Text.Trim(),
                    MaNV = maNV,
                    VaiTro = cbQuyen.Text.Trim(),
                    TrangThai = rbKichHoat.Checked ? "Hoạt động" : "Ngưng hoạt động"
                };

                if (taiKhoanService.KiemTraTonTaiMaNV(maNV))
                {
                    MessageBox.Show("Nhân viên này đã có tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (taiKhoanService.ThemTaiKhoan(tk))
                {
                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachTaiKhoan();
                    ResetTaiKhoan();
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại. Kiểm tra lại dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListTaiKhoan.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần sửa!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                    string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                    string.IsNullOrWhiteSpace(cbMaNhanVien.SelectedIndex.ToString()) ||
                    (!rbKichHoat.Checked && !rbChuaKichHoat.Checked))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string trangThai = rbKichHoat.Checked ? "Kích hoạt" : "Chưa kích hoạt";

                TaiKhoanModel tk = new TaiKhoanModel
                {
                    TenDangNhap = txtTenDangNhap.Text,
                    MatKhau = txtMatKhau.Text.Trim(),
                    MaNV = Convert.ToInt32(cbMaNhanVien.SelectedValue),
                    VaiTro = cbQuyen.Text,
                    TrangThai = rbKichHoat.Checked ? "Hoạt động" : "Ngưng hoạt động"
                };

                bool result = taiKhoanService.SuaTaiKhoan(tk);

                if (result)
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachTaiKhoan();
                    ResetTaiKhoan();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListTaiKhoan.CurrentRow != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string tenDangNhap = dgvListTaiKhoan.CurrentRow.Cells["TenDangNhap"].Value.ToString();

                        bool xoaThanhCong = taiKhoanService.XoaTaiKhoan(tenDangNhap);

                        if (xoaThanhCong)
                        {
                            MessageBox.Show("Xóa tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachTaiKhoan();
                            ResetTaiKhoan();
                        }
                        else
                        {
                            MessageBox.Show("Xóa tài khoản thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một tài khoản để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachTaiKhoan();
                ResetTaiKhoan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi làm mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox

                List<TaiKhoanModel> result = taiKhoanService.TimKiemTaiKhoan(keyword);

                if (result.Count > 0)
                {
                    dgvListTaiKhoan.DataSource = result;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy tài khoản nào với từ khóa {keyword}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvListTaiKhoan.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListTaiKhoan.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Workbook|*.xlsx";
                    saveFileDialog.Title = "Lưu file Excel";
                    saveFileDialog.FileName = "DanhSachTaiKhoan.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Tài khoản");

                            // Header
                            for (int i = 0; i < dgvListTaiKhoan.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvListTaiKhoan.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                                worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                            // Dữ liệu
                            for (int i = 0; i < dgvListTaiKhoan.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvListTaiKhoan.Columns.Count; j++)
                                {
                                    object value = dgvListTaiKhoan.Rows[i].Cells[j].Value;
                                    worksheet.Cell(i + 2, j + 1).Value = value == DBNull.Value ? string.Empty : value.ToString();
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
                MessageBox.Show("Đã xảy ra lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadDanhSachTaiKhoan()
        {
            dgvListTaiKhoan.DataSource = taiKhoanService.GetAllTaiKhoan();
            dgvListTaiKhoan.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
            dgvListTaiKhoan.Columns["MatKhau"].HeaderText = "Mật khẩu";
            dgvListTaiKhoan.Columns["MaNV"].HeaderText = "Mã nhân viên";
            dgvListTaiKhoan.Columns["VaiTro"].HeaderText = "Vai trò";
            dgvListTaiKhoan.Columns["TrangThai"].HeaderText = "Trạng thái";

            cbQuyen.Items.Clear();
            cbQuyen.Items.Add("Admin");
            cbQuyen.Items.Add("Nhân viên");
            cbQuyen.DropDownStyle = ComboBoxStyle.DropDownList; // chỉ chọn, không nhập


            // Load danh sách nhân viên vào ComboBox
            var dsNhanVien = nhanVienService.GetNhanVienByIdName(); // Trả về List<NhanVienModel>

            cbMaNhanVien.DataSource = dsNhanVien;
            cbMaNhanVien.DisplayMember = "HienThiMaVaTen";
            cbMaNhanVien.ValueMember = "MaNV";
        }

        private void ResetTaiKhoan()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();

            // Reset quyền về trống hoặc giá trị mặc định
            cbQuyen.SelectedIndex = -1; // hoặc cbQuyen.Text = "";

            // Reset trạng thái hoạt động
            rbKichHoat.Checked = false;
            rbChuaKichHoat.Checked = false;

            // Reset nhân viên
            cbMaNhanVien.SelectedIndex = -1;
        }


        private void cbMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbMaNhanVien.SelectedItem is NhanVienModel selectedNV)
                {
                    string chucVu = nhanVienService.GetChucVuByMaNV(selectedNV.MaNV);

                    if (string.IsNullOrEmpty(chucVu))
                        cbQuyen.SelectedItem = null;
                    else if (chucVu.ToLower().Contains("admin") || chucVu.ToLower().Contains("quản lý"))
                        cbQuyen.SelectedItem = "Admin";
                    else
                        cbQuyen.SelectedItem = "Nhân viên";
                }
                else
                {
                    cbQuyen.SelectedItem = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void dgvListTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvListTaiKhoan.Rows[e.RowIndex];

                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                cbQuyen.Text = row.Cells["VaiTro"].Value.ToString();
                cbMaNhanVien.SelectedValue = Convert.ToInt32(row.Cells["MaNV"].Value);
                string trangThai = row.Cells["TrangThai"].Value.ToString();
                rbKichHoat.Checked = trangThai == "Hoạt động";
                rbChuaKichHoat.Checked = trangThai == "Ngưng hoạt động";

            }
        }
    }
}
