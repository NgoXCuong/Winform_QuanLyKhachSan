using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;
// using iTextSharp.text;      // Bỏ comment nếu bạn đã cài thư viện iTextSharp
// using iTextSharp.text.pdf;  // Bỏ comment nếu bạn đã cài thư viện iTextSharp
// using System.IO;

using System.Drawing;
using System.Drawing.Printing;

namespace QuanLyKhachSan.UI
{
    public partial class HoaDonForm : Form
    {
        private readonly HoaDonService _service = new HoaDonService();
        private List<HoaDonModel> _originalList = new List<HoaDonModel>();
        private int _currentMaKH = 0;

        public HoaDonForm()
        {
            InitializeComponent();

            this.Load += HoaDonForm_Load;
            dgvHoaDon.CellClick += DgvHoaDon_CellClick;

            // --- THAY ĐỔI: Sự kiện cho ComboBox ---
            // Dùng SelectionChangeCommitted để chỉ tính tiền khi NGƯỜI DÙNG chọn bằng chuột/phím
            cbMaDatPhong.SelectionChangeCommitted += cbMaDatPhong_SelectionChangeCommitted;
            // Nếu muốn gõ mã rồi Enter cũng nhận thì dùng KeyDown
            cbMaDatPhong.KeyDown += cbMaDatPhong_KeyDown;

            btnTaoMoi.Click += BtnTaoMoi_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnInHoaDon.Click += BtnInHoaDon_Click;
        }

        private void HoaDonForm_Load(object sender, EventArgs e)
        {
            InitComboBoxes();
            LoadData();
            ResetForm();
            LoadListHoaDon();

            // Set default date range to be very wide (past and future)
            dtpTuNgay.Value = new DateTime(2000, 1, 1);
            dtpDenNgay.Value = new DateTime(2100, 12, 31);
        }

        private void LoadListHoaDon()
        {
            var list = _service.GetAll();

            dgvHoaDon.DataSource = null;
            dgvHoaDon.DataSource = list;

            dgvHoaDon.Columns["MaHD"].HeaderText = "Mã HD";
            dgvHoaDon.Columns["MaDatPhong"].HeaderText = "Mã DP";
            dgvHoaDon.Columns["MaKH"].HeaderText = "Mã KH";
            dgvHoaDon.Columns["TienPhong"].HeaderText = "Tiền phòng";
            dgvHoaDon.Columns["TongTienThanhToan"].HeaderText = "Tổng tiền";
            dgvHoaDon.Columns["TrangThaiThanhToan"].HeaderText = "Trạng thái TT"; // Ẩn cột ảnh nếu không cần hiển thị
            dgvHoaDon.Columns["PhuongThucThanhToan"].HeaderText = "Phương thức TT";
            dgvHoaDon.Columns["NgayThanhToan"].HeaderText = "Ngày TT";
            dgvHoaDon.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvHoaDon.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvHoaDon.Columns["TenKhachHang"].HeaderText = "Tên KH";
            dgvHoaDon.Columns["SoPhong"].HeaderText = "Số phòng";
            dgvHoaDon.Columns["TienDichVu"].HeaderText = "Tiền dịch vụ";
        }

        #region 1. Cấu hình và Load dữ liệu

