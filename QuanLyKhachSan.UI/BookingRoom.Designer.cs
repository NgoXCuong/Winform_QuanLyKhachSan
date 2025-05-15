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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookingRoom));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lvPhong = new System.Windows.Forms.ListView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbBaoTri = new System.Windows.Forms.RadioButton();
            this.rbDangO = new System.Windows.Forms.RadioButton();
            this.rbDaDat = new System.Windows.Forms.RadioButton();
            this.rbTrong = new System.Windows.Forms.RadioButton();
            this.rbTatCa = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTongTienAll = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.txtTenDatPhong = new System.Windows.Forms.TextBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.cbDichVu = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.dtNgayTra = new System.Windows.Forms.DateTimePicker();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.dtNgayNhan = new System.Windows.Forms.DateTimePicker();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dtNgayDat = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnHuyDat = new System.Windows.Forms.Button();
            this.btnDatPhong = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvListDatPhong = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvChonDichVu = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lbTienDV = new System.Windows.Forms.Label();
            this.btnHuyDV = new System.Windows.Forms.Button();
            this.btnThemDV = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDatPhong)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChonDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 611);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(954, 549);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tiền DV:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 599);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách phòng";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.lvPhong);
            this.panel6.Location = new System.Drawing.Point(7, 87);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(310, 354);
            this.panel6.TabIndex = 1;
            // 
            // lvPhong
            // 
            this.lvPhong.HideSelection = false;
            this.lvPhong.Location = new System.Drawing.Point(4, 10);
            this.lvPhong.Name = "lvPhong";
            this.lvPhong.Size = new System.Drawing.Size(300, 332);
            this.lvPhong.TabIndex = 0;
            this.lvPhong.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbBaoTri);
            this.groupBox6.Controls.Add(this.rbDangO);
            this.groupBox6.Controls.Add(this.rbDaDat);
            this.groupBox6.Controls.Add(this.rbTrong);
            this.groupBox6.Controls.Add(this.rbTatCa);
            this.groupBox6.Location = new System.Drawing.Point(7, 22);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(310, 59);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            // 
            // rbBaoTri
            // 
            this.rbBaoTri.AutoSize = true;
            this.rbBaoTri.Location = new System.Drawing.Point(226, 37);
            this.rbBaoTri.Name = "rbBaoTri";
            this.rbBaoTri.Size = new System.Drawing.Size(63, 20);
            this.rbBaoTri.TabIndex = 4;
            this.rbBaoTri.TabStop = true;
            this.rbBaoTri.Text = "Bảo trì";
            this.rbBaoTri.UseVisualStyleBackColor = true;
            this.rbBaoTri.CheckedChanged += new System.EventHandler(this.rbBaoTri_CheckedChanged);
            // 
            // rbDangO
            // 
            this.rbDangO.AutoSize = true;
            this.rbDangO.Location = new System.Drawing.Point(226, 11);
            this.rbDangO.Name = "rbDangO";
            this.rbDangO.Size = new System.Drawing.Size(68, 20);
            this.rbDangO.TabIndex = 3;
            this.rbDangO.TabStop = true;
            this.rbDangO.Text = "Đang ở";
            this.rbDangO.UseVisualStyleBackColor = true;
            this.rbDangO.CheckedChanged += new System.EventHandler(this.rbDangO_CheckedChanged);
            // 
            // rbDaDat
            // 
            this.rbDaDat.AutoSize = true;
            this.rbDaDat.Location = new System.Drawing.Point(6, 37);
            this.rbDaDat.Name = "rbDaDat";
            this.rbDaDat.Size = new System.Drawing.Size(105, 20);
            this.rbDaDat.TabIndex = 2;
            this.rbDaDat.Text = "Phòng đã đặt";
            this.rbDaDat.UseVisualStyleBackColor = true;
            this.rbDaDat.CheckedChanged += new System.EventHandler(this.rbDaDat_CheckedChanged);
            // 
            // rbTrong
            // 
            this.rbTrong.AutoSize = true;
            this.rbTrong.Location = new System.Drawing.Point(103, 11);
            this.rbTrong.Name = "rbTrong";
            this.rbTrong.Size = new System.Drawing.Size(97, 20);
            this.rbTrong.TabIndex = 1;
            this.rbTrong.Text = "Phòng trống";
            this.rbTrong.UseVisualStyleBackColor = true;
            this.rbTrong.CheckedChanged += new System.EventHandler(this.rbTrong_CheckedChanged);
            // 
            // rbTatCa
            // 
            this.rbTatCa.AutoSize = true;
            this.rbTatCa.Checked = true;
            this.rbTatCa.Location = new System.Drawing.Point(6, 11);
            this.rbTatCa.Name = "rbTatCa";
            this.rbTatCa.Size = new System.Drawing.Size(63, 20);
            this.rbTatCa.TabIndex = 0;
            this.rbTatCa.TabStop = true;
            this.rbTatCa.Text = "Tất cả";
            this.rbTatCa.UseVisualStyleBackColor = true;
            this.rbTatCa.CheckedChanged += new System.EventHandler(this.rbTatCa_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(957, 581);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tổng tiền:";
            // 
            // lbTongTienAll
            // 
            this.lbTongTienAll.AutoSize = true;
            this.lbTongTienAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTongTienAll.ForeColor = System.Drawing.Color.Red;
            this.lbTongTienAll.Location = new System.Drawing.Point(1046, 581);
            this.lbTongTienAll.Name = "lbTongTienAll";
            this.lbTongTienAll.Size = new System.Drawing.Size(46, 18);
            this.lbTongTienAll.TabIndex = 4;
            this.lbTongTienAll.Text = "label2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Location = new System.Drawing.Point(348, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(766, 247);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox14);
            this.groupBox2.Controls.Add(this.groupBox13);
            this.groupBox2.Controls.Add(this.groupBox12);
            this.groupBox2.Controls.Add(this.groupBox11);
            this.groupBox2.Controls.Add(this.groupBox10);
            this.groupBox2.Controls.Add(this.groupBox9);
            this.groupBox2.Controls.Add(this.groupBox8);
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Location = new System.Drawing.Point(16, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(738, 221);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin đặt phòng";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.txtMaNV);
            this.groupBox14.Location = new System.Drawing.Point(593, 40);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(139, 50);
            this.groupBox14.TabIndex = 8;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Mã nhân viên";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(7, 21);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(119, 22);
            this.txtMaNV.TabIndex = 0;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.txtMaKH);
            this.groupBox13.Location = new System.Drawing.Point(353, 40);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(220, 50);
            this.groupBox13.TabIndex = 7;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Mã khách hàng";
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(7, 21);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(203, 22);
            this.txtMaKH.TabIndex = 0;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.txtTenDatPhong);
            this.groupBox12.Location = new System.Drawing.Point(353, 96);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(220, 50);
            this.groupBox12.TabIndex = 6;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Phòng đặt";
            // 
            // txtTenDatPhong
            // 
            this.txtTenDatPhong.Location = new System.Drawing.Point(7, 21);
            this.txtTenDatPhong.Name = "txtTenDatPhong";
            this.txtTenDatPhong.Size = new System.Drawing.Size(203, 22);
            this.txtTenDatPhong.TabIndex = 0;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.txtSoLuong);
            this.groupBox11.Location = new System.Drawing.Point(593, 160);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(132, 54);
            this.groupBox11.TabIndex = 1;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Số lượng";
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(7, 22);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(119, 22);
            this.txtSoLuong.TabIndex = 0;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.cbDichVu);
            this.groupBox10.Location = new System.Drawing.Point(353, 160);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(220, 50);
            this.groupBox10.TabIndex = 5;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Dịch vụ sử dụng";
            // 
            // cbDichVu
            // 
            this.cbDichVu.FormattingEnabled = true;
            this.cbDichVu.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbDichVu.Location = new System.Drawing.Point(6, 22);
            this.cbDichVu.Name = "cbDichVu";
            this.cbDichVu.Size = new System.Drawing.Size(204, 24);
            this.cbDichVu.TabIndex = 7;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.dtNgayTra);
            this.groupBox9.Location = new System.Drawing.Point(46, 162);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(265, 50);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Ngày trả";
            // 
            // dtNgayTra
            // 
            this.dtNgayTra.Location = new System.Drawing.Point(7, 22);
            this.dtNgayTra.Name = "dtNgayTra";
            this.dtNgayTra.Size = new System.Drawing.Size(252, 22);
            this.dtNgayTra.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.dtNgayNhan);
            this.groupBox8.Location = new System.Drawing.Point(46, 96);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(265, 50);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Ngày nhận";
            // 
            // dtNgayNhan
            // 
            this.dtNgayNhan.Location = new System.Drawing.Point(7, 27);
            this.dtNgayNhan.Name = "dtNgayNhan";
            this.dtNgayNhan.Size = new System.Drawing.Size(252, 22);
            this.dtNgayNhan.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dtNgayDat);
            this.groupBox7.Location = new System.Drawing.Point(46, 32);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(265, 50);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Ngày đặt";
            // 
            // dtNgayDat
            // 
            this.dtNgayDat.Location = new System.Drawing.Point(7, 21);
            this.dtNgayDat.Name = "dtNgayDat";
            this.dtNgayDat.Size = new System.Drawing.Size(252, 22);
            this.dtNgayDat.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Location = new System.Drawing.Point(7, 447);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(310, 146);
            this.panel3.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTim);
            this.groupBox3.Controls.Add(this.btnTim);
            this.groupBox3.Controls.Add(this.btnHuyDat);
            this.groupBox3.Controls.Add(this.btnDatPhong);
            this.groupBox3.Location = new System.Drawing.Point(4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 137);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chức năng";
            // 
            // txtTim
            // 
            this.txtTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTim.Location = new System.Drawing.Point(36, 25);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(122, 26);
            this.txtTim.TabIndex = 3;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(180, 24);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 30);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            // 
            // btnHuyDat
            // 
            this.btnHuyDat.Location = new System.Drawing.Point(53, 87);
            this.btnHuyDat.Name = "btnHuyDat";
            this.btnHuyDat.Size = new System.Drawing.Size(80, 30);
            this.btnHuyDat.TabIndex = 1;
            this.btnHuyDat.Text = "Hủy đặt";
            this.btnHuyDat.UseVisualStyleBackColor = true;
            this.btnHuyDat.Click += new System.EventHandler(this.btnHuyDat_Click);
            // 
            // btnDatPhong
            // 
            this.btnDatPhong.Location = new System.Drawing.Point(180, 87);
            this.btnDatPhong.Name = "btnDatPhong";
            this.btnDatPhong.Size = new System.Drawing.Size(80, 30);
            this.btnDatPhong.TabIndex = 0;
            this.btnDatPhong.Text = "Đặt phòng";
            this.btnDatPhong.UseVisualStyleBackColor = true;
            this.btnDatPhong.Click += new System.EventHandler(this.btnDatPhong_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox4);
            this.panel4.Location = new System.Drawing.Point(348, 459);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(603, 152);
            this.panel4.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvListDatPhong);
            this.groupBox4.Location = new System.Drawing.Point(16, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(573, 146);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh sách đặt phòng";
            // 
            // dgvListDatPhong
            // 
            this.dgvListDatPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListDatPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListDatPhong.Location = new System.Drawing.Point(7, 21);
            this.dgvListDatPhong.Name = "dgvListDatPhong";
            this.dgvListDatPhong.Size = new System.Drawing.Size(556, 116);
            this.dgvListDatPhong.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox5);
            this.panel5.Location = new System.Drawing.Point(348, 253);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(754, 200);
            this.panel5.TabIndex = 4;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvChonDichVu);
            this.groupBox5.Location = new System.Drawing.Point(16, 9);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(725, 179);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Danh sách dịch vụ đã chọn";
            // 
            // dgvChonDichVu
            // 
            this.dgvChonDichVu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChonDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChonDichVu.Location = new System.Drawing.Point(7, 21);
            this.dgvChonDichVu.Name = "dgvChonDichVu";
            this.dgvChonDichVu.Size = new System.Drawing.Size(712, 147);
            this.dgvChonDichVu.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "phongtrong.png");
            this.imageList1.Images.SetKeyName(1, "phongdadat.png");
            this.imageList1.Images.SetKeyName(2, "phongdango.png");
            this.imageList1.Images.SetKeyName(3, "phongbaotri.png");
            // 
            // lbTienDV
            // 
            this.lbTienDV.AutoSize = true;
            this.lbTienDV.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTienDV.ForeColor = System.Drawing.Color.Red;
            this.lbTienDV.Location = new System.Drawing.Point(1032, 549);
            this.lbTienDV.Name = "lbTienDV";
            this.lbTienDV.Size = new System.Drawing.Size(46, 18);
            this.lbTienDV.TabIndex = 5;
            this.lbTienDV.Text = "label4";
            // 
            // btnHuyDV
            // 
            this.btnHuyDV.Location = new System.Drawing.Point(998, 504);
            this.btnHuyDV.Name = "btnHuyDV";
            this.btnHuyDV.Size = new System.Drawing.Size(80, 30);
            this.btnHuyDV.TabIndex = 4;
            this.btnHuyDV.Text = "Hủy DV";
            this.btnHuyDV.UseVisualStyleBackColor = true;
            this.btnHuyDV.Click += new System.EventHandler(this.btnHuyDV_Click);
            // 
            // btnThemDV
            // 
            this.btnThemDV.Location = new System.Drawing.Point(998, 468);
            this.btnThemDV.Name = "btnThemDV";
            this.btnThemDV.Size = new System.Drawing.Size(80, 30);
            this.btnThemDV.TabIndex = 6;
            this.btnThemDV.Text = "Thêm DV";
            this.btnThemDV.UseVisualStyleBackColor = true;
            this.btnThemDV.Click += new System.EventHandler(this.btnThemDV_Click);
            // 
            // BookingRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 611);
            this.Controls.Add(this.btnThemDV);
            this.Controls.Add(this.btnHuyDV);
            this.Controls.Add(this.lbTienDV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbTongTienAll);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BookingRoom";
            this.Text = "BookingRoom";
            this.Load += new System.EventHandler(this.BookingRoom_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDatPhong)).EndInit();
            this.panel5.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChonDichVu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbDaDat;
        private System.Windows.Forms.RadioButton rbTrong;
        private System.Windows.Forms.RadioButton rbTatCa;
        private System.Windows.Forms.ListView lvPhong;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label lbTongTienAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView dgvChonDichVu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.DateTimePicker dtNgayTra;
        private System.Windows.Forms.DateTimePicker dtNgayNhan;
        private System.Windows.Forms.DateTimePicker dtNgayDat;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnHuyDat;
        private System.Windows.Forms.Button btnDatPhong;
        private System.Windows.Forms.DataGridView dgvListDatPhong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.RadioButton rbBaoTri;
        private System.Windows.Forms.RadioButton rbDangO;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TextBox txtTenDatPhong;
        private System.Windows.Forms.ComboBox cbDichVu;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.Label lbTienDV;
        private System.Windows.Forms.Button btnHuyDV;
        private System.Windows.Forms.Button btnThemDV;
    }
}