using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan.UI
{
    public partial class BookingRoom : Form
    {
        private DatPhongService datPhongService = new DatPhongService();
        private PhongService phongService = new PhongService();
        private KhachHangService khachHangService = new KhachHangService();
        private DichVuService dichVuService = new DichVuService();
        private NhanVienModel nhanVienModel = new NhanVienModel();

        public BookingRoom()
        {
            InitializeComponent();
            //LoadLoaiPhong();
            LoadPhong(); // Hiển thị tất cả phòng ban đầu
            LoadKhachHang(); // 🧩 Thêm dòng này
            LoadListDichVu();

            LoadDatPhong();
        }

        // ======================= PHONG ===============================
        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                Bitmap bmp = new Bitmap(120, 90);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.LightGray);
                    g.DrawString("No Image", new Font("Segoe UI", 8), Brushes.Black, new PointF(10, 35));
                }
                return bmp;
            }

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void LoadPhong(string trangThaiLoc = "", string tenLoaiPhongLoc = "")
        {
            try
            {
                // 🔹 Xóa tất cả tab cũ
                tabControlFloors.TabPages.Clear();

                // 🔹 Lấy danh sách phòng từ DB
                List<PhongModel> danhSachPhong = phongService.GetAllPhong();

                // 🔹 Nhóm phòng theo tầng
                var phongTheoTang = danhSachPhong
                    .Where(phong =>
                    {
                        string trangThai = phong.TrangThai?.Trim().ToLower() ?? "";
                        string tenLoaiPhong = phong.TenLoaiPhong?.Trim().ToLower() ?? "";

                        // 🔹 Lọc theo loại phòng (nếu có chọn)
                        if (!string.IsNullOrEmpty(tenLoaiPhongLoc) && tenLoaiPhong != tenLoaiPhongLoc.ToLower())
                            return false;

                        // 🔹 Lọc theo trạng thái (nếu có chọn)
                        if (!string.IsNullOrEmpty(trangThaiLoc) && trangThai != trangThaiLoc.ToLower())
                            return false;

                        return true;
                    })
                    .GroupBy(p => p.Tang)
                    .OrderBy(g => g.Key);

                // 🔹 Tạo tab cho mỗi tầng
                foreach (var nhomTang in phongTheoTang)
                {
                    // 🔹 Tạo tab page cho tầng
                    TabPage tabPage = new TabPage($"Tầng {nhomTang.Key}");
                    tabPage.BackColor = Color.White;

                    // 🔹 Tạo ListView cho tầng này
                    ListView lvTang = new ListView();
                    lvTang.View = View.LargeIcon;
                    lvTang.LargeImageList = imageListPhong;
                    lvTang.FullRowSelect = true;
                    lvTang.GridLines = true;
                    lvTang.HideSelection = false;
                    lvTang.MultiSelect = false;
                    lvTang.Dock = DockStyle.Fill;
                    lvTang.Font = new Font("Roboto Condensed", 12F, FontStyle.Regular);
                    lvTang.SelectedIndexChanged += LvTang_SelectedIndexChanged;

                    // 🔹 Thêm phòng vào ListView của tầng
                    foreach (var phong in nhomTang.OrderBy(p => p.SoPhong))
                    {
                        // 🔹 Chuyển ảnh từ DB sang Image
                        Image img = ByteArrayToImage(phong.Anh);

                        // 🔹 Thêm ảnh vào ImageList
                        imageListPhong.Images.Add(img);

                        // 🔹 Tạo item hiển thị
                        string tenItem = $"{phong.SoPhong}\n({phong.TrangThai ?? "Không rõ"})";
                        ListViewItem item = new ListViewItem(tenItem)
                        {
                            ImageIndex = imageListPhong.Images.Count - 1,
                            Tag = phong
                        };

                        lvTang.Items.Add(item);
                    }

                    // 🔹 Thêm ListView vào tab
                    tabPage.Controls.Add(lvTang);
                    tabControlFloors.TabPages.Add(tabPage);
                }

                // 🔹 Nếu không có phòng nào, hiển thị thông báo
                if (tabControlFloors.TabPages.Count == 0)
                {
                    TabPage emptyTab = new TabPage("Không có phòng");
                    Label lblEmpty = new Label();
                    lblEmpty.Text = "Không có phòng nào phù hợp với bộ lọc.";
                    lblEmpty.AutoSize = true;
                    lblEmpty.Font = new Font("Roboto Condensed", 12F, FontStyle.Regular);
                    lblEmpty.Location = new Point(20, 20);
                    emptyTab.Controls.Add(lblEmpty);
                    tabControlFloors.TabPages.Add(emptyTab);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BookingRoom_Load(object sender, EventArgs e)
        {
            rbTatCa.Checked = true; // Load tất cả phòng khi mở form
            TinhSoNgay(); // Tính số ngày ban đầu
            CapNhatTongTien(); // Cập nhật tổng tiền ban đầu
            btnDatPhong.Enabled = false; // Mặc định vô hiệu hóa nút đặt phòng
        }

        private void rbTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTatCa.Checked)
                LoadPhong(); // Hiển thị tất cả
        }

        private void rbTrong_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTrong.Checked)
                LoadPhong("Trống");
        }

        private void rbCoKhach_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCoKhach.Checked)
                LoadPhong("Có khách");
        }

        private void rbBaoTri_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBaoTri.Checked)
                LoadPhong("Bảo trì");
        }

        //private void LoadLoaiPhong()
        //{
        //    LoaiPhongService loaiPhongService = new LoaiPhongService();  // hoặc thông qua service nếu có
        //    var listLoaiPhong = loaiPhongService.GetAllLoaiPhong();

        //    // 🟢 Thêm mục "Tất cả" ở đầu danh sách
        //    listLoaiPhong.Insert(0, new LoaiPhongModel
        //    {
        //        MaLoaiPhong = 0,
        //        TenLoaiPhong = "Tất cả"
        //    });
        //}

        // Event handler cho việc chọn phòng trong các tab tầng
        private void LvTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lvSender = sender as ListView;
            if (lvSender == null || lvSender.SelectedItems.Count == 0)
            {
                lblSelectedRoomInfo.Text = "⚠️ Chưa chọn phòng nào.";
                tienPhong = 0;
                CapNhatTongTien();
                btnDatPhong.Enabled = false; // Vô hiệu hóa nút khi không chọn phòng
                return;
            }

            var selectedItem = lvSender.SelectedItems[0];

            if (selectedItem.Tag is PhongModel phong)
            {
                // 🧩 Hiển thị thông tin phòng
                lblSelectedRoomInfo.Text =
                    $"🆔 Mã phòng: {phong.MaPhong}\n" +
                    $"🏠 Số phòng: {phong.SoPhong}\n" +
                    $"🏷️ Loại phòng: {phong.TenLoaiPhong}\n" +
                    $"💰 Giá cơ bản: {phong.GiaPhong:N0} VNĐ\n" +
                    $"👥 Sức chứa tối đa: {phong.SucChuaToiDa} người\n" +
                    $"📶 Trạng thái: {phong.TrangThai}\n" +
                    $"🏢 Tầng: {phong.Tang}\n" +
                    $"📝 Mô tả: {phong.MoTa}";

                // 🧮 Cập nhật giá phòng
                tienPhong = phong.GiaPhong;

                // 🏷️ Hiển thị trên giao diện (nếu có label giá phòng)
                if (lblTienPhongValue != null)
                    lblTienPhongValue.Text = phong.GiaPhong.ToString("N0") + " VNĐ";

                // 🔁 Cập nhật tổng tiền
                CapNhatTongTien();

                // 🛑 Kiểm tra trạng thái phòng
                if (phong.TrangThai?.Trim().ToLower() == "có khách")
                {
                    btnDatPhong.Enabled = false;
                    MessageBox.Show("Phòng này đã có khách! Không thể đặt phòng.", "Phòng đã kín",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    btnDatPhong.Enabled = true;
                }
            }
        }

        // Phương thức lấy phòng đã chọn từ các tab
        private PhongModel GetSelectedRoom()
        {
            foreach (TabPage tab in tabControlFloors.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0] is ListView lv)
                {
                    if (lv.SelectedItems.Count > 0 && lv.SelectedItems[0].Tag is PhongModel phong)
                    {
                        return phong;
                    }
                }
            }
            return null;
        }

        private void lvPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPhong.SelectedItems.Count > 0)
            {
                var selectedItem = lvPhong.SelectedItems[0];

                if (selectedItem.Tag is PhongModel phong)
                {
                    // 🧩 Hiển thị thông tin phòng
                    lblSelectedRoomInfo.Text =
                        $"🆔 Mã phòng: {phong.MaPhong}\n" +
                        $"🏠 Số phòng: {phong.SoPhong}\n" +
                        $"🏷️ Loại phòng: {phong.TenLoaiPhong}\n" +
                        $"💰 Giá cơ bản: {phong.GiaPhong:N0} VNĐ\n" +
                        $"👥 Sức chứa tối đa: {phong.SucChuaToiDa} người\n" +
                        $"📶 Trạng thái: {phong.TrangThai}\n" +
                        $"🏢 Tầng: {phong.Tang}\n" +
                        $"📝 Mô tả: {phong.MoTa}";

                    // 🧮 Cập nhật giá phòng
                    tienPhong = phong.GiaPhong;

                    // 🏷️ Hiển thị trên giao diện (nếu có label giá phòng)
                    if (lblTienPhongValue != null)
                        lblTienPhongValue.Text = phong.GiaPhong.ToString("N0") + " VNĐ";

                    // 🔁 Cập nhật tổng tiền
                    CapNhatTongTien();
                }
            }
            else
            {
                lblSelectedRoomInfo.Text = "⚠️ Chưa chọn phòng nào.";
                tienPhong = 0;
                CapNhatTongTien();
            }
        }

        // ============================ KHACH HANG =====================
        private void LoadKhachHang(int? maKhachHangMoi = null)
        {
            var listKH = khachHangService.GetAllKhachHang();
            listKH.Insert(0, new KhachHangModel { MaKH = 0, HoTen = "— Chọn khách hàng —" });

            cboKhachHang.DataSource = listKH;
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "MaKH";

            // 🔹 Nếu có khách hàng mới, chọn luôn
            if (maKhachHangMoi.HasValue)
                cboKhachHang.SelectedValue = maKhachHangMoi.Value;
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedItem is KhachHangModel selectedKH && selectedKH.MaKH != 0)
            {
                txtHoTen.Text = selectedKH.HoTen;
                txtEmail.Text = selectedKH.Email;
                txtSoDienThoai.Text = selectedKH.SoDienThoai;
                txtCCCD.Text = selectedKH.CCCD;
            }
            else
            {
                txtHoTen.Text = "";
                txtEmail.Text = "";
                txtSoDienThoai.Text = "";
                txtCCCD.Text = "";
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            using (var frm = new KhachHangForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadKhachHang(frm.MaKhachHangMoi);
                }
            }
        }

        // ================================ DICH VU =============================
        private void LoadListDichVu()
        {
            // Lấy dữ liệu
            var list = dichVuService.GetAllDichVu();

            dgvDichVu.DataSource = null;
            dgvDichVu.AutoGenerateColumns = false;
            dgvDichVu.Columns.Clear();

            // ====== Cột checkbox chọn dịch vụ ======
            var chkCol = new DataGridViewCheckBoxColumn()
            {
                Name = "chkChon",
                HeaderText = "Chọn",
                //Width = 10
            };
            dgvDichVu.Columns.Add(chkCol);

            // ====== Cột nhập số lượng ======
            var soLuongCol = new DataGridViewTextBoxColumn()
            {
                Name = "SoLuong",
                HeaderText = "Số lượng",
                //Width = 10,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvDichVu.Columns.Add(soLuongCol);


            // ====== Các cột thông tin ======
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "MaDV",
                HeaderText = "Mã DV",
                //Width = 50
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "TenDichVu",
                HeaderText = "Tên Dịch Vụ",
                //Width = 150
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn Giá",
                //Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "Đơn vị tính",
                //Width = 80
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "MoTa",
                HeaderText = "Mô tả",
                //Width = 200
            });

            // ====== Cột ảnh ======
            var imgCol = new DataGridViewImageColumn()
            {
                HeaderText = "Ảnh",
                Name = "Anh",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                //Width = 120
            };
            dgvDichVu.Columns.Add(imgCol);

            // ====== Gán dữ liệu ======
            dgvDichVu.DataSource = list;

            // ====== Sau khi bind xong, hiển thị ảnh ======
            dgvDichVu.DataBindingComplete += (s, e) =>
            {
                foreach (DataGridViewRow row in dgvDichVu.Rows)
                {
                    if (row.DataBoundItem is DichVuModel dv && dv.Anh != null && dv.Anh.Length > 0)
                    {
                        try
                        {
                            using (MemoryStream ms = new MemoryStream(dv.Anh))
                            {
                                row.Cells["Anh"].Value = Image.FromStream(ms);
                            }
                        }
                        catch
                        {
                            row.Cells["Anh"].Value = null;
                        }
                    }
                    else
                    {
                        row.Cells["Anh"].Value = null;
                    }
                }
            };

            dgvDichVu.CellValueChanged -= dgvDichVu_CellValueChanged; // tránh gắn trùng
            dgvDichVu.CellValueChanged += dgvDichVu_CellValueChanged;

        }



        private List<DichVuModel> GetDichVuDaChon()
        {
            var dichVuDaChon = new List<DichVuModel>();

            foreach (DataGridViewRow row in dgvDichVu.Rows)
            {
                if (row.Cells["chkChon"].Value is bool isChecked && isChecked)
                {
                    if (row.DataBoundItem is DichVuModel dv)
                        dichVuDaChon.Add(dv);
                }
            }

            return dichVuDaChon;
        }

        // ==================== TÍNH TOÁN TỔNG TIỀN ====================
        private DateTime? ngayNhan;
        private DateTime? ngayTra;
        private decimal tienPhong = 0;
        private decimal tienDichVu = 0;

        // Khi chọn ngày nhận
        private void dtpNgayNhan_ValueChanged(object sender, EventArgs e)
        {
            ngayNhan = dtpNgayNhan.Value;
            TinhSoNgay();
        }

        // Khi chọn ngày trả
        private void dtpNgayTra_ValueChanged(object sender, EventArgs e)
        {
            ngayTra = dtpNgayTra.Value;
            TinhSoNgay();
        }

        private void TinhSoNgay()
        {
            if (ngayNhan.HasValue && ngayTra.HasValue && ngayTra > ngayNhan)
            {
                int soNgay = (ngayTra.Value - ngayNhan.Value).Days;
                lblSoNgayValue.Text = soNgay.ToString();
                CapNhatTongTien();
            }
            else
            {
                lblSoNgayValue.Text = "0";
                lblTongTienValue.Text = "0";
            }
        }

        private void CapNhatTongTien()
        {
            if (decimal.TryParse(lblSoNgayValue.Text, out decimal soNgay))
            {
                decimal tong = (tienPhong * soNgay) + tienDichVu;
                lblTongTienValue.Text = tong.ToString("N0") + " VNĐ";
            }
        }

        private void dgvDichVu_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDichVu.Columns.Contains("chkChon"))
            {
                // Nếu thay đổi ở cột "chọn" hoặc "số lượng"
                if (e.ColumnIndex == dgvDichVu.Columns["chkChon"].Index ||
                    e.ColumnIndex == dgvDichVu.Columns["SoLuong"].Index)
                {
                    var dichVuDaChon = new List<(DichVuModel dv, int soLuong)>();

                    foreach (DataGridViewRow row in dgvDichVu.Rows)
                    {
                        if (row.Cells["chkChon"].Value is bool isChecked && isChecked)
                        {
                            int soLuong = 1;
                            if (row.Cells["SoLuong"].Value != null &&
                                int.TryParse(row.Cells["SoLuong"].Value.ToString(), out int sl) &&
                                sl > 0)
                            {
                                soLuong = sl;
                            }

                            if (row.DataBoundItem is DichVuModel dv)
                                dichVuDaChon.Add((dv, soLuong));
                        }
                    }

                    // 🧮 Tính tổng tiền dịch vụ
                    tienDichVu = dichVuDaChon.Sum(x => x.dv.DonGia * x.soLuong);
                    lblTienDichVuValue.Text = tienDichVu.ToString("N0") + " VNĐ";

                    CapNhatTongTien();
                }
            }
        }

        
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            try
            {
                // ======= 1️⃣ KIỂM TRA DỮ LIỆU =======
                PhongModel phong = GetSelectedRoom();
                if (phong == null)
                {
                    MessageBox.Show("⚠️ Vui lòng chọn phòng cần đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!(cboKhachHang.SelectedItem is KhachHangModel kh) || kh.MaKH == 0)
                {
                    MessageBox.Show("⚠️ Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ngayNhan.HasValue || !ngayTra.HasValue || ngayTra <= ngayNhan)
                {
                    MessageBox.Show("⚠️ Ngày nhận và ngày trả không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ======= 2️⃣ LẤY DỊCH VỤ =======
                var dsDichVu = new List<DatPhongDichVuModel>();
                foreach (DataGridViewRow row in dgvDichVu.Rows)
                {
                    if (row.Cells["chkChon"].Value is bool isChecked && isChecked)
                    {
                        if (row.DataBoundItem is DichVuModel dv)
                        {
                            int soLuong = int.TryParse(Convert.ToString(row.Cells["SoLuong"].Value), out int sl) && sl > 0 ? sl : 1;
                            dsDichVu.Add(new DatPhongDichVuModel
                            {
                                MaDV = dv.MaDV,
                                SoLuong = soLuong,
                                DonGia = dv.DonGia,
                                NgaySuDung = DateTime.Now
                            });
                        }
                    }
                }

                // ======= 3️⃣ LẤY TỔNG TIỀN TỪ UI =======
                decimal tongTienFromUI = 0;
                if (decimal.TryParse(lblTongTienValue.Text.Replace(" VNĐ", "").Replace(",", ""), out decimal parsedTongTien))
                {
                    tongTienFromUI = parsedTongTien;
                }

                // ======= 4️⃣ TẠO ĐẶT PHÒNG =======
                var datPhong = new DatPhongModel
                {
                    MaPhong = phong.MaPhong,
                    MaKH = kh.MaKH,
                    MaNV = SessionInfo.CurrentUser?.MaNV ?? 0,
                    NgayNhanPhong = ngayNhan.Value,
                    NgayTraPhong = ngayTra.Value,
                    SoNguoi = (int)numSoNguoi.Value,
                    TongTien = tongTienFromUI, // Lấy từ UI thay vì tính lại
                    TrangThai = "Chờ xác nhận",
                    GhiChu = txtGhiChu.Text.Trim(),
                    NgayTao = DateTime.Now
                };

                int maDatPhongMoi = datPhongService.ThemDatPhong(datPhong, dsDichVu);

                if (maDatPhongMoi <= 0)
                {
                    MessageBox.Show("❌ Đặt phòng thất bại (InsertDatPhong trả về 0)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                phongService.CapNhatTrangThaiPhong(phong.MaPhong, "Có khách");

                // Tính tổng tiền để hiển thị trong thông báo thành công
                int soNgay = Math.Max(1, (ngayTra.Value - ngayNhan.Value).Days);
                decimal tienPhongTotal = phong.GiaPhong * soNgay;
                decimal tienDichVuTotal = dsDichVu.Sum(dv => dv.DonGia * dv.SoLuong);
                decimal tongTien = tienPhongTotal + tienDichVuTotal;

                MessageBox.Show($"✅ Đặt phòng thành công!\nTổng tiền: {tongTien:N0} VNĐ",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadPhong();
                ResetFormDatPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("🔥 Lỗi khi đặt phòng!\n\n" + ex.ToString(),
                    "Lỗi chi tiết", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ResetFormDatPhong()
        {
            cboKhachHang.SelectedIndex = 0;
            txtHoTen.Clear();
            txtEmail.Clear();
            txtSoDienThoai.Clear();
            txtCCCD.Clear();
            dtpNgayNhan.Value = DateTime.Now;
            dtpNgayTra.Value = DateTime.Now.AddDays(1);
            lblSoNgayValue.Text = "0";
            lblTienDichVuValue.Text = "0 VNĐ";
            lblTienPhongValue.Text = "0 VNĐ";
            lblTongTienValue.Text = "0 VNĐ";

            // Bỏ chọn phòng trong tất cả các tab
            foreach (TabPage tab in tabControlFloors.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0] is ListView lv)
                {
                    lv.SelectedItems.Clear();
                }
            }

            // Bỏ chọn dịch vụ
            foreach (DataGridViewRow row in dgvDichVu.Rows)
            {
                row.Cells["chkChon"].Value = false;
                row.Cells["SoLuong"].Value = null;
            }
        }

        private void LoadDatPhong()
        {
            try
            {
                var list = datPhongService.GetAllDatPhong();
                dgvDatPhong.DataSource = list;

                // Tuỳ chọn hiển thị
                dgvDatPhong.Columns["MaDatPhong"].HeaderText = "Mã Đặt Phòng";
                dgvDatPhong.Columns["MaKH"].HeaderText = "Khách Hàng";
                dgvDatPhong.Columns["MaPhong"].HeaderText = "Phòng";
                dgvDatPhong.Columns["NgayNhanPhong"].HeaderText = "Ngày Nhận";
                dgvDatPhong.Columns["NgayTraPhong"].HeaderText = "Ngày Trả";
                dgvDatPhong.Columns["SoNguoi"].HeaderText = "Số Người";

                // Định dạng cột Tổng Tiền
                if (dgvDatPhong.Columns.Contains("TongTien"))
                {
                    dgvDatPhong.Columns["TongTien"].HeaderText = "Tổng Tiền";
                    dgvDatPhong.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    dgvDatPhong.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                dgvDatPhong.Columns["TrangThai"].HeaderText = "Trạng Thái";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách đặt phòng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            //LoadLoaiPhong();
            LoadPhong(); // Hiển thị tất cả phòng ban đầu
            LoadKhachHang(); // 🧩 Thêm dòng này
            LoadListDichVu();
            LoadDatPhong();
        }

        private int? maDatPhongDangChon = null;

        private void btnXoaDatPhong_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra đã chọn dòng nào chưa
                if (maDatPhongDangChon == null)
                {
                    MessageBox.Show("⚠️ Vui lòng chọn đặt phòng cần xóa!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ Xác nhận xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa đặt phòng Mã {maDatPhongDangChon}?",
                                             "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                // 3️⃣ Lấy thông tin đặt phòng (để biết mã phòng cần cập nhật trạng thái)
                var datPhong = datPhongService.GetDatPhongById(maDatPhongDangChon.Value);
                if (datPhong == null)
                {
                    MessageBox.Show("❌ Không tìm thấy thông tin đặt phòng trong cơ sở dữ liệu!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 4️⃣ Gọi service xóa DB
                bool success = datPhongService.XoaDatPhong(maDatPhongDangChon.Value);
                if (success)
                {
                    // 5️⃣ Cập nhật trạng thái phòng sau khi xóa đặt phòng
                    phongService.CapNhatTrangThaiPhong(datPhong.MaPhong, "Trống");

                    MessageBox.Show("✅ Xóa đặt phòng thành công!", "Thành công",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 6️⃣ Làm mới dữ liệu
                    LoadDatPhong();
                    LoadPhong();
                    maDatPhongDangChon = null;
                }
                else
                {
                    MessageBox.Show("❌ Xóa đặt phòng thất bại!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("🔥 Lỗi khi xóa đặt phòng:\n" + ex.Message,
                                "Lỗi chi tiết", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void dgvDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvDatPhong.Rows[e.RowIndex];
            maDatPhongDangChon = Convert.ToInt32(row.Cells["MaDatPhong"].Value);

            // ======================
            // 1️⃣ HIỂN THỊ KHÁCH HÀNG
            // ======================
            int maKH = Convert.ToInt32(row.Cells["MaKH"].Value);
            cboKhachHang.SelectedValue = maKH;

            var kh = khachHangService.LayThongTinKhachHang(maKH);
            if (kh != null)
            {
                txtHoTen.Text = kh.HoTen;
                txtEmail.Text = kh.Email;
                txtSoDienThoai.Text = kh.SoDienThoai;
                txtCCCD.Text = kh.CCCD;
            }

            // ======================
            // 2️⃣ HIỂN THỊ PHÒNG
            // ======================
            int maPhong = Convert.ToInt32(row.Cells["MaPhong"].Value);
            var phong = phongService.GetById(maPhong);
            if (phong != null)
            {
                lblSelectedRoomInfo.Text =
                    $"🆔 Mã phòng: {phong.MaPhong}\n" +
                    $"🏠 Số phòng: {phong.SoPhong}\n" +
                    $"🏷️ Loại phòng: {phong.TenLoaiPhong}\n" +
                    $"💰 Giá cơ bản: {phong.GiaPhong:N0} VNĐ\n" +
                    $"👥 Sức chứa tối đa: {phong.SucChuaToiDa} người\n" +
                    $"📶 Trạng thái: {phong.TrangThai}\n" +
                    $"🏢 Tầng: {phong.Tang}";
            }

            // ===== Chọn lại phòng trong các tab tầng =====
            foreach (TabPage tab in tabControlFloors.TabPages)
            {
                if (tab.Controls.Count > 0 && tab.Controls[0] is ListView lv)
                {
                    bool found = false;
                    foreach (ListViewItem item in lv.Items)
                    {
                        item.Selected = false;
                        if (item.Tag is PhongModel p && p.MaPhong == maPhong)
                        {
                            item.Selected = true;
                            item.EnsureVisible();
                            // Chuyển đến tab chứa phòng này
                            tabControlFloors.SelectedTab = tab;
                            found = true;
                            break;
                        }
                    }
                    if (found) break;
                }
            }

            // ======================
            // 3️⃣ HIỂN THỊ NGÀY GIỜ VÀ TRẠNG THÁI
            // ======================
            if (DateTime.TryParse(row.Cells["NgayNhanPhong"].Value?.ToString(), out DateTime ngayNhan))
                dtpNgayNhan.Value = ngayNhan;

            if (DateTime.TryParse(row.Cells["NgayTraPhong"].Value?.ToString(), out DateTime ngayTra))
                dtpNgayTra.Value = ngayTra;

            if (int.TryParse(row.Cells["SoNguoi"].Value?.ToString(), out int soNguoi))
                numSoNguoi.Value = soNguoi;

            cboTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();
            lblTongTienValue.Text = $"{Convert.ToDecimal(row.Cells["TongTien"].Value):N0} VNĐ";

            if (dgvDatPhong.Columns.Contains("GhiChu"))
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();

            var dsDichVuDaDat = datPhongService.GetDichVuByDatPhong(maDatPhongDangChon.Value);

            // Reset tất cả checkbox dịch vụ
            foreach (DataGridViewRow dvRow in dgvDichVu.Rows)
            {
                dvRow.Cells["chkChon"].Value = false;
                dvRow.Cells["SoLuong"].Value = null;
            }

            // Tick lại dịch vụ đã đặt
            foreach (var dv in dsDichVuDaDat)
            {
                foreach (DataGridViewRow dvRow in dgvDichVu.Rows)
                {
                    if (dvRow.DataBoundItem is DichVuModel dvModel && dvModel.MaDV == dv.MaDV)
                    {
                        dvRow.Cells["chkChon"].Value = true;
                        dvRow.Cells["SoLuong"].Value = dv.SoLuong;
                        break;
                    }
                }
            }

            // Cập nhật tổng tiền dịch vụ
            tienDichVu = dsDichVuDaDat.Sum(d => d.DonGia * d.SoLuong);
            lblTienDichVuValue.Text = tienDichVu.ToString("N0") + " VNĐ";

            // Cập nhật lại tổng tiền toàn bộ
            CapNhatTongTien();
        }

        private void btnSuaDatPhong_Click(object sender, EventArgs e)
        {
            if (maDatPhongDangChon == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn một đặt phòng để sửa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1️⃣ Lấy dữ liệu từ giao diện
                PhongModel selectedRoom = GetSelectedRoom();
                var dp = new DatPhongModel
                {
                    MaDatPhong = maDatPhongDangChon.Value,
                    MaKH = Convert.ToInt32(cboKhachHang.SelectedValue),
                    MaPhong = selectedRoom?.MaPhong ?? 0,
                    MaNV = SessionInfo.CurrentUser?.MaNV ?? 0,
                    NgayNhanPhong = dtpNgayNhan.Value,
                    NgayTraPhong = dtpNgayTra.Value,
                    SoNguoi = (int)numSoNguoi.Value,
                    TrangThai = cboTrangThai.Text,
                    GhiChu = txtGhiChu.Text.Trim()
                };

                // 2️⃣ Lấy danh sách dịch vụ đã chọn
                var dsDichVuDaDat = new List<DatPhongDichVuModel>();
                foreach (DataGridViewRow row in dgvDichVu.Rows)
                {
                    bool chon = Convert.ToBoolean(row.Cells["chkChon"].Value ?? false);
                    if (chon && row.DataBoundItem is DichVuModel dv)
                    {
                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value ?? 1);
                        dsDichVuDaDat.Add(new DatPhongDichVuModel
                        {
                            MaDatPhong = dp.MaDatPhong,
                            MaDV = dv.MaDV,
                            SoLuong = soLuong,
                            DonGia = dv.DonGia,
                            NgaySuDung = DateTime.Now
                        });
                    }
                }

                // 3️⃣ Lấy tổng tiền từ UI
                decimal tongTien = 0;
                if (decimal.TryParse(lblTongTienValue.Text.Replace(" VNĐ", "").Replace(",", ""), out decimal parsedTongTien))
                {
                    tongTien = parsedTongTien;
                }
                dp.TongTien = tongTien; // Gán tổng tiền từ UI

                // 4️⃣ Gọi service cập nhật đặt phòng
                bool result = datPhongService.SuaDatPhong(dp);

                if (result)
                {
                    // 5️⃣ Cập nhật dịch vụ
                    datPhongService.CapNhatDichVu(dp.MaDatPhong, dsDichVuDaDat);

                    // 6️⃣ Làm mới giao diện
                    MessageBox.Show("✅ Cập nhật đặt phòng thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDatPhong();
                    LoadPhong();
                }
                else
                {
                    MessageBox.Show("❌ Cập nhật đặt phòng thất bại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("🔥 Lỗi khi sửa đặt phòng:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