        private void InitComboBoxes()
        {
            // 1. Load danh sách Đặt Phòng vào ComboBox
            DataTable dtDatPhong = _service.LayDanhSachDatPhong();
            cbMaDatPhong.DataSource = dtDatPhong;
            cbMaDatPhong.DisplayMember = "HienThi";   // Cột hiển thị: "101 - Nguyen Van A..."
            cbMaDatPhong.ValueMember = "MaDatPhong"; // Giá trị ngầm: 101
            cbMaDatPhong.SelectedIndex = -1;         // Không chọn dòng nào lúc đầu

            // 2. CBO Trạng thái lọc
            cboTrangThaiThanhToan.Items.Clear();
            cboTrangThaiThanhToan.Items.AddRange(new string[] { "Tất cả", "Đã thanh toán", "Chưa thanh toán" });
            cboTrangThaiThanhToan.SelectedIndex = 0;

            // 3. CBO Chi tiết
            cboTrangThaiThanhToanDetail.Items.Clear();
            cboTrangThaiThanhToanDetail.Items.AddRange(new string[] { "Chưa thanh toán", "Đã thanh toán", "Thanh toán một phần" });
            cboTrangThaiThanhToanDetail.SelectedIndex = 0;

            // 4. CBO Phương thức
            cboPhuongThucThanhToan.Items.Clear();
            cboPhuongThucThanhToan.Items.AddRange(new string[] { "Tiền mặt", "Chuyển khoản", "Thẻ tín dụng" });
            cboPhuongThucThanhToan.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                _originalList = _service.GetAll();
                dgvHoaDon.DataSource = _originalList;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            if (dgvHoaDon.Columns["TongTienThanhToan"] != null) dgvHoaDon.Columns["TongTienThanhToan"].DefaultCellStyle.Format = "N0";
            if (dgvHoaDon.Columns["TienPhong"] != null) dgvHoaDon.Columns["TienPhong"].DefaultCellStyle.Format = "N0";
            if (dgvHoaDon.Columns["TienDichVu"] != null) dgvHoaDon.Columns["TienDichVu"].DefaultCellStyle.Format = "N0";
            if (dgvHoaDon.Columns["NgayThanhToan"] != null) dgvHoaDon.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private void ResetForm()
        {
            txtMaHD.Clear();

            // Reset ComboBox Đặt phòng
            cbMaDatPhong.SelectedIndex = -1;
            cbMaDatPhong.Enabled = true;

            txtTenKhachHang.Clear();
            txtSoPhong.Clear();
            txtTienPhong.Text = "0";
            txtTienDichVu.Text = "0";
            txtTongTienThanhToan.Text = "0";
            txtGhiChu.Clear();
            _currentMaKH = 0;

            cboTrangThaiThanhToanDetail.SelectedIndex = 0;
            cboPhuongThucThanhToan.SelectedIndex = 0;
            dtpNgayThanhToan.Value = DateTime.Now;
            dgvDichVu.DataSource = null;
        }

        #endregion

        #region 2. Xử lý hiển thị (Binding)

        private void DgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                var row = dgvHoaDon.Rows[e.RowIndex];
                var hd = row.DataBoundItem as HoaDonModel;

                if (hd != null)
                {
                    txtMaHD.Text = hd.MaHD.ToString();

                    // Gán giá trị cho ComboBox (chọn đúng mã đặt phòng)
                    cbMaDatPhong.SelectedValue = hd.MaDatPhong;
                    cbMaDatPhong.Enabled = false; // Khóa lại khi xem chi tiết

                    txtTenKhachHang.Text = hd.TenKhachHang;
                    txtSoPhong.Text = hd.SoPhong;
                    _currentMaKH = hd.MaKH;

                    txtTienPhong.Text = hd.TienPhong.ToString("N0");
                    txtTienDichVu.Text = hd.TienDichVu.ToString("N0");
                    txtTongTienThanhToan.Text = hd.TongTienThanhToan.ToString("N0");

                    cboTrangThaiThanhToanDetail.Text = hd.TrangThaiThanhToan;
                    cboPhuongThucThanhToan.Text = hd.PhuongThucThanhToan;
                    if (hd.NgayThanhToan.HasValue) dtpNgayThanhToan.Value = hd.NgayThanhToan.Value;
                    txtGhiChu.Text = hd.GhiChu;

                    // Load dịch vụ
                    LoadGridDichVu(hd.MaDatPhong);
                }
            }
            catch { }
        }

        private void LoadGridDichVu(int maDatPhong)
        {
            DataTable dt = _service.GetDichVuSuDung(maDatPhong);
            dgvDichVu.DataSource = dt;
            if (dgvDichVu.Columns["DonGia"] != null) dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dgvDichVu.Columns["ThanhTien"] != null) dgvDichVu.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        #endregion

        #region 3. Tự động tính toán khi CHỌN Mã Đặt Phòng (Mới)

        // Sự kiện khi người dùng click chọn trong danh sách ComboBox
        private void cbMaDatPhong_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CalculateInfo();
        }

        // Sự kiện khi người dùng gõ phím Enter trên ComboBox
        private void cbMaDatPhong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalculateInfo();
                e.SuppressKeyPress = true;
            }
        }

        private void CalculateInfo()
        {
            // Kiểm tra xem có giá trị nào được chọn không
            if (cbMaDatPhong.SelectedValue != null && int.TryParse(cbMaDatPhong.SelectedValue.ToString(), out int maDP))
            {
                var info = _service.TinhToanHoaDon(maDP);
                if (info != null)
                {
                    txtTenKhachHang.Text = info.TenKhachHang;
                    txtSoPhong.Text = info.SoPhong;
                    txtTienPhong.Text = info.TienPhong.ToString("N0");
                    txtTienDichVu.Text = info.TienDichVu.ToString("N0");
                    txtTongTienThanhToan.Text = info.TongTien.ToString("N0");
                    _currentMaKH = info.MaKH;

                    LoadGridDichVu(maDP);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin chi tiết cho mã đặt phòng này!");
                }
            }
        }

        #endregion

        #region 4. CRUD Chức năng

        private void BtnTaoMoi_Click(object sender, EventArgs e)
        {
            // Kiểm tra ComboBox có chọn giá trị hợp lệ không
            if (cbMaDatPhong.SelectedValue == null || _currentMaKH == 0)
            {
                MessageBox.Show("Vui lòng chọn Mã Đặt Phòng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                HoaDonModel hd = new HoaDonModel
                {
                    // Lấy giá trị từ ComboBox
                    MaDatPhong = Convert.ToInt32(cbMaDatPhong.SelectedValue),
                    MaKH = _currentMaKH,
                    TienPhong = decimal.Parse(txtTienPhong.Text, NumberStyles.AllowThousands),
                    TongTienThanhToan = decimal.Parse(txtTongTienThanhToan.Text, NumberStyles.AllowThousands),
                    TrangThaiThanhToan = cboTrangThaiThanhToanDetail.Text,
                    PhuongThucThanhToan = cboPhuongThucThanhToan.Text,
                    NgayThanhToan = dtpNgayThanhToan.Value,
                    GhiChu = txtGhiChu.Text
                };

                if (_service.Add(hd))
                {
                    MessageBox.Show("Tạo hóa đơn thành công!");
                    LoadData();
                    ResetForm();
                }
                else MessageBox.Show("Thêm thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text)) return;
            try
            {
                HoaDonModel hd = new HoaDonModel
                {
                    MaHD = int.Parse(txtMaHD.Text),
                    TienPhong = decimal.Parse(txtTienPhong.Text, NumberStyles.AllowThousands),
                    TongTienThanhToan = decimal.Parse(txtTongTienThanhToan.Text, NumberStyles.AllowThousands),
                    TrangThaiThanhToan = cboTrangThaiThanhToanDetail.Text,
                    PhuongThucThanhToan = cboPhuongThucThanhToan.Text,
                    NgayThanhToan = dtpNgayThanhToan.Value,
                    GhiChu = txtGhiChu.Text
                };

                if (_service.Update(hd))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                    ResetForm();
                }
                else MessageBox.Show("Cập nhật thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text)) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_service.Delete(int.Parse(txtMaHD.Text)))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                    ResetForm();
                }
                else MessageBox.Show("Xóa thất bại!");
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadData();
            // Load lại danh sách phòng phòng trường hợp có phòng mới đặt
            DataTable dt = _service.LayDanhSachDatPhong();
            cbMaDatPhong.DataSource = dt;
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            string status = cboTrangThaiThanhToan.Text;

            // Reload data to ensure we have the latest information
            var allData = _service.GetAll();

            var result = allData.Where(x =>
            {
                // Tìm theo từ khóa (Mã HD hoặc Tên khách hàng) - có thể bỏ trống
                bool keywordMatch = true; // Mặc định true nếu không nhập gì
                if (!string.IsNullOrEmpty(keyword))
                {
                    keywordMatch = x.MaHD.ToString().Contains(keyword) ||
                                 (x.TenKhachHang ?? "").ToLower().Contains(keyword.ToLower());
                }

                // Tìm theo trạng thái thanh toán - có thể chọn "Tất cả"
                bool statusMatch = true; // Mặc định true nếu chọn "Tất cả"
                if (status != "Tất cả")
                {
                    statusMatch = (status == "Đã thanh toán" && (x.TrangThaiThanhToan?.Contains("Đã thanh toán") == true || x.TrangThaiThanhToan?.Contains("Thanh toán một phần") == true)) ||
                                (status == "Chưa thanh toán" && x.TrangThaiThanhToan?.Contains("Chưa thanh toán") == true);
                }

                // Tìm theo khoảng thời gian - có thể bỏ qua nếu không chọn ngày cụ thể
                bool dateMatch = true; // Mặc định true (không lọc theo ngày)
                DateTime from = dtpTuNgay.Value.Date;
                DateTime to = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

                // Chỉ áp dụng lọc ngày nếu người dùng có chọn ngày khác ngày hiện tại
                if (from.Date != DateTime.Today || to.Date != DateTime.Today.AddDays(1))
                {
                    dateMatch = x.NgayTao >= from && x.NgayTao <= to;
                }

                // Kết hợp tất cả điều kiện tìm kiếm
                return keywordMatch && statusMatch && dateMatch;
            }).ToList();

            // Update original list for future searches
            _originalList = allData;

            // Set filtered result to grid
            dgvHoaDon.DataSource = null;
            dgvHoaDon.DataSource = result;
            FormatGrid();

            // Hiển thị kết quả tìm kiếm
            MessageBox.Show($"Tìm thấy {result.Count} hóa đơn phù hợp!", "Kết quả tìm kiếm",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Biến toàn cục để lưu mã hóa đơn đang in
        private int _maHDCanIn = 0;

        private void InHoaDon(int maHD)
        {
            _maHDCanIn = maHD;
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(Pd_PrintPage);

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.Width = 800;
            ppd.Height = 600;
            ppd.ShowDialog();
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            // --- 1. CHUẨN BỊ DỮ LIỆU ---
            HoaDonModel hd = _service.TimHoaDonTheoMa(_maHDCanIn);
            if (hd == null) return;
            DataTable dtDichVu = _service.GetDichVuSuDung(hd.MaDatPhong);

            // --- 2. CẤU HÌNH ĐỒ HỌA ---
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Đơn vị đo và lề
            float margin = 40;
            float width = e.PageBounds.Width - (margin * 2);
            float yPos = margin + 20;
            float xPos = margin;

            // Font chữ
            Font fTitle = new Font("Segoe UI", 22, FontStyle.Bold);
            Font fHeader = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fSubHeader = new Font("Segoe UI", 11, FontStyle.Bold);
            Font fBody = new Font("Segoe UI", 10, FontStyle.Regular);
            Font fBodyBold = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fSmall = new Font("Segoe UI", 9, FontStyle.Italic);

            // Bút vẽ
            Pen pThin = new Pen(Color.Silver, 1); // Màu nhạt hơn chút cho tinh tế
            Pen pThick = new Pen(Color.Black, 2);
            Pen pDashed = new Pen(Color.Gray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            Brush bBack = new SolidBrush(Color.FromArgb(240, 240, 240));

            // Format căn chỉnh
            StringFormat center = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat right = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
            StringFormat left = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

            // --- 3. VẼ HEADER ---

            // 1. Tên Khách sạn (Căn Trái - Font to)
            // Vẽ tại vị trí xPos (lề trái)
            g.DrawString("KHÁCH SẠN GRAND LUXURY", fTitle, Brushes.DarkSlateBlue, xPos, yPos);

            // 2. Thông tin liên hệ (Căn Phải - Font nhỏ)
            // Sử dụng 'xPos + width' để lấy toạ độ lề phải, và dùng format 'right' để căn lề phải
            // Cộng thêm một chút vào yPos (+5, +25) để căn chỉnh thẳng hàng đẹp mắt với chữ to bên trái

            // Dòng 1: Địa chỉ
            g.DrawString("Trịnh Văn Bô, Nam Từ Liêm, Hà Nội", fBody, Brushes.Black, xPos + width, yPos + 5, right);

            // Dòng 2: Hotline (Cách dòng 1 khoảng 25 đơn vị)
            g.DrawString("Hotline: 9999 9999 - Website: www.hotel.com", fBody, Brushes.Black, xPos + width, yPos + 30, right);

            // 3. Kết thúc phần Header
            // Tăng yPos lên một đoạn lớn để tạo khoảng cách thoáng với phần Tiêu đề hóa đơn bên dưới
            yPos += 60;

            // Vẽ đường kẻ ngang phân cách
            g.DrawLine(pThick, xPos, yPos, xPos + width, yPos);

            yPos += 45; // Cách xa header hơn
            g.DrawLine(pThick, xPos, yPos, xPos + width, yPos);

            // --- 4. TIÊU ĐỀ & THÔNG TIN ---
            yPos += 25;
            g.DrawString("HÓA ĐƠN THANH TOÁN", fHeader, Brushes.Black, e.PageBounds.Width / 2, yPos, center);

            yPos += 40;
            float col1 = xPos;
            float col2 = xPos + width / 2 + 20;
            float lineSpacing = 25; // Khoảng cách giữa các dòng thông tin khách

            g.DrawString($"Khách hàng: {hd.TenKhachHang.ToUpper()}", fBodyBold, Brushes.Black, col1, yPos);
            g.DrawString($"Mã hóa đơn: #{hd.MaHD}", fBody, Brushes.Black, col2, yPos);

            yPos += lineSpacing;
            g.DrawString($"Phòng số: {hd.SoPhong}", fBody, Brushes.Black, col1, yPos);
            g.DrawString($"Ngày lập: {hd.NgayTao:dd/MM/yyyy HH:mm}", fBody, Brushes.Black, col2, yPos);

            yPos += lineSpacing;
            g.DrawString($"Thu ngân: Admin", fBody, Brushes.Black, col1, yPos);

            // --- 5. BẢNG DỊCH VỤ (HEADER) ---
            yPos += 40; // Cách xa phần thông tin khách ra

            // Định nghĩa độ rộng cột
            float wCol1 = 40; float wCol2 = 250; float wCol3 = 60; float wCol4 = 100;
            float wCol5 = width - wCol1 - wCol2 - wCol3 - wCol4;
            float xCol1 = xPos;
            float xCol2 = xCol1 + wCol1;
            float xCol3 = xCol2 + wCol2;
            float xCol4 = xCol3 + wCol3;
            float xCol5 = xCol4 + wCol4;

            // Header bảng cao hơn để thoáng
            float headerHeight = 40;
            g.FillRectangle(bBack, xPos, yPos, width, headerHeight);
            g.DrawRectangle(pThin, xPos, yPos, width, headerHeight);

            float textHeaderY = yPos + (headerHeight / 2); // Căn giữa theo chiều dọc
            g.DrawString("STT", fSubHeader, Brushes.Black, xCol1 + 5, textHeaderY, left);
            g.DrawString("Tên Dịch Vụ", fSubHeader, Brushes.Black, xCol2 + 5, textHeaderY, left);
            g.DrawString("SL", fSubHeader, Brushes.Black, xCol3 + wCol3 / 2, textHeaderY, center);
            g.DrawString("Đơn Giá", fSubHeader, Brushes.Black, xCol4 + wCol4 - 5, textHeaderY, right);
            g.DrawString("Thành Tiền", fSubHeader, Brushes.Black, xCol5 + wCol5 - 5, textHeaderY, right);

            yPos += headerHeight;

            // --- 6. NỘI DUNG DỊCH VỤ (ROWS) ---
            int stt = 1;
            float rowHeight = 40; // Tăng chiều cao mỗi dòng dữ liệu (trước là 30)

            foreach (DataRow row in dtDichVu.Rows)
            {
                string tenDV = row["TenDichVu"].ToString();
                string soLuong = row["SoLuong"].ToString();
                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                // Căn giữa chữ trong dòng cao 40px
                float textRowY = yPos + (rowHeight / 2);

                g.DrawString(stt++.ToString(), fBody, Brushes.Black, xCol1 + 5, textRowY, left);
                g.DrawString(tenDV, fBody, Brushes.Black, xCol2 + 5, textRowY, left);
                g.DrawString(soLuong, fBody, Brushes.Black, xCol3 + wCol3 / 2, textRowY, center);
                g.DrawString(donGia.ToString("N0"), fBody, Brushes.Black, xCol4 + wCol4 - 5, textRowY, right);
                g.DrawString(thanhTien.ToString("N0"), fBody, Brushes.Black, xCol5 + wCol5 - 5, textRowY, right);

                // Vẽ đường kẻ nằm sát đáy của rowHeight
                // yPos đang là đỉnh dòng, yPos + rowHeight là đáy dòng
                g.DrawLine(pDashed, xPos, yPos + rowHeight, xPos + width, yPos + rowHeight);

                yPos += rowHeight; // Xuống dòng tiếp theo
            }

            // --- 7. TỔNG KẾT TIỀN ---
            yPos += 20; // Khoảng cách từ dòng cuối cùng của bảng đến phần tổng tiền
            float xTotalLabel = xCol4 - 50;
            float xTotalValue = xCol5 + wCol5 - 5;
            float totalLineSpacing = 30; // Khoảng cách dòng trong phần tổng tiền

            // Tiền phòng
            g.DrawString("Tiền phòng:", fBody, Brushes.Black, xTotalLabel, yPos, right);
            g.DrawString(hd.TienPhong.ToString("N0") + " đ", fBody, Brushes.Black, xTotalValue, yPos, right);

            yPos += totalLineSpacing;

            // Tiền dịch vụ
            g.DrawString("Tiền dịch vụ:", fBody, Brushes.Black, xTotalLabel, yPos, right);
            g.DrawString(hd.TienDichVu.ToString("N0") + " đ", fBody, Brushes.Black, xTotalValue, yPos, right);

            // Đường kẻ tổng kết (vẽ cách chữ trên 1 khoảng và chữ dưới 1 khoảng)
            yPos += 15;
            g.DrawLine(pThick, xTotalLabel - 100, yPos, xPos + width, yPos);
            yPos += 15;

            // Tổng cộng
            g.DrawString("TỔNG THANH TOÁN:", fHeader, Brushes.Black, xTotalLabel, yPos, right);
            g.DrawString(hd.TongTienThanhToan.ToString("N0") + " đ", new Font("Segoe UI", 16, FontStyle.Bold), Brushes.Red, xTotalValue, yPos, right);

            // --- 8. FOOTER & CHỮ KÝ ---
            yPos += 100; // Đẩy phần chữ ký xuống xa hơn

            g.DrawString("Khách hàng", fSubHeader, Brushes.Black, xPos + 80, yPos, center);
            g.DrawString("Người lập phiếu", fSubHeader, Brushes.Black, width - 80, yPos, center);

            yPos += 25;
            g.DrawString("(Ký và ghi rõ họ tên)", fSmall, Brushes.Gray, xPos + 80, yPos, center);
            g.DrawString("(Ký và ghi rõ họ tên)", fSmall, Brushes.Gray, width - 80, yPos, center);

            // Lời cảm ơn cuối trang (Giữ nguyên logic đẩy cao footer)
            float footerY = e.PageBounds.Height - margin - 40;
            g.DrawLine(pThin, xPos, footerY, xPos + width, footerY);
            g.DrawString("Cảm ơn quý khách đã sử dụng dịch vụ! Hẹn gặp lại!", fSmall, Brushes.Black, e.PageBounds.Width / 2, footerY + 20, center);
        }

        private void BtnInHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int maHD = int.Parse(txtMaHD.Text);
                InHoaDon(maHD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in: " + ex.Message);
            }
        }
        #endregion
    }
}
