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
        private KhachHangService khachHangService;
        private NhanVienService nhanVienService;

        public HoaDonForm()
        {
            InitializeComponent();
            cbMaDatPhong.SelectedIndexChanged += cbMaDatPhong_SelectedIndexChanged;
            cbKhachHang.SelectedIndexChanged += cbKhachHang_SelectedIndexChanged;
            cbNhanVien.SelectedIndexChanged += cbNhanVien_SelectedIndexChanged;
            khachHangService = new KhachHangService(); // hoặc truyền từ bên ngoài nếu dùng DI
    nhanVienService = new NhanVienService();   



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

            dgvChiTietHoaDon.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
            dgvChiTietHoaDon.Columns["MaDatPhong"].HeaderText = "Mã Đặt Phòng";
            dgvChiTietHoaDon.Columns["NgayLap"].HeaderText = "Ngày Lập";
            dgvChiTietHoaDon.Columns["KhachHang"].HeaderText = "Khách Hàng";
            dgvChiTietHoaDon.Columns["NhanVien"].HeaderText = "Nhân Viên";
            dgvChiTietHoaDon.Columns["TongTien"].HeaderText = "Tổng tiền";



            dgvChiTietHoaDon.Columns["MaKhachHang"].Visible = false;
            dgvChiTietHoaDon.Columns["MaNhanVien"].Visible = false;
        }

        //private void LoadComboBoxData()
        //{
        //    cbKhachHang.DataSource = hoaDonService.LayDanhSachKhachHang();
        //    cbKhachHang.DisplayMember = "Value";
        //    cbKhachHang.ValueMember = "Key";

        //    cbNhanVien.DataSource = hoaDonService.LayDanhSachNhanVien();
        //    cbNhanVien.DisplayMember = "Value";
        //    cbNhanVien.ValueMember = "Key";
        //}

        private void LoadComboBoxData()
        {
            try
            {
                // Load customer list
                var dsKhachHang = hoaDonService.LayDanhSachKhachHang();
                cbKhachHang.DataSource = dsKhachHang;
                cbKhachHang.DisplayMember = "Value";
                cbKhachHang.ValueMember = "Key";

                // Load employee list
                var dsNhanVien = hoaDonService.LayDanhSachNhanVien();
                cbNhanVien.DataSource = dsNhanVien;
                cbNhanVien.DisplayMember = "Value";
                cbNhanVien.ValueMember = "Key";

                // Load booking ID list
                var maDatPhongList = hoaDonService.LayDanhSachDatPhong();
                cbMaDatPhong.DataSource = maDatPhongList;
                cbMaDatPhong.DisplayMember = "Value"; // Displays "TenKhachHang - NgayDat"
                cbMaDatPhong.ValueMember = "Key";     // Uses MaDatPhong as the value
                cbMaDatPhong.SelectedIndex = -1;      // Ensure no item is selected by default

                if (maDatPhongList == null || maDatPhongList.Count == 0)
                {
                    cbMaDatPhong.Enabled = false;
                    MessageBox.Show("Không có mã đặt phòng nào trong hệ thống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cbMaDatPhong.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu ComboBox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        //private void btnThem_Click(object sender, EventArgs e)
        //{
        //    if (!int.TryParse(cbMaDatPhong.Text, out int maDatPhong) ||
        //        !decimal.TryParse(txtTongTien.Text, out decimal tongTien))
        //    {
        //        MessageBox.Show("Vui lòng nhập đúng định dạng cho Mã đặt phòng và Tổng tiền.");
        //        return;
        //    }

        //    var hoaDon = new HoaDonModel
        //    {
        //        MaDatPhong = maDatPhong,
        //        NgayLap = dtpNgayLap.Value,
        //        TongTien = tongTien
        //    };

        //    hoaDonService.ThemHoaDon(hoaDon);
        //    LoadData();
        //    ClearForm();
        //}
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbMaDatPhong.SelectedValue == null || !int.TryParse(cbMaDatPhong.SelectedValue.ToString(), out int maDatPhong) ||
                    !decimal.TryParse(txtTongTien.Text, out decimal tongTien))
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng cho Mã đặt phòng và Tổng tiền.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Thêm hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                //txtMaHoaDon.Text = hoaDon.MaHoaDon.ToString();
                //cbMaDatPhong.Text = hoaDon.MaDatPhong.ToString();
                //dtpNgayLap.Value = hoaDon.NgayLap;
                //cbKhachHang.Text = hoaDon.MaKhachHang.ToString();
                //cbNhanVien.Text = hoaDon.MaNhanVien.ToString();

                //txtTongTien.Text = hoaDon.TongTien.ToString("N2");

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
            //001
            txtMaHoaDon.Text = row.Cells["MaHoaDon"].Value?.ToString();
            cbMaDatPhong.Text = row.Cells["MaDatPhong"].Value?.ToString();

            dtpNgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
            txtTongTien.Text = Convert.ToDecimal(row.Cells["TongTien"].Value).ToString("N2");

            // Gán SelectedValue theo ID (chắc chắn có trong model)
            cbKhachHang.SelectedValue = Convert.ToInt32(row.Cells["MaKhachHang"].Value);
            cbNhanVien.SelectedValue = Convert.ToInt32(row.Cells["MaNhanVien"].Value);
        }



        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMaHoaDon.Text, out int maHoaDon))
            {
                MessageBox.Show("Mã hóa đơn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var hoaDon = hoaDonService.TimHoaDonTheoMa(maHoaDon);
            if (hoaDon == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        Document doc = new Document(PageSize.A4, 40f, 40f, 40f, 40f);
                        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Kiểm tra và tải font
                        string fontPath = Path.Combine(Application.StartupPath, "Fonts", "C:\\Users\\LAPTOP\\Downloads\\Roboto\\static\\Roboto-Regular.ttf");
                        BaseFont baseFont;
                        if (File.Exists(fontPath))
                        {
                            baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        }
                        else
                        {
                            baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            MessageBox.Show("Font Roboto không tìm thấy, sử dụng font mặc định.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // Tạo các font để tái sử dụng
                        Font titleFont = new Font(baseFont, 20, iTextSharp.text.Font.BOLD, new BaseColor(33, 150, 243)); // Màu xanh dương
                        Font headerFont = new Font(baseFont, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        Font boldFont = new Font(baseFont, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                        Font normalFont = new Font(baseFont, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        Font italicFont = new Font(baseFont, 10, iTextSharp.text.Font.ITALIC, BaseColor.GRAY);

                        // Thêm logo
                        string logoPath = Path.Combine(Application.StartupPath, "Images", "F:\\Code_BTL\\CShap\\QuanLyKhachSan\\QuanLyKhachSan.UI\\Image\\hotel64px.png");
                        if (File.Exists(logoPath))
                        {
                            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                            logo.ScaleToFit(80f, 80f);
                            logo.Alignment = Element.ALIGN_CENTER;
                            doc.Add(logo);
                        }

                        // Thêm thông tin công ty
                        Paragraph companyInfo = new Paragraph(
                            "Tập đoàn N8\nNam Từ Liêm, TP.Hà Nội\nHotline: 0123 456 789\nEmail: n8@gmail.com",
                            headerFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 10f
                        };
                        doc.Add(companyInfo);

                        // Tiêu đề hóa đơn
                        Paragraph title = new Paragraph("HÓA ĐƠN THANH TOÁN", titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 15f
                        };
                        doc.Add(title);

                        // Bảng thông tin hóa đơn
                        PdfPTable infoTable = new PdfPTable(2)
                        {
                            WidthPercentage = 100f,
                            SpacingAfter = 15f
                        };
                        infoTable.SetWidths(new float[] { 1f, 2f });

                        // Thêm các ô với viền và màu nền
                        infoTable.AddCell(new PdfPCell(new Phrase("Mã hóa đơn:", boldFont)) { Padding = 5f, BackgroundColor = new BaseColor(240, 240, 240) });
                        infoTable.AddCell(new PdfPCell(new Phrase(hoaDon.MaHoaDon.ToString(), normalFont)) { Padding = 5f });
                        infoTable.AddCell(new PdfPCell(new Phrase("Mã đặt phòng:", boldFont)) { Padding = 5f, BackgroundColor = new BaseColor(240, 240, 240) });
                        infoTable.AddCell(new PdfPCell(new Phrase(hoaDon.MaDatPhong.ToString(), normalFont)) { Padding = 5f });
                        infoTable.AddCell(new PdfPCell(new Phrase("Khách hàng:", boldFont)) { Padding = 5f, BackgroundColor = new BaseColor(240, 240, 240) });
                        infoTable.AddCell(new PdfPCell(new Phrase(hoaDon.KhachHang, normalFont)) { Padding = 5f });
                        infoTable.AddCell(new PdfPCell(new Phrase("Nhân viên lập:", boldFont)) { Padding = 5f, BackgroundColor = new BaseColor(240, 240, 240) });
                        infoTable.AddCell(new PdfPCell(new Phrase(hoaDon.NhanVien, normalFont)) { Padding = 5f });
                        infoTable.AddCell(new PdfPCell(new Phrase("Ngày lập:", boldFont)) { Padding = 5f, BackgroundColor = new BaseColor(240, 240, 240) });
                        infoTable.AddCell(new PdfPCell(new Phrase(hoaDon.NgayLap.ToString("dd/MM/yyyy"), normalFont)) { Padding = 5f });
                        infoTable.AddCell(new PdfPCell(new Phrase("Tổng tiền:", boldFont)) { Padding = 5f, BackgroundColor = new BaseColor(240, 240, 240) });
                        infoTable.AddCell(new PdfPCell(new Phrase($"{hoaDon.TongTien:N0} VND", normalFont)) { Padding = 5f });

                        doc.Add(infoTable);

                        // Đường phân cách
                        PdfPTable separator = new PdfPTable(1)
                        {
                            WidthPercentage = 100f,
                            SpacingBefore = 10f,
                            SpacingAfter = 10f
                        };
                        separator.AddCell(new PdfPCell() { Border = PdfPCell.TOP_BORDER, BorderColor = new BaseColor(200, 200, 200) });
                        doc.Add(separator);

                        // Dòng cảm ơn
                        Paragraph thankYou = new Paragraph("Cảm ơn quý khách đã sử dụng dịch vụ!", italicFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingBefore = 10f
                        };
                        doc.Add(thankYou);

                        // Chân trang
                        Paragraph footer = new Paragraph($"Hóa đơn được tạo vào: {DateTime.Now:dd/MM/yyyy HH:mm}", italicFont)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingBefore = 10f
                        };
                        doc.Add(footer);

                        doc.Close();
                    }

                    MessageBox.Show("Xuất hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}\nVui lòng kiểm tra đường dẫn file font hoặc quyền ghi file.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbMaDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbMaDatPhong.SelectedValue != null && int.TryParse(cbMaDatPhong.SelectedValue.ToString(), out int maDatPhong))
                {
                    decimal tongTien = hoaDonService.TinhTongTienTheoMaDatPhong(maDatPhong);
                    txtTongTien.Text = tongTien.ToString("N0"); // Format number without decimals
                }
                else
                {
                    txtTongTien.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính tổng tiền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTongTien.Text = "0";
            }
        }
        private void cbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cbNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbNhanVien.SelectedValue != null && int.TryParse(cbNhanVien.SelectedValue.ToString(), out int maNV))
                {
                    var nhanVien = nhanVienService.LayThongTinNhanVien(maNV);
                    if (nhanVien != null)
                    {
                        // Không cần set lại Text vì ComboBox đã binding tự động
                        // Chỉ cần xử lý logic nếu cần
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin nhân viên: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
