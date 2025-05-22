using System;
using System.Collections.Generic;
using System.Data;
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

        public HoaDonForm()
        {
            InitializeComponent();
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

        private void LoadData()
        {
            dgvChiTietHoaDon.DataSource = hoaDonService.LayTatCaHoaDon();
        }

        private void LoadComboBoxData()
        {
            cbKhachHang.DataSource = hoaDonService.LayDanhSachKhachHang();
            cbKhachHang.DisplayMember = "Value";
            cbKhachHang.ValueMember = "Key";

            cbNhanVien.DataSource = hoaDonService.LayDanhSachNhanVien();
            cbNhanVien.DisplayMember = "Value";
            cbNhanVien.ValueMember = "Key";
        }

        private void ClearForm()
        {
            txtMaHoaDon.Clear();
            cbMaDatPhong.SelectedIndex = -1;
            cbKhachHang.SelectedIndex = -1;
            cbNhanVien.SelectedIndex = -1;
            txtTongTien.Clear();
            dtpNgayLap.Value = DateTime.Today;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(cbMaDatPhong.Text, out int maDatPhong) ||
                !decimal.TryParse(txtTongTien.Text, out decimal tongTien))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng cho Mã đặt phòng và Tổng tiền.");
                return;
            }

            var hoaDon = new HoaDonModel
            {
                MaDatPhong = maDatPhong,
                NgayLap = dtpNgayLap.Value,
                TongTien = tongTien
            };

            hoaDonService.ThemHoaDon(hoaDon);
            LoadData();
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoaDon.Text, out int maHoaDon) ||
                !int.TryParse(cbMaDatPhong.Text, out int maDatPhong) ||
                !decimal.TryParse(txtTongTien.Text, out decimal tongTien))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng các trường!");
                return;
            }

            var hoaDon = new HoaDonModel
            {
                MaHoaDon = maHoaDon,
                MaDatPhong = maDatPhong,
                NgayLap = dtpNgayLap.Value,
                TongTien = tongTien
            };

            hoaDonService.CapNhatHoaDon(hoaDon);
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoaDon.Text, out int maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!");
                return;
            }

            hoaDonService.XoaHoaDon(maHoaDon);
            LoadData();
            ClearForm();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTim.Text, out int maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!");
                return;
            }

            var hoaDon = hoaDonService.TimHoaDonTheoMa(maHoaDon);
            if (hoaDon != null)
            {
                txtMaHoaDon.Text = hoaDon.MaHoaDon.ToString();
                cbMaDatPhong.Text = hoaDon.MaDatPhong.ToString();
                dtpNgayLap.Value = hoaDon.NgayLap;
                cbKhachHang.Text = hoaDon.KhachHang;
                cbNhanVien.Text = hoaDon.NhanVien;
                txtTongTien.Text = hoaDon.TongTien.ToString("N2");

                dgvChiTietHoaDon.DataSource = new List<HoaDonModel> { hoaDon };
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void dgvChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvChiTietHoaDon.Rows[e.RowIndex];
            txtMaHoaDon.Text = row.Cells["MaHoaDon"].Value.ToString();
            cbMaDatPhong.Text = row.Cells["MaDatPhong"].Value.ToString();
            dtpNgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
            cbKhachHang.Text = row.Cells["KhachHang"].Value.ToString();
            cbNhanVien.Text = row.Cells["NhanVien"].Value.ToString();
            txtTongTien.Text = row.Cells["TongTien"].Value.ToString();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoaDon.Text, out int maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!");
                return;
            }

            var hoaDon = hoaDonService.TimHoaDonTheoMa(maHoaDon);
            if (hoaDon == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn!");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF file (*.pdf)|*.pdf",
                FileName = $"HoaDon_{maHoaDon}.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document doc = new Document();
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                        var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                        doc.Add(new Paragraph("HÓA ĐƠN THANH TOÁN", titleFont));
                        doc.Add(new Paragraph($"Mã hóa đơn: {hoaDon.MaHoaDon}", normalFont));
                        doc.Add(new Paragraph($"Mã đặt phòng: {hoaDon.MaDatPhong}", normalFont));
                        doc.Add(new Paragraph($"Khách hàng: {hoaDon.KhachHang}", normalFont));
                        doc.Add(new Paragraph($"Nhân viên lập: {hoaDon.NhanVien}", normalFont));
                        doc.Add(new Paragraph($"Ngày lập: {hoaDon.NgayLap:dd/MM/yyyy}", normalFont));
                        doc.Add(new Paragraph($"Tổng tiền: {hoaDon.TongTien:N0} VND", normalFont));

                        doc.Close();
                    }

                    MessageBox.Show("Xuất hóa đơn thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message);
                }
            }
        }
    }
}
