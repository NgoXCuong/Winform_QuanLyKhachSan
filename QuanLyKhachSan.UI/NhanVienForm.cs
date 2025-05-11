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
        public NhanVienForm()
        {
            InitializeComponent();
        }

        private void NhanVienForm_Load(object sender, EventArgs e)
        {
            NhanVienService service = new NhanVienService();
            dgvListNhanVien.DataSource = service.GetAllNhanVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                (!rbNam.Checked && !rbNu.Checked) ||
                string.IsNullOrWhiteSpace(txtChucVu.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            // dtNgaySinh không bao giồ null
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            NhanVienService nhanVienService = new NhanVienService();
            bool ketQua = nhanVienService.ThemNhanVien(nv);

            if (ketQua)
            {
                MessageBox.Show("Thêm nhân viên thành công", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListNhanVien();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadListNhanVien()
        {
            NhanVienService service = new NhanVienService();
            dgvListNhanVien.DataSource = service.GetAllNhanVien();
        }


        private void ClearForm()
        {
            txtHoTen.Clear();
            rbNam.Checked = false;
            rbNu.Checked = false;
            dtNgaySinh.Value = DateTime.Now;
            txtChucVu.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
        }

        private void linkThemAnh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NhanVienService nvService = new NhanVienService();

            if (picAnhNhanVien.Image == null && dgvListNhanVien.CurrentRow != null)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Chọn ảnh nhân viên";
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string imgPath = openFileDialog.FileName;
                        picAnhNhanVien.Image = Image.FromFile(imgPath);

                        int maNV = Convert.ToInt32(dgvListNhanVien.CurrentRow.Cells["MaNV"].Value);
                        bool result = nvService.CapNhatAnhNhanVien(maNV, picAnhNhanVien.Image);

                        MessageBox.Show(result ? "Cập nhật ảnh thành công" : "Cập nhật ảnh thất bại");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên trước khi thêm ảnh", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dgvListNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvListNhanVien.Rows[e.RowIndex];

                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                rbNam.Checked = row.Cells["GioiTinh"].Value.ToString() == "Nam";
                rbNu.Checked = row.Cells["GioiTinh"].Value.ToString() == "Nữ";
                dtNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtChucVu.Text = row.Cells["ChucVu"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();

                int maNV = Convert.ToInt32(row.Cells["MaNV"].Value);

                // Gọi service để lấy ảnh của nhân viên
                var nhanVienService = new NhanVienService();
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

        public Image Base64ToImage(string base64String)
        {
            try
            {
                // Loại bỏ các ký tự không hợp lệ (nếu có)
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
                // Xử lý lỗi nếu chuỗi Base64 không hợp lệ
                MessageBox.Show("Chuỗi Base64 không hợp lệ: " + ex.Message);
                return null; // Trả về null nếu không thể giải mã
            }
        }


        bool IsValidBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return false;

            // Kiểm tra xem chuỗi có hợp lệ với Base64 hay không
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


    }
}
