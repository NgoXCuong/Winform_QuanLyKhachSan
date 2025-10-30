using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;


namespace QuanLyKhachSan.UI
{
    public partial class Test : Form
    {
        private DatPhongService datPhongService = new DatPhongService();
        private PhongService phongService = new PhongService();
        private KhachHangService khachHangService = new KhachHangService();
        private DichVuService dichVuService = new DichVuService();
        private NhanVienModel nhanVienModel = new NhanVienModel();
        private int CurrentUserId => nhanVienModel.MaNV;

        public Test()
        {
            InitializeComponent();
            LoadLoaiPhong();
            LoadPhong(); // Hiển thị tất cả phòng ban đầu
            LoadKhachHang(); // 🧩 Thêm dòng này
            LoadListDichVu();
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


        private void Test_Load(object sender, EventArgs e)
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
        private void LoadKhachHang()
        {
            var listKH = khachHangService.GetAllKhachHang();

            // 🟢 Thêm mục đầu tiên "Chọn khách hàng"
            listKH.Insert(0, new KhachHangModel
            {
                MaKH = 0,
                HoTen = "— Chọn khách hàng —"
            });

            cboKhachHang.DataSource = listKH;
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "MaKH";
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
                Width = 20
            };
            dgvDichVu.Columns.Add(chkCol);

            // ====== Các cột thông tin ======
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "MaDV",
                HeaderText = "Mã DV",
                Width = 70
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "TenDichVu",
                HeaderText = "Tên Dịch Vụ",
                Width = 150
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn Giá",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "Đơn vị tính",
                Width = 80
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "MoTa",
                HeaderText = "Mô tả",
                Width = 200
            });

            // ====== Cột ảnh ======
            var imgCol = new DataGridViewImageColumn()
            {
                HeaderText = "Ảnh",
                Name = "Anh",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                Width = 120
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


        private void btnXacNhanDichVu_Click(object sender, EventArgs e)
        {
            var dichVuDaChon = GetDichVuDaChon();

            if (dichVuDaChon.Count == 0)
            {
                MessageBox.Show("Chưa chọn dịch vụ nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tienDichVu = 0;
                lblTienDichVuValue.Text = "0 VNĐ";
                CapNhatTongTien();
                return;
            }

            decimal tongTien = dichVuDaChon.Sum(dv => dv.DonGia);

            tienDichVu = tongTien;
            lblTienDichVuValue.Text = tongTien.ToString("N0") + " VNĐ";
            CapNhatTongTien();

            MessageBox.Show(
                $"Đã chọn {dichVuDaChon.Count} dịch vụ.\nTổng tiền dịch vụ: {tongTien:N0} VNĐ",
                "Xác nhận",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
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
            if (dgvDichVu.Columns.Contains("chkChon") && e.ColumnIndex == dgvDichVu.Columns["chkChon"].Index)
            {
                var dichVuDaChon = GetDichVuDaChon();
                tienDichVu = dichVuDaChon.Sum(dv => dv.DonGia);
                lblTienDichVuValue.Text = tienDichVu.ToString("N0") + " VNĐ";
                CapNhatTongTien();
            }
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra chọn phòng
                if (lvPhong.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn phòng cần đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ Lấy phòng được chọn
                var selectedItem = lvPhong.SelectedItems[0];
                if (!(selectedItem.Tag is PhongModel phong))
                {
                    MessageBox.Show("Không lấy được thông tin phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 3️⃣ Kiểm tra trạng thái
                if (phong.TrangThai?.Trim().ToLower() != "trống")
                {
                    MessageBox.Show($"Phòng {phong.SoPhong} hiện đang {phong.TrangThai}. Không thể đặt!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4️⃣ Kiểm tra khách hàng
                if (cboKhachHang.SelectedValue == null || (int)cboKhachHang.SelectedValue == 0)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5️⃣ Tính số ngày thuê
                int soNgay = (dtpNgayTra.Value - dtpNgayNhan.Value).Days;
                if (soNgay <= 0)
                {
                    MessageBox.Show("Ngày trả phải lớn hơn ngày nhận!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 6️⃣ Lấy tổng tiền (tiền phòng + dịch vụ)
                var dichVuDaChon = GetDichVuDaChon();
                decimal tienDV = dichVuDaChon.Sum(dv => dv.DonGia);
                decimal tienPhong = phong.GiaPhong * soNgay;
                decimal tongTien = tienPhong + tienDV;

                // 7️⃣ Tạo model đặt phòng
                DatPhongModel dp = new DatPhongModel
                {
                    MaKH = Convert.ToInt32(cboKhachHang.SelectedValue),
                    MaPhong = phong.MaPhong,
                    MaNV = CurrentUserId,
                    NgayNhanPhong = dtpNgayNhan.Value,
                    NgayTraPhong = dtpNgayTra.Value,
                    SoNguoi = (int)numSoNguoi.Value,
                    GhiChu = txtGhiChu.Text,
                    TongTien = tongTien,
                    TrangThai = "Đang đặt",
                    NgayTao = DateTime.Now
                };

                // 8️⃣ Gọi Service xử lý
                //bool success = datPhongService.InsertDatPhong(dp);

                //if (success)
                //{
                //    MessageBox.Show(
                //        $"✅ Đặt phòng {phong.SoPhong} thành công!\n" +
                //        $"Thời gian: {soNgay} ngày\n" +
                //        $"Tổng tiền: {tongTien:N0} VNĐ",
                //        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information
                //    );

                //    // 9️⃣ Cập nhật trạng thái phòng sang "Có khách"
                //    phong.TrangThai = "Có khách";
                //    phongService.SuaPhong(phong);

                //    // 🔁 Reload danh sách phòng
                //    LoadPhong();
                //}
                //else
                //{
                //    MessageBox.Show("❌ Đặt phòng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đặt phòng: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
