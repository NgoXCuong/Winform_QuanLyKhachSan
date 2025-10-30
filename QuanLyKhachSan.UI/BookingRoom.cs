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
            LoadLoaiPhong();
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

        private void LoadPhong(string trangThaiLoc = "", int maLoaiPhongLoc = 0)
        {
            try
            {
                lvPhong.BeginUpdate();
                lvPhong.View = View.LargeIcon;
                lvPhong.LargeImageList = imageListPhong;
                lvPhong.Items.Clear();
                imageListPhong.Images.Clear(); // 🔹 Xóa ảnh cũ trước khi load mới

                // 🔹 Lấy danh sách phòng từ DB
                List<PhongModel> danhSachPhong = phongService.GetAllPhong();

                foreach (var phong in danhSachPhong)
                {
                    string trangThai = phong.TrangThai?.Trim().ToLower() ?? "";

                    // 🔹 Lọc theo loại phòng (nếu có chọn)
                    if (maLoaiPhongLoc != 0 && phong.MaLoaiPhong != maLoaiPhongLoc)
                        continue;

                    // 🔹 Lọc theo trạng thái (nếu có chọn)
                    if (!string.IsNullOrEmpty(trangThaiLoc) && trangThai != trangThaiLoc.ToLower())
                        continue;

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

                    lvPhong.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lvPhong.EndUpdate();
            }
        }

        private void BookingRoom_Load(object sender, EventArgs e)
        {
            rbTatCa.Checked = true; // Load tất cả phòng khi mở form
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

        private void LoadLoaiPhong()
        {
            LoaiPhongService loaiPhongService = new LoaiPhongService();  // hoặc thông qua service nếu có
            var listLoaiPhong = loaiPhongService.GetAllLoaiPhong();

            // 🟢 Thêm mục "Tất cả" ở đầu danh sách
            listLoaiPhong.Insert(0, new LoaiPhongModel
            {
                MaLoaiPhong = 0,
                TenLoaiPhong = "Tất cả"
            });

            cboLoaiPhong.DataSource = listLoaiPhong;
            cboLoaiPhong.DisplayMember = "TenLoaiPhong";
            cboLoaiPhong.ValueMember = "MaLoaiPhong";
        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiPhong.SelectedItem is LoaiPhongModel selectedLoaiPhong)
            {
                int maLoaiPhong = selectedLoaiPhong.MaLoaiPhong;

                // Nếu có radio trạng thái đang chọn:
                string trangThaiLoc = "";
                if (rbTrong.Checked) trangThaiLoc = "trống";
                else if (rbCoKhach.Checked) trangThaiLoc = "có khách";
                else if (rbBaoTri.Checked) trangThaiLoc = "bảo trì";

                LoadPhong(trangThaiLoc, maLoaiPhong);
            }
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
                        $"🏢 Tầng: {phong.Tang}";

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
                if (lvPhong.SelectedItems.Count == 0)
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

                var phong = lvPhong.SelectedItems[0].Tag as PhongModel;
                if (phong == null)
                {
                    MessageBox.Show("❌ Không thể lấy thông tin phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // ======= 3️⃣ TÍNH TỔNG TIỀN =======
                int soNgay = Math.Max(1, (ngayTra.Value - ngayNhan.Value).Days);
                decimal tienPhongTotal = phong.GiaPhong * soNgay;
                decimal tienDichVuTotal = dsDichVu.Sum(dv => dv.DonGia * dv.SoLuong);
                decimal tongTien = tienPhongTotal + tienDichVuTotal;

                var datPhong = new DatPhongModel
                {
                    MaPhong = phong.MaPhong,
                    MaKH = kh.MaKH,
                    MaNV = SessionInfo.CurrentUser?.MaNV ?? 0,
                    NgayNhanPhong = ngayNhan.Value,
                    NgayTraPhong = ngayTra.Value,
                    SoNguoi = (int)numSoNguoi.Value,
                    TongTien = tongTien,
                    TrangThai = "Chờ xác nhận",
                    GhiChu = txtGhiChu.Text.Trim(), // 🟢 thêm dòng này
                    NgayTao = DateTime.Now
                };

                int maDatPhongMoi = datPhongService.ThemDatPhong(datPhong, dsDichVu);

                if (maDatPhongMoi <= 0)
                {
                    MessageBox.Show("❌ Đặt phòng thất bại (InsertDatPhong trả về 0)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                phongService.CapNhatTrangThaiPhong(phong.MaPhong, "Có khách");

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

            // Bỏ chọn phòng
            lvPhong.SelectedItems.Clear();

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
                dgvDatPhong.Columns["TongTien"].HeaderText = "Tổng Tiền (VNĐ)";
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
            LoadLoaiPhong();
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDatPhong.Rows[e.RowIndex];
                maDatPhongDangChon = Convert.ToInt32(row.Cells["MaDatPhong"].Value); // 👈 đổi tên ở đây
            }
        }
    }
}
