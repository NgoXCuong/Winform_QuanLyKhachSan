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
                        Document doc = new Document(PageSize.A4, 50f, 50f, 50f, 50f);
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Font hỗ trợ tiếng Việt (Arial Unicode MS hoặc Times New Roman)
                        string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "C:\\Users\\LAPTOP\\Downloads\\Roboto\\static\\Roboto-Regular.ttf");

                        BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                        Font titleFont = new Font(new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                        Font normalFont = new Font(baseFont, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        Font boldFont = new Font(baseFont, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                        // --- Header: Thông tin công ty ---
                        Paragraph companyInfo = new Paragraph("N8 HOTEL\nĐịa chỉ: Nam Từ Liêm, Hà Nội\nĐiện thoại: 0123 456 789", normalFont)
                        {
                            Alignment = Element.ALIGN_LEFT,
                            SpacingAfter = 20f
                        };
                        doc.Add(companyInfo);

                        // --- Tiêu đề ---
                        Paragraph title = new Paragraph("HÓA ĐƠN THANH TOÁN", titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 20f
                        };
                        doc.Add(title);

                        // --- Bảng thông tin hóa đơn ---
                        PdfPTable table = new PdfPTable(2)
                        {
                            WidthPercentage = 100f,
                            SpacingAfter = 20f
                        };
                        table.SetWidths(new float[] { 1f, 2f });

                        PdfPCell MakeCell(string text, Font font, int alignment = Element.ALIGN_LEFT, float padding = 6f)
                        {
                            return new PdfPCell(new Phrase(text, font))
                            {
                                Border = PdfPCell.NO_BORDER,
                                HorizontalAlignment = alignment,
                                PaddingBottom = padding
                            };
                        }

                        table.AddCell(MakeCell("Mã hóa đơn:", boldFont));
                        table.AddCell(MakeCell(hoaDon.MaHoaDon.ToString(), normalFont, Element.ALIGN_RIGHT));

                        table.AddCell(MakeCell("Mã đặt phòng:", boldFont));
                        table.AddCell(MakeCell(hoaDon.MaDatPhong.ToString(), normalFont, Element.ALIGN_RIGHT));

                        table.AddCell(MakeCell("Khách hàng:", boldFont));
                        table.AddCell(MakeCell(hoaDon.KhachHang, normalFont, Element.ALIGN_RIGHT));

                        table.AddCell(MakeCell("Nhân viên lập:", boldFont));
                        table.AddCell(MakeCell(hoaDon.NhanVien, normalFont, Element.ALIGN_RIGHT));

                        table.AddCell(MakeCell("Ngày lập:", boldFont));
                        table.AddCell(MakeCell(hoaDon.NgayLap.ToString("dd/MM/yyyy"), normalFont, Element.ALIGN_RIGHT));

                        table.AddCell(MakeCell("Tổng tiền:", boldFont));
                        table.AddCell(MakeCell($"{hoaDon.TongTien:N0} VND", normalFont, Element.ALIGN_RIGHT));

                        doc.Add(table);

                        // --- Gạch ngang ---
                        PdfPTable line = new PdfPTable(1)
                        {
                            WidthPercentage = 100f,
                            SpacingBefore = 10f,
                            SpacingAfter = 10f
                        };
                        line.AddCell(new PdfPCell() { Border = PdfPCell.TOP_BORDER, BorderColor = BaseColor.GRAY });
                        doc.Add(line);

                        // --- Footer: Ngày in và cảm ơn ---
                        Paragraph footer = new Paragraph($"Ngày in: {DateTime.Now:dd/MM/yyyy}\n\nCảm ơn quý khách đã sử dụng dịch vụ của chúng tôi!", normalFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingBefore = 30f
                        };
                        doc.Add(footer);

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
