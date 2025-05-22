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
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace QuanLyKhachSan.UI
{
    public partial class HoaDonForm : Form
    {
        private HoaDonService service = new HoaDonService();

        public HoaDonForm()
        {
            try
            {
                InitializeComponent();
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
            dgvChiTietHoaDon.DataSource = service.LayTatCaHoaDon();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            var model = new HoaDonModel
            {
                MaDatPhong = int.Parse(cbMaDatPhong.Text),
                NgayLap = dtpNgayLap.Value,
                TongTien = decimal.Parse(txtTongTien.Text)
            };
            service.Them(model);
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(cbMaDatPhong.Text, out int maDatPhong))
            {
                MessageBox.Show("Mã đặt phòng không hợp lệ!");
                return;
            }

            if (!decimal.TryParse(txtTongTien.Text, out decimal tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ!");
                return;
            }

            var model = new HoaDonModel
            {
                MaDatPhong = maDatPhong,
                NgayLap = dtpNgayLap.Value,
                TongTien = tongTien
            };

            service.Them(model);
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoaDon.Text, out int maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!");
                return;
            }

            if (!int.TryParse(cbMaDatPhong.Text, out int maDatPhong))
            {
                MessageBox.Show("Mã đặt phòng không hợp lệ!");
                return;
            }

            if (!decimal.TryParse(txtTongTien.Text, out decimal tongTien))
            {
                MessageBox.Show("Tổng tiền không hợp lệ!");
                return;
            }

            var model = new HoaDonModel
            {
                MaHoaDon = maHoaDon,
                MaDatPhong = maDatPhong,
                NgayLap = dtpNgayLap.Value,
                TongTien = tongTien
            };

            service.CapNhat(model);
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoaDon.Text, out int maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!");
                return;
            }

            service.Xoa(maHoaDon);
            LoadData();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtTim.Text, out int maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn tìm kiếm không hợp lệ!");
                return;
            }

            var hoaDon = service.TimHoaDon(maHoaDon);
            if (hoaDon != null)
            {
                // Hiển thị lên các ô
                txtMaHoaDon.Text = hoaDon.MaHoaDon.ToString();
                cbMaDatPhong.Text = hoaDon.MaDatPhong.ToString();
                dtpNgayLap.Value = hoaDon.NgayLap;
                cbKhachHang.Text = hoaDon.KhachHang;
                cbNhanVien.Text = hoaDon.NhanVien;
                txtTongTien.Text = hoaDon.TongTien.ToString("N2");

                // Đổ lại dữ liệu vào DataGridView (chỉ 1 hóa đơn)
                dgvChiTietHoaDon.DataSource = new List<HoaDonModel> { hoaDon };
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn!");
            }
        }


        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoaDon.Text, out int maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!");
                return;
            }

            var hoaDon = service.TimHoaDon(maHoaDon);
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
                    Document doc = new Document();
                    PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
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
                    MessageBox.Show("Xuất hóa đơn thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message);
                }
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChiTietHoaDon.Rows[e.RowIndex];

                //cbKhachHang.Text = row.Cells["TenKhachHang"].Value.ToString();
                //cbNhanVien.Text = row.Cells["TenNhanVien"].Value.ToString();
                cbKhachHang.Text = row.Cells["KhachHang"].Value.ToString();
                cbNhanVien.Text = row.Cells["NhanVien"].Value.ToString();

                txtMaHoaDon.Text = row.Cells["MaHoaDon"].Value.ToString();
                cbMaDatPhong.Text = row.Cells["MaDatPhong"].Value.ToString();
                dtpNgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
                txtTongTien.Text = row.Cells["TongTien"].Value.ToString();

            }
        }
        private HoaDonService hoaDonService = new HoaDonService();

        private void LoadComboBoxData()
        {
            cbKhachHang.DataSource = hoaDonService.GetDanhSachKhachHang();
            cbKhachHang.DisplayMember = "Value";
            cbKhachHang.ValueMember = "Key";

            cbNhanVien.DataSource = hoaDonService.GetDanhSachNhanVien();
            cbNhanVien.DisplayMember = "Value";
            cbNhanVien.ValueMember = "Key";
        }
    }
}
