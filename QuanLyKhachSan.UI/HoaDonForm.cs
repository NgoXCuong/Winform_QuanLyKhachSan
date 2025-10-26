using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace QuanLyKhachSan.UI
{
    public partial class HoaDonForm : Form
    {
        private readonly HoaDonService hoaDonService = new HoaDonService();
        private readonly KhachHangService khachHangService = new KhachHangService();

        public HoaDonForm()
        {
            InitializeComponent();

            // Gán sự kiện
            cbMaDatPhong.SelectedIndexChanged += CbMaDatPhong_SelectedIndexChanged;
            cbKhachHang.SelectedIndexChanged += CbKhachHang_SelectedIndexChanged;

            try
            {
                LoadComboBoxData();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message);
            }
        }

        // Load danh sách hóa đơn lên DataGridView
        private void LoadData()
        {
            dgvChiTietHoaDon.DataSource = hoaDonService.LayTatCaHoaDon();

            if (dgvChiTietHoaDon.Columns.Contains("MaHD"))
                dgvChiTietHoaDon.Columns["MaHD"].HeaderText = "Mã Hóa Đơn";

            if (dgvChiTietHoaDon.Columns.Contains("MaDatPhong"))
                dgvChiTietHoaDon.Columns["MaDatPhong"].HeaderText = "Mã Đặt Phòng";

            if (dgvChiTietHoaDon.Columns.Contains("MaKH"))
                dgvChiTietHoaDon.Columns["MaKH"].HeaderText = "Khách Hàng";

            if (dgvChiTietHoaDon.Columns.Contains("TienPhong"))
                dgvChiTietHoaDon.Columns["TienPhong"].HeaderText = "Tiền Phòng";

            if (dgvChiTietHoaDon.Columns.Contains("TongTienThanhToan"))
                dgvChiTietHoaDon.Columns["TongTienThanhToan"].HeaderText = "Tổng Thanh Toán";

            if (dgvChiTietHoaDon.Columns.Contains("TrangThaiThanhToan"))
                dgvChiTietHoaDon.Columns["TrangThaiThanhToan"].HeaderText = "Trạng Thái";

            if (dgvChiTietHoaDon.Columns.Contains("PhuongThucThanhToan"))
                dgvChiTietHoaDon.Columns["PhuongThucThanhToan"].HeaderText = "PT Thanh Toán";

            if (dgvChiTietHoaDon.Columns.Contains("NgayThanhToan"))
                dgvChiTietHoaDon.Columns["NgayThanhToan"].HeaderText = "Ngày Thanh Toán";

            if (dgvChiTietHoaDon.Columns.Contains("GhiChu"))
                dgvChiTietHoaDon.Columns["GhiChu"].HeaderText = "Ghi Chú";

            if (dgvChiTietHoaDon.Columns.Contains("NgayTao"))
                dgvChiTietHoaDon.Columns["NgayTao"].HeaderText = "Ngày Tạo";
        }

        // Load dữ liệu cho ComboBox
        private void LoadComboBoxData()
        {
            try
            {
                // Khách hàng
                var dsKhachHang = hoaDonService.LayDanhSachKhachHang();
                cbKhachHang.DataSource = dsKhachHang;
                cbKhachHang.DisplayMember = "Value";
                cbKhachHang.ValueMember = "Key";
                cbKhachHang.SelectedIndex = -1;

                // Mã đặt phòng
                var dsDatPhong = hoaDonService.LayDanhSachDatPhong();
                cbMaDatPhong.DataSource = dsDatPhong;
                cbMaDatPhong.DisplayMember = "Value";
                cbMaDatPhong.ValueMember = "Key";
                cbMaDatPhong.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải ComboBox: " + ex.Message);
            }
        }

        // Xóa dữ liệu form
        private void ClearForm()
        {
            txtMaHoaDon.Clear();
            cbMaDatPhong.SelectedIndex = -1;
            cbKhachHang.SelectedIndex = -1;
            txtTienPhong.Clear();
            txtTongTien.Clear();
            cbTrangThaiThanhToan.SelectedIndex = -1;
            cbPhuongThucThanhToan.SelectedIndex = -1;
            dtpNgayThanhToan.Value = DateTime.Today;
            txtGhiChu.Clear();
            dtpNgayTao.Value = DateTime.Today;
        }

        // Thêm hóa đơn
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbMaDatPhong.SelectedValue == null || cbKhachHang.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn Mã Đặt Phòng và Khách Hàng!");
                    return;
                }

                var hoaDon = new HoaDonModel
                {
                    MaDatPhong = Convert.ToInt32(cbMaDatPhong.SelectedValue),
                    MaKH = Convert.ToInt32(cbKhachHang.SelectedValue),
                    TienPhong = decimal.TryParse(txtTienPhong.Text, out decimal tien) ? tien : 0,
                    TongTienThanhToan = decimal.TryParse(txtTongTien.Text, out decimal tong) ? tong : 0,
                    TrangThaiThanhToan = cbTrangThaiThanhToan.Text,
                    PhuongThucThanhToan = cbPhuongThucThanhToan.Text,
                    NgayThanhToan = dtpNgayThanhToan.Value,
                    GhiChu = txtGhiChu.Text,
                    NgayTao = dtpNgayTao.Value
                };

                hoaDonService.ThemHoaDon(hoaDon);
                LoadData();
                ClearForm();
                MessageBox.Show("Thêm hóa đơn thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm hóa đơn: " + ex.Message);
            }
        }

        // Sửa hóa đơn
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaHoaDon.Text, out int maHD))
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ!");
                    return;
                }

                var hoaDon = new HoaDonModel
                {
                    MaHD = maHD,
                    MaDatPhong = Convert.ToInt32(cbMaDatPhong.SelectedValue),
                    MaKH = Convert.ToInt32(cbKhachHang.SelectedValue),
                    TienPhong = decimal.TryParse(txtTienPhong.Text, out decimal tien) ? tien : 0,
                    TongTienThanhToan = decimal.TryParse(txtTongTien.Text, out decimal tong) ? tong : 0,
                    TrangThaiThanhToan = cbTrangThaiThanhToan.Text,
                    PhuongThucThanhToan = cbPhuongThucThanhToan.Text,
                    NgayThanhToan = dtpNgayThanhToan.Value,
                    GhiChu = txtGhiChu.Text,
                    NgayTao = dtpNgayTao.Value
                };

                hoaDonService.CapNhatHoaDon(hoaDon);
                LoadData();
                MessageBox.Show("Cập nhật hóa đơn thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        // Xóa hóa đơn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaHoaDon.Text, out int maHD))
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ!");
                    return;
                }

                hoaDonService.XoaHoaDon(maHD);
                LoadData();
                ClearForm();
                MessageBox.Show("Xóa hóa đơn thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }

        // Tìm kiếm theo Mã hóa đơn
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTim.Text, out int maHD))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!");
                return;
            }

            var hoaDon = hoaDonService.TimHoaDonTheoMa(maHD);
            if (hoaDon != null)
                dgvChiTietHoaDon.DataSource = new List<HoaDonModel> { hoaDon };
            else
                MessageBox.Show("Không tìm thấy hóa đơn!");
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        // Khi click vào DataGridView
        private void dgvChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvChiTietHoaDon.Rows[e.RowIndex];
            txtMaHoaDon.Text = row.Cells["MaHD"].Value?.ToString();
            cbMaDatPhong.SelectedValue = Convert.ToInt32(row.Cells["MaDatPhong"].Value);
            cbKhachHang.SelectedValue = Convert.ToInt32(row.Cells["MaKH"].Value);
            txtTienPhong.Text = row.Cells["TienPhong"].Value?.ToString();
            txtTongTien.Text = row.Cells["TongTienThanhToan"].Value?.ToString();
            cbTrangThaiThanhToan.Text = row.Cells["TrangThaiThanhToan"].Value?.ToString();
            cbPhuongThucThanhToan.Text = row.Cells["PhuongThucThanhToan"].Value?.ToString();
            dtpNgayThanhToan.Value = row.Cells["NgayThanhToan"].Value != null ? Convert.ToDateTime(row.Cells["NgayThanhToan"].Value) : DateTime.Today;
            txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
        }

        // Tính tổng tiền khi chọn mã đặt phòng
        private void CbMaDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaDatPhong.SelectedValue != null)
            {
                int maDatPhong = Convert.ToInt32(cbMaDatPhong.SelectedValue);
                ////decimal tienPhong = hoaDonService.TinhTienPhong(maDatPhong);
                //txtTienPhong.Text = tienPhong.ToString("N0");
                //txtTongTien.Text = tienPhong.ToString("N0");
            }
        }

        private void CbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbKhachHang.SelectedValue != null && int.TryParse(cbKhachHang.SelectedValue.ToString(), out int maKH))
                {
                    var khachHang = khachHangService.LayThongTinKhachHang(maKH);
                    if (khachHang != null)
                    {
                        // Không cần set lại Text vì ComboBox đã binding tự động
                        // Chỉ cần xử lý logic nếu cần
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin khách hàng: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xuất hóa đơn ra PDF
        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoaDon.Text, out int maHD))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!");
                return;
            }

            var hoaDon = hoaDonService.TimHoaDonTheoMa(maHD);
            if (hoaDon == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn!");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF file (*.pdf)|*.pdf",
                FileName = $"HoaDon_{maHD}.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        //doc.Add(new Paragraph("HÓA ĐƠN THANH TOÁN", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD)) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 15f });

                        PdfPTable table = new PdfPTable(2) { WidthPercentage = 100f };
                        table.AddCell("Mã Hóa Đơn"); table.AddCell(hoaDon.MaHD.ToString());
                        table.AddCell("Mã Đặt Phòng"); table.AddCell(hoaDon.MaDatPhong.ToString());
                        table.AddCell("Khách Hàng"); table.AddCell(hoaDon.MaKH.ToString());
                        table.AddCell("Tiền Phòng"); table.AddCell($"{hoaDon.TienPhong:N0}");
                        table.AddCell("Tổng Thanh Toán"); table.AddCell($"{hoaDon.TongTienThanhToan:N0}");
                        table.AddCell("Trạng Thái"); table.AddCell(hoaDon.TrangThaiThanhToan);
                        table.AddCell("Phương Thức"); table.AddCell(hoaDon.PhuongThucThanhToan);
                        table.AddCell("Ngày Thanh Toán"); table.AddCell(hoaDon.NgayThanhToan?.ToString("dd/MM/yyyy") ?? "");
                        table.AddCell("Ghi Chú"); table.AddCell(hoaDon.GhiChu);
                        table.AddCell("Ngày Tạo"); table.AddCell(hoaDon.NgayTao.ToString("dd/MM/yyyy"));

                        doc.Add(table);
                        doc.Close();
                    }

                    MessageBox.Show("Xuất PDF thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất PDF: " + ex.Message);
                }
            }
        }
    }
}
