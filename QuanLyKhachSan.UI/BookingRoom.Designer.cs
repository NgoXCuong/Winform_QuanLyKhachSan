namespace QuanLyKhachSan.UI
{
    partial class BookingRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTotal = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSuaDatPhong = new System.Windows.Forms.Button();
            this.btnXoaDatPhong = new System.Windows.Forms.Button();
            this.btnDatPhong = new System.Windows.Forms.Button();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.lblTongTienValue = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblTienDichVuValue = new System.Windows.Forms.Label();
            this.lblTienDichVu = new System.Windows.Forms.Label();
            this.lblTienPhongValue = new System.Windows.Forms.Label();
            this.lblTienPhong = new System.Windows.Forms.Label();
            this.lblSoNgayValue = new System.Windows.Forms.Label();
            this.lblSoNgay = new System.Windows.Forms.Label();
            this.pnlSelectedRoom = new System.Windows.Forms.GroupBox();
            this.lblSelectedRoomInfo = new System.Windows.Forms.Label();
            this.pnlRoomList = new System.Windows.Forms.GroupBox();
            this.lvPhong = new System.Windows.Forms.ListView();
            this.pnlRoomFilter = new System.Windows.Forms.Panel();
            this.rbBaoTri = new System.Windows.Forms.RadioButton();
            this.rbCoKhach = new System.Windows.Forms.RadioButton();
            this.rbTrong = new System.Windows.Forms.RadioButton();
            this.rbTatCa = new System.Windows.Forms.RadioButton();
            this.lblLoaiPhong = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCustomer = new System.Windows.Forms.GroupBox();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblChonKH = new System.Windows.Forms.Label();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.pnlBooking = new System.Windows.Forms.GroupBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.numSoNguoi = new System.Windows.Forms.NumericUpDown();
            this.lblSoNguoi = new System.Windows.Forms.Label();
            this.dtpNgayTra = new System.Windows.Forms.DateTimePicker();
            this.lblNgayTra = new System.Windows.Forms.Label();
            this.dtpNgayNhan = new System.Windows.Forms.DateTimePicker();
            this.lblNgayNhan = new System.Windows.Forms.Label();
            this.pnlBookingList = new System.Windows.Forms.GroupBox();
            this.dgvDatPhong = new System.Windows.Forms.DataGridView();
            this.pnlServices = new System.Windows.Forms.GroupBox();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMaDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageListPhong = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnlTotal.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.pnlSelectedRoom.SuspendLayout();
            this.pnlRoomList.SuspendLayout();
            this.pnlRoomFilter.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlCustomer.SuspendLayout();
            this.pnlBooking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNguoi)).BeginInit();
            this.pnlBookingList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatPhong)).BeginInit();
            this.pnlServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.pnlLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlRight, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1577, 780);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.tableLayoutPanel3);
            this.pnlLeft.Controls.Add(this.pnlRoomList);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(8, 8);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(5);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(2);
            this.pnlLeft.Size = new System.Drawing.Size(618, 764);
            this.pnlLeft.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.pnlTotal, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.pnlSelectedRoom, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 442);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(614, 320);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // pnlTotal
            // 
            this.pnlTotal.Controls.Add(this.tableLayoutPanel4);
            this.pnlTotal.Controls.Add(this.lblTongTienValue);
            this.pnlTotal.Controls.Add(this.lblTongTien);
            this.pnlTotal.Controls.Add(this.lblTienDichVuValue);
            this.pnlTotal.Controls.Add(this.lblTienDichVu);
            this.pnlTotal.Controls.Add(this.lblTienPhongValue);
            this.pnlTotal.Controls.Add(this.lblTienPhong);
            this.pnlTotal.Controls.Add(this.lblSoNgayValue);
            this.pnlTotal.Controls.Add(this.lblSoNgay);
            this.pnlTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotal.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTotal.Location = new System.Drawing.Point(8, 163);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTotal.Size = new System.Drawing.Size(598, 149);
            this.pnlTotal.TabIndex = 3;
            this.pnlTotal.TabStop = false;
            this.pnlTotal.Text = "Tổng chi phí";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.btnSuaDatPhong, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnXoaDatPhong, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnDatPhong, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnRefreshList, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(10, 97);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.tableLayoutPanel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(578, 42);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btnSuaDatPhong
            // 
            this.btnSuaDatPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnSuaDatPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSuaDatPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuaDatPhong.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaDatPhong.ForeColor = System.Drawing.Color.White;
            this.btnSuaDatPhong.Location = new System.Drawing.Point(298, 6);
            this.btnSuaDatPhong.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnSuaDatPhong.Name = "btnSuaDatPhong";
            this.btnSuaDatPhong.Size = new System.Drawing.Size(119, 30);
            this.btnSuaDatPhong.TabIndex = 0;
            this.btnSuaDatPhong.Text = "SỬA ĐẶT";
            this.btnSuaDatPhong.UseVisualStyleBackColor = false;
            this.btnSuaDatPhong.Click += new System.EventHandler(this.btnSuaDatPhong_Click);
            // 
            // btnXoaDatPhong
            // 
            this.btnXoaDatPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnXoaDatPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXoaDatPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDatPhong.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaDatPhong.ForeColor = System.Drawing.Color.White;
            this.btnXoaDatPhong.Location = new System.Drawing.Point(159, 6);
            this.btnXoaDatPhong.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnXoaDatPhong.Name = "btnXoaDatPhong";
            this.btnXoaDatPhong.Size = new System.Drawing.Size(119, 30);
            this.btnXoaDatPhong.TabIndex = 1;
            this.btnXoaDatPhong.Text = "HỦY ĐẶT";
            this.btnXoaDatPhong.UseVisualStyleBackColor = false;
            this.btnXoaDatPhong.Click += new System.EventHandler(this.btnXoaDatPhong_Click);
            // 
            // btnDatPhong
            // 
            this.btnDatPhong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnDatPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDatPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatPhong.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatPhong.ForeColor = System.Drawing.Color.White;
            this.btnDatPhong.Location = new System.Drawing.Point(20, 6);
            this.btnDatPhong.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnDatPhong.Name = "btnDatPhong";
            this.btnDatPhong.Size = new System.Drawing.Size(119, 30);
            this.btnDatPhong.TabIndex = 0;
            this.btnDatPhong.Text = "ĐẶT PHÒNG";
            this.btnDatPhong.UseVisualStyleBackColor = false;
            this.btnDatPhong.Click += new System.EventHandler(this.btnDatPhong_Click);
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnRefreshList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefreshList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshList.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshList.ForeColor = System.Drawing.Color.White;
            this.btnRefreshList.Location = new System.Drawing.Point(437, 6);
            this.btnRefreshList.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(121, 30);
            this.btnRefreshList.TabIndex = 2;
            this.btnRefreshList.Text = "LÀM MỚI";
            this.btnRefreshList.UseVisualStyleBackColor = false;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // lblTongTienValue
            // 
            this.lblTongTienValue.AutoSize = true;
            this.lblTongTienValue.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblTongTienValue.Location = new System.Drawing.Point(349, 61);
            this.lblTongTienValue.Name = "lblTongTienValue";
            this.lblTongTienValue.Size = new System.Drawing.Size(64, 23);
            this.lblTongTienValue.TabIndex = 7;
            this.lblTongTienValue.Text = "0 VNĐ";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.Location = new System.Drawing.Point(233, 65);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(95, 23);
            this.lblTongTien.TabIndex = 6;
            this.lblTongTien.Text = "Tổng tiền:";
            // 
            // lblTienDichVuValue
            // 
            this.lblTienDichVuValue.AutoSize = true;
            this.lblTienDichVuValue.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienDichVuValue.Location = new System.Drawing.Point(350, 34);
            this.lblTienDichVuValue.Name = "lblTienDichVuValue";
            this.lblTienDichVuValue.Size = new System.Drawing.Size(49, 19);
            this.lblTienDichVuValue.TabIndex = 5;
            this.lblTienDichVuValue.Text = "0 VNĐ";
            // 
            // lblTienDichVu
            // 
            this.lblTienDichVu.AutoSize = true;
            this.lblTienDichVu.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienDichVu.Location = new System.Drawing.Point(233, 34);
            this.lblTienDichVu.Name = "lblTienDichVu";
            this.lblTienDichVu.Size = new System.Drawing.Size(91, 19);
            this.lblTienDichVu.TabIndex = 4;
            this.lblTienDichVu.Text = "Tiền dịch vụ:";
            // 
            // lblTienPhongValue
            // 
            this.lblTienPhongValue.AutoSize = true;
            this.lblTienPhongValue.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienPhongValue.Location = new System.Drawing.Point(138, 68);
            this.lblTienPhongValue.Name = "lblTienPhongValue";
            this.lblTienPhongValue.Size = new System.Drawing.Size(49, 19);
            this.lblTienPhongValue.TabIndex = 3;
            this.lblTienPhongValue.Text = "0 VNĐ";
            // 
            // lblTienPhong
            // 
            this.lblTienPhong.AutoSize = true;
            this.lblTienPhong.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTienPhong.Location = new System.Drawing.Point(21, 67);
            this.lblTienPhong.Name = "lblTienPhong";
            this.lblTienPhong.Size = new System.Drawing.Size(85, 19);
            this.lblTienPhong.TabIndex = 2;
            this.lblTienPhong.Text = "Tiền phòng:";
            // 
            // lblSoNgayValue
            // 
            this.lblSoNgayValue.AutoSize = true;
            this.lblSoNgayValue.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoNgayValue.Location = new System.Drawing.Point(137, 34);
            this.lblSoNgayValue.Name = "lblSoNgayValue";
            this.lblSoNgayValue.Size = new System.Drawing.Size(52, 19);
            this.lblSoNgayValue.TabIndex = 1;
            this.lblSoNgayValue.Text = "0 ngày";
            // 
            // lblSoNgay
            // 
            this.lblSoNgay.AutoSize = true;
            this.lblSoNgay.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoNgay.Location = new System.Drawing.Point(19, 34);
            this.lblSoNgay.Name = "lblSoNgay";
            this.lblSoNgay.Size = new System.Drawing.Size(118, 19);
            this.lblSoNgay.TabIndex = 0;
            this.lblSoNgay.Text = "Số ngày ở (đêm):";
            // 
            // pnlSelectedRoom
            // 
            this.pnlSelectedRoom.Controls.Add(this.lblSelectedRoomInfo);
            this.pnlSelectedRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSelectedRoom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlSelectedRoom.Location = new System.Drawing.Point(8, 8);
            this.pnlSelectedRoom.Name = "pnlSelectedRoom";
            this.pnlSelectedRoom.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSelectedRoom.Size = new System.Drawing.Size(598, 149);
            this.pnlSelectedRoom.TabIndex = 1;
            this.pnlSelectedRoom.TabStop = false;
            this.pnlSelectedRoom.Text = "Thông tin phòng đã chọn";
            // 
            // lblSelectedRoomInfo
            // 
            this.lblSelectedRoomInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectedRoomInfo.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedRoomInfo.Location = new System.Drawing.Point(10, 28);
            this.lblSelectedRoomInfo.Name = "lblSelectedRoomInfo";
            this.lblSelectedRoomInfo.Size = new System.Drawing.Size(578, 111);
            this.lblSelectedRoomInfo.TabIndex = 0;
            this.lblSelectedRoomInfo.Text = "Chưa chọn phòng nào.\r\n\r\nVui lòng chọn phòng từ danh sách bên trên.";
            // 
            // pnlRoomList
            // 
            this.pnlRoomList.Controls.Add(this.lvPhong);
            this.pnlRoomList.Controls.Add(this.pnlRoomFilter);
            this.pnlRoomList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRoomList.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRoomList.Location = new System.Drawing.Point(2, 2);
            this.pnlRoomList.Name = "pnlRoomList";
            this.pnlRoomList.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRoomList.Size = new System.Drawing.Size(614, 434);
            this.pnlRoomList.TabIndex = 0;
            this.pnlRoomList.TabStop = false;
            this.pnlRoomList.Text = "Danh sách phòng";
            // 
            // lvPhong
            // 
            this.lvPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPhong.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPhong.FullRowSelect = true;
            this.lvPhong.GridLines = true;
            this.lvPhong.HideSelection = false;
            this.lvPhong.Location = new System.Drawing.Point(10, 68);
            this.lvPhong.MultiSelect = false;
            this.lvPhong.Name = "lvPhong";
            this.lvPhong.Size = new System.Drawing.Size(594, 356);
            this.lvPhong.TabIndex = 1;
            this.lvPhong.UseCompatibleStateImageBehavior = false;
            this.lvPhong.View = System.Windows.Forms.View.Details;
            this.lvPhong.SelectedIndexChanged += new System.EventHandler(this.lvPhong_SelectedIndexChanged);
            // 
            // pnlRoomFilter
            // 
            this.pnlRoomFilter.Controls.Add(this.rbBaoTri);
            this.pnlRoomFilter.Controls.Add(this.rbCoKhach);
            this.pnlRoomFilter.Controls.Add(this.rbTrong);
            this.pnlRoomFilter.Controls.Add(this.rbTatCa);
            this.pnlRoomFilter.Controls.Add(this.lblLoaiPhong);
            this.pnlRoomFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRoomFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pnlRoomFilter.Location = new System.Drawing.Point(10, 30);
            this.pnlRoomFilter.Name = "pnlRoomFilter";
            this.pnlRoomFilter.Size = new System.Drawing.Size(594, 38);
            this.pnlRoomFilter.TabIndex = 0;
            // 
            // rbBaoTri
            // 
            this.rbBaoTri.AutoSize = true;
            this.rbBaoTri.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBaoTri.Location = new System.Drawing.Point(346, 6);
            this.rbBaoTri.Name = "rbBaoTri";
            this.rbBaoTri.Size = new System.Drawing.Size(70, 23);
            this.rbBaoTri.TabIndex = 6;
            this.rbBaoTri.Text = "Bảo trì";
            this.rbBaoTri.UseVisualStyleBackColor = true;
            this.rbBaoTri.CheckedChanged += new System.EventHandler(this.rbBaoTri_CheckedChanged);
            // 
            // rbCoKhach
            // 
            this.rbCoKhach.AutoSize = true;
            this.rbCoKhach.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCoKhach.Location = new System.Drawing.Point(240, 6);
            this.rbCoKhach.Name = "rbCoKhach";
            this.rbCoKhach.Size = new System.Drawing.Size(86, 23);
            this.rbCoKhach.TabIndex = 4;
            this.rbCoKhach.Text = "Có khách";
            this.rbCoKhach.UseVisualStyleBackColor = true;
            this.rbCoKhach.CheckedChanged += new System.EventHandler(this.rbCoKhach_CheckedChanged);
            // 
            // rbTrong
            // 
            this.rbTrong.AutoSize = true;
            this.rbTrong.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTrong.Location = new System.Drawing.Point(111, 6);
            this.rbTrong.Name = "rbTrong";
            this.rbTrong.Size = new System.Drawing.Size(106, 23);
            this.rbTrong.TabIndex = 3;
            this.rbTrong.Text = "Phòng trống";
            this.rbTrong.UseVisualStyleBackColor = true;
            this.rbTrong.CheckedChanged += new System.EventHandler(this.rbTrong_CheckedChanged);
            // 
            // rbTatCa
            // 
            this.rbTatCa.AutoSize = true;
            this.rbTatCa.Checked = true;
            this.rbTatCa.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTatCa.Location = new System.Drawing.Point(14, 6);
            this.rbTatCa.Name = "rbTatCa";
            this.rbTatCa.Size = new System.Drawing.Size(66, 23);
            this.rbTatCa.TabIndex = 2;
            this.rbTatCa.TabStop = true;
            this.rbTatCa.Text = "Tất cả";
            this.rbTatCa.UseVisualStyleBackColor = true;
            this.rbTatCa.CheckedChanged += new System.EventHandler(this.rbTatCa_CheckedChanged);
            // 
            // lblLoaiPhong
            // 
            this.lblLoaiPhong.AutoSize = true;
            this.lblLoaiPhong.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiPhong.Location = new System.Drawing.Point(12, 15);
            this.lblLoaiPhong.Name = "lblLoaiPhong";
            this.lblLoaiPhong.Size = new System.Drawing.Size(0, 19);
            this.lblLoaiPhong.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.tableLayoutPanel2);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(636, 8);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(5);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(933, 764);
            this.pnlRight.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pnlCustomer, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlBooking, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pnlBookingList, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.pnlServices, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(933, 764);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.Controls.Add(this.cboKhachHang);
            this.pnlCustomer.Controls.Add(this.lblChonKH);
            this.pnlCustomer.Controls.Add(this.btnAddCustomer);
            this.pnlCustomer.Controls.Add(this.txtCCCD);
            this.pnlCustomer.Controls.Add(this.txtEmail);
            this.pnlCustomer.Controls.Add(this.txtSoDienThoai);
            this.pnlCustomer.Controls.Add(this.txtHoTen);
            this.pnlCustomer.Controls.Add(this.lblCCCD);
            this.pnlCustomer.Controls.Add(this.lblEmail);
            this.pnlCustomer.Controls.Add(this.lblSoDienThoai);
            this.pnlCustomer.Controls.Add(this.lblHoTen);
            this.pnlCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomer.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCustomer.Location = new System.Drawing.Point(8, 8);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCustomer.Size = new System.Drawing.Size(917, 159);
            this.pnlCustomer.TabIndex = 0;
            this.pnlCustomer.TabStop = false;
            this.pnlCustomer.Text = "Thông tin khách hàng";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(151, 35);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(378, 27);
            this.cboKhachHang.TabIndex = 11;
            this.cboKhachHang.SelectedIndexChanged += new System.EventHandler(this.cboKhachHang_SelectedIndexChanged);
            // 
            // lblChonKH
            // 
            this.lblChonKH.AutoSize = true;
            this.lblChonKH.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChonKH.Location = new System.Drawing.Point(20, 38);
            this.lblChonKH.Name = "lblChonKH";
            this.lblChonKH.Size = new System.Drawing.Size(135, 19);
            this.lblChonKH.TabIndex = 10;
            this.lblChonKH.Text = "Chọn khách hàng: *";
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomer.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
            this.btnAddCustomer.Location = new System.Drawing.Point(595, 30);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(120, 30);
            this.btnAddCustomer.TabIndex = 9;
            this.btnAddCustomer.Text = "Thêm KH mới";
            this.btnAddCustomer.UseVisualStyleBackColor = false;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // txtCCCD
            // 
            this.txtCCCD.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCCD.Location = new System.Drawing.Point(389, 104);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.ReadOnly = true;
            this.txtCCCD.Size = new System.Drawing.Size(140, 27);
            this.txtCCCD.TabIndex = 8;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(389, 69);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(140, 27);
            this.txtEmail.TabIndex = 7;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoDienThoai.Location = new System.Drawing.Point(151, 104);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.ReadOnly = true;
            this.txtSoDienThoai.Size = new System.Drawing.Size(140, 27);
            this.txtSoDienThoai.TabIndex = 6;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHoTen.Location = new System.Drawing.Point(151, 68);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.ReadOnly = true;
            this.txtHoTen.Size = new System.Drawing.Size(140, 27);
            this.txtHoTen.TabIndex = 5;
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCCD.Location = new System.Drawing.Point(318, 107);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(49, 19);
            this.lblCCCD.TabIndex = 4;
            this.lblCCCD.Text = "CCCD:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(318, 72);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(49, 19);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email:";
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoDienThoai.Location = new System.Drawing.Point(20, 107);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(98, 19);
            this.lblSoDienThoai.TabIndex = 2;
            this.lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoTen.Location = new System.Drawing.Point(20, 71);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(56, 19);
            this.lblHoTen.TabIndex = 0;
            this.lblHoTen.Text = "Họ tên:";
            // 
            // pnlBooking
            // 
            this.pnlBooking.Controls.Add(this.lblGhiChu);
            this.pnlBooking.Controls.Add(this.txtGhiChu);
            this.pnlBooking.Controls.Add(this.cboTrangThai);
            this.pnlBooking.Controls.Add(this.lblTrangThai);
            this.pnlBooking.Controls.Add(this.numSoNguoi);
            this.pnlBooking.Controls.Add(this.lblSoNguoi);
            this.pnlBooking.Controls.Add(this.dtpNgayTra);
            this.pnlBooking.Controls.Add(this.lblNgayTra);
            this.pnlBooking.Controls.Add(this.dtpNgayNhan);
            this.pnlBooking.Controls.Add(this.lblNgayNhan);
            this.pnlBooking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBooking.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBooking.Location = new System.Drawing.Point(10, 175);
            this.pnlBooking.Margin = new System.Windows.Forms.Padding(5);
            this.pnlBooking.Name = "pnlBooking";
            this.pnlBooking.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBooking.Size = new System.Drawing.Size(913, 186);
            this.pnlBooking.TabIndex = 1;
            this.pnlBooking.TabStop = false;
            this.pnlBooking.Text = "Thông tin đặt phòng";
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.Location = new System.Drawing.Point(18, 97);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(61, 19);
            this.lblGhiChu.TabIndex = 17;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGhiChu.Location = new System.Drawing.Point(156, 97);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(557, 58);
            this.txtGhiChu.TabIndex = 18;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Chờ xác nhận",
            "Đã xác nhận",
            "Đã nhận phòng",
            "Đã trả phòng",
            "Đã hủy"});
            this.cboTrangThai.Location = new System.Drawing.Point(554, 68);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(159, 27);
            this.cboTrangThai.TabIndex = 20;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.Location = new System.Drawing.Point(427, 74);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(78, 19);
            this.lblTrangThai.TabIndex = 19;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // numSoNguoi
            // 
            this.numSoNguoi.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSoNguoi.Location = new System.Drawing.Point(156, 68);
            this.numSoNguoi.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSoNguoi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoNguoi.Name = "numSoNguoi";
            this.numSoNguoi.Size = new System.Drawing.Size(149, 27);
            this.numSoNguoi.TabIndex = 16;
            this.numSoNguoi.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSoNguoi
            // 
            this.lblSoNguoi.AutoSize = true;
            this.lblSoNguoi.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoNguoi.Location = new System.Drawing.Point(18, 74);
            this.lblSoNguoi.Name = "lblSoNguoi";
            this.lblSoNguoi.Size = new System.Drawing.Size(81, 19);
            this.lblSoNguoi.TabIndex = 15;
            this.lblSoNguoi.Text = "Số người: *";
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTra.Location = new System.Drawing.Point(556, 36);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(159, 27);
            this.dtpNgayTra.TabIndex = 14;
            this.dtpNgayTra.ValueChanged += new System.EventHandler(this.dtpNgayTra_ValueChanged);
            // 
            // lblNgayTra
            // 
            this.lblNgayTra.AutoSize = true;
            this.lblNgayTra.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayTra.Location = new System.Drawing.Point(427, 40);
            this.lblNgayTra.Name = "lblNgayTra";
            this.lblNgayTra.Size = new System.Drawing.Size(123, 19);
            this.lblNgayTra.TabIndex = 13;
            this.lblNgayTra.Text = "Ngày trả phòng: *";
            // 
            // dtpNgayNhan
            // 
            this.dtpNgayNhan.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayNhan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayNhan.Location = new System.Drawing.Point(156, 36);
            this.dtpNgayNhan.Name = "dtpNgayNhan";
            this.dtpNgayNhan.Size = new System.Drawing.Size(149, 27);
            this.dtpNgayNhan.TabIndex = 12;
            this.dtpNgayNhan.ValueChanged += new System.EventHandler(this.dtpNgayNhan_ValueChanged);
            // 
            // lblNgayNhan
            // 
            this.lblNgayNhan.AutoSize = true;
            this.lblNgayNhan.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayNhan.Location = new System.Drawing.Point(18, 40);
            this.lblNgayNhan.Name = "lblNgayNhan";
            this.lblNgayNhan.Size = new System.Drawing.Size(137, 19);
            this.lblNgayNhan.TabIndex = 11;
            this.lblNgayNhan.Text = "Ngày nhận phòng: *";
            // 
            // pnlBookingList
            // 
            this.pnlBookingList.Controls.Add(this.dgvDatPhong);
            this.pnlBookingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBookingList.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBookingList.Location = new System.Drawing.Point(8, 549);
            this.pnlBookingList.Name = "pnlBookingList";
            this.pnlBookingList.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBookingList.Size = new System.Drawing.Size(917, 207);
            this.pnlBookingList.TabIndex = 4;
            this.pnlBookingList.TabStop = false;
            this.pnlBookingList.Text = "Danh sách đặt phòng";
            // 
            // dgvDatPhong
            // 
            this.dgvDatPhong.AllowUserToAddRows = false;
            this.dgvDatPhong.AllowUserToDeleteRows = false;
            this.dgvDatPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvDatPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDatPhong.Location = new System.Drawing.Point(10, 30);
            this.dgvDatPhong.MultiSelect = false;
            this.dgvDatPhong.Name = "dgvDatPhong";
            this.dgvDatPhong.ReadOnly = true;
            this.dgvDatPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatPhong.Size = new System.Drawing.Size(897, 167);
            this.dgvDatPhong.TabIndex = 1;
            this.dgvDatPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatPhong_CellClick);
            // 
            // pnlServices
            // 
            this.pnlServices.Controls.Add(this.dgvDichVu);
            this.pnlServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlServices.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlServices.Location = new System.Drawing.Point(8, 369);
            this.pnlServices.Name = "pnlServices";
            this.pnlServices.Padding = new System.Windows.Forms.Padding(10);
            this.pnlServices.Size = new System.Drawing.Size(917, 174);
            this.pnlServices.TabIndex = 2;
            this.pnlServices.TabStop = false;
            this.pnlServices.Text = "Dịch vụ sử dụng (Chọn dịch vụ từ danh sách)";
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDichVu.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChon,
            this.colMaDV,
            this.colTenDV,
            this.colDonGia,
            this.colSoLuong,
            this.colThanhTien});
            this.dgvDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDichVu.Location = new System.Drawing.Point(10, 30);
            this.dgvDichVu.Margin = new System.Windows.Forms.Padding(5);
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(897, 134);
            this.dgvDichVu.TabIndex = 0;
            this.dgvDichVu.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDichVu_CellValueChanged);
            // 
            // colChon
            // 
            this.colChon.FillWeight = 30F;
            this.colChon.HeaderText = "Chọn";
            this.colChon.Name = "colChon";
            // 
            // colMaDV
            // 
            this.colMaDV.FillWeight = 40F;
            this.colMaDV.HeaderText = "Mã DV";
            this.colMaDV.Name = "colMaDV";
            this.colMaDV.ReadOnly = true;
            // 
            // colTenDV
            // 
            this.colTenDV.FillWeight = 80F;
            this.colTenDV.HeaderText = "Tên dịch vụ";
            this.colTenDV.Name = "colTenDV";
            this.colTenDV.ReadOnly = true;
            // 
            // colDonGia
            // 
            this.colDonGia.FillWeight = 50F;
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.FillWeight = 40F;
            this.colSoLuong.HeaderText = "SL";
            this.colSoLuong.Name = "colSoLuong";
            // 
            // colThanhTien
            // 
            this.colThanhTien.FillWeight = 60F;
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            // 
            // imageListPhong
            // 
            this.imageListPhong.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListPhong.ImageSize = new System.Drawing.Size(30, 30);
            this.imageListPhong.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // BookingRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1577, 780);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BookingRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Đặt Phòng - Hotel Management System";
            this.Load += new System.EventHandler(this.BookingRoom_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.pnlSelectedRoom.ResumeLayout(false);
            this.pnlRoomList.ResumeLayout(false);
            this.pnlRoomFilter.ResumeLayout(false);
            this.pnlRoomFilter.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.pnlBooking.ResumeLayout(false);
            this.pnlBooking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNguoi)).EndInit();
            this.pnlBookingList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatPhong)).EndInit();
            this.pnlServices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox pnlRoomList;
        private System.Windows.Forms.ListView lvPhong;
        private System.Windows.Forms.Panel pnlRoomFilter;
        private System.Windows.Forms.RadioButton rbBaoTri;
        private System.Windows.Forms.RadioButton rbCoKhach;
        private System.Windows.Forms.RadioButton rbTrong;
        private System.Windows.Forms.RadioButton rbTatCa;
        private System.Windows.Forms.Label lblLoaiPhong;
        private System.Windows.Forms.GroupBox pnlSelectedRoom;
        private System.Windows.Forms.Label lblSelectedRoomInfo;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox pnlCustomer;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblChonKH;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.GroupBox pnlServices;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.GroupBox pnlTotal;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblTienDichVu;
        private System.Windows.Forms.Label lblTienPhong;
        private System.Windows.Forms.Label lblSoNgay;
        private System.Windows.Forms.Button btnDatPhong;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ImageList imageListPhong;
        private System.Windows.Forms.Label lblTongTienValue;
        private System.Windows.Forms.Label lblTienDichVuValue;
        private System.Windows.Forms.Label lblTienPhongValue;
        private System.Windows.Forms.Label lblSoNgayValue;
        private System.Windows.Forms.GroupBox pnlBookingList;
        private System.Windows.Forms.DataGridView dgvDatPhong;
        private System.Windows.Forms.Button btnSuaDatPhong;
        private System.Windows.Forms.Button btnXoaDatPhong;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox pnlBooking;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.NumericUpDown numSoNguoi;
        private System.Windows.Forms.Label lblSoNguoi;
        private System.Windows.Forms.DateTimePicker dtpNgayTra;
        private System.Windows.Forms.Label lblNgayTra;
        private System.Windows.Forms.DateTimePicker dtpNgayNhan;
        private System.Windows.Forms.Label lblNgayNhan;
    }
}