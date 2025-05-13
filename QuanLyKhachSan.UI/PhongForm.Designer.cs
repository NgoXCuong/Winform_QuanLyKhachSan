namespace QuanLyKhachSan.UI
{
    partial class PhongForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPhong = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvListPhong = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbTrangThai = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbLoaiPhong = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtSoPhong = new System.Windows.Forms.TextBox();
            this.tabLoaiPhong = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.dgvListLoaiPhong = new System.Windows.Forms.DataGridView();
            this.txtTenLoaiPhong = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.txtGiaPhong = new System.Windows.Forms.TextBox();
            this.txtSoNguoi = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPhong.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPhong)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabLoaiPhong.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLoaiPhong)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPhong);
            this.tabControl1.Controls.Add(this.tabLoaiPhong);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1112, 606);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPhong
            // 
            this.tabPhong.Controls.Add(this.panel4);
            this.tabPhong.Controls.Add(this.panel1);
            this.tabPhong.Location = new System.Drawing.Point(4, 25);
            this.tabPhong.Name = "tabPhong";
            this.tabPhong.Padding = new System.Windows.Forms.Padding(3);
            this.tabPhong.Size = new System.Drawing.Size(1104, 577);
            this.tabPhong.TabIndex = 0;
            this.tabPhong.Text = "Quản lý phòng";
            this.tabPhong.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(3, -8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1094, 317);
            this.panel1.TabIndex = 1;
            // 
            // dgvListPhong
            // 
            this.dgvListPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListPhong.Location = new System.Drawing.Point(6, 23);
            this.dgvListPhong.Name = "dgvListPhong";
            this.dgvListPhong.Size = new System.Drawing.Size(1059, 205);
            this.dgvListPhong.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Location = new System.Drawing.Point(796, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(290, 300);
            this.panel3.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.txtSearch);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnXoa);
            this.groupBox2.Controls.Add(this.btnXuat);
            this.groupBox2.Controls.Add(this.btnSua);
            this.groupBox2.Controls.Add(this.btnThem);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 277);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chức năng";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(28, 170);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 40);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Làm mới";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(28, 243);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(141, 26);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(181, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 30);
            this.button1.TabIndex = 5;
            this.button1.Text = "Tìm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(28, 110);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 40);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuat.Location = new System.Drawing.Point(162, 110);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(100, 40);
            this.btnXuat.TabIndex = 2;
            this.btnXuat.Text = "Xuất Excel";
            this.btnXuat.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(162, 50);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 40);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(28, 50);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 40);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Location = new System.Drawing.Point(3, 11);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(777, 292);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 267);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phòng";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbTrangThai);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(98, 154);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(275, 55);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Trạng thái";
            // 
            // cbTrangThai
            // 
            this.cbTrangThai.FormattingEnabled = true;
            this.cbTrangThai.Items.AddRange(new object[] {
            "Trống",
            "Đã đặt",
            "Đang ở",
            "Bảo trì"});
            this.cbTrangThai.Location = new System.Drawing.Point(6, 21);
            this.cbTrangThai.Name = "cbTrangThai";
            this.cbTrangThai.Size = new System.Drawing.Size(264, 26);
            this.cbTrangThai.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbLoaiPhong);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(452, 56);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(275, 55);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Loại phòng";
            // 
            // cbLoaiPhong
            // 
            this.cbLoaiPhong.FormattingEnabled = true;
            this.cbLoaiPhong.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbLoaiPhong.Location = new System.Drawing.Point(6, 21);
            this.cbLoaiPhong.Name = "cbLoaiPhong";
            this.cbLoaiPhong.Size = new System.Drawing.Size(263, 26);
            this.cbLoaiPhong.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtSoPhong);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(98, 56);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(275, 55);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Số phòng";
            // 
            // txtSoPhong
            // 
            this.txtSoPhong.Location = new System.Drawing.Point(6, 23);
            this.txtSoPhong.Name = "txtSoPhong";
            this.txtSoPhong.Size = new System.Drawing.Size(264, 24);
            this.txtSoPhong.TabIndex = 1;
            // 
            // tabLoaiPhong
            // 
            this.tabLoaiPhong.Controls.Add(this.panel7);
            this.tabLoaiPhong.Controls.Add(this.panel6);
            this.tabLoaiPhong.Controls.Add(this.panel5);
            this.tabLoaiPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabLoaiPhong.Location = new System.Drawing.Point(4, 25);
            this.tabLoaiPhong.Name = "tabLoaiPhong";
            this.tabLoaiPhong.Padding = new System.Windows.Forms.Padding(3);
            this.tabLoaiPhong.Size = new System.Drawing.Size(1104, 577);
            this.tabLoaiPhong.TabIndex = 1;
            this.tabLoaiPhong.Text = "Quản lý loại phòng";
            this.tabLoaiPhong.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox7);
            this.panel5.Location = new System.Drawing.Point(6, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(787, 315);
            this.panel5.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox8);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(3, 327);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1098, 247);
            this.panel6.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox9);
            this.panel7.Location = new System.Drawing.Point(800, 7);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(295, 314);
            this.panel7.TabIndex = 2;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox13);
            this.groupBox7.Controls.Add(this.groupBox12);
            this.groupBox7.Controls.Add(this.groupBox11);
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Location = new System.Drawing.Point(11, 11);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(765, 285);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Thông tin loại phòng";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.dgvListLoaiPhong);
            this.groupBox8.Location = new System.Drawing.Point(11, 15);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(1068, 229);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Danh sách loại phòng";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.txtTim);
            this.groupBox9.Controls.Add(this.btnSearch);
            this.groupBox9.Controls.Add(this.button6);
            this.groupBox9.Controls.Add(this.button5);
            this.groupBox9.Controls.Add(this.btnDelete);
            this.groupBox9.Controls.Add(this.btnUpdate);
            this.groupBox9.Controls.Add(this.btnAdd);
            this.groupBox9.Location = new System.Drawing.Point(15, 10);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(270, 285);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Chức năng";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txtTenLoaiPhong);
            this.groupBox10.Location = new System.Drawing.Point(46, 49);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(300, 55);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Tên  loại phòng";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.txtMoTa);
            this.groupBox11.Location = new System.Drawing.Point(46, 143);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(300, 106);
            this.groupBox11.TabIndex = 1;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Mô tả";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.txtGiaPhong);
            this.groupBox12.Location = new System.Drawing.Point(442, 49);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(300, 55);
            this.groupBox12.TabIndex = 2;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Giá phòng";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.txtSoNguoi);
            this.groupBox13.Location = new System.Drawing.Point(442, 143);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(300, 55);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Số người tối đa";
            // 
            // dgvListLoaiPhong
            // 
            this.dgvListLoaiPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListLoaiPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListLoaiPhong.Location = new System.Drawing.Point(7, 22);
            this.dgvListLoaiPhong.Name = "dgvListLoaiPhong";
            this.dgvListLoaiPhong.Size = new System.Drawing.Size(1055, 201);
            this.dgvListLoaiPhong.TabIndex = 0;
            // 
            // txtTenLoaiPhong
            // 
            this.txtTenLoaiPhong.Location = new System.Drawing.Point(7, 22);
            this.txtTenLoaiPhong.Name = "txtTenLoaiPhong";
            this.txtTenLoaiPhong.Size = new System.Drawing.Size(287, 24);
            this.txtTenLoaiPhong.TabIndex = 0;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(7, 22);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTa.Size = new System.Drawing.Size(285, 75);
            this.txtMoTa.TabIndex = 0;
            // 
            // txtGiaPhong
            // 
            this.txtGiaPhong.Location = new System.Drawing.Point(7, 22);
            this.txtGiaPhong.Name = "txtGiaPhong";
            this.txtGiaPhong.Size = new System.Drawing.Size(287, 24);
            this.txtGiaPhong.TabIndex = 0;
            // 
            // txtSoNguoi
            // 
            this.txtSoNguoi.Location = new System.Drawing.Point(7, 23);
            this.txtSoNguoi.Name = "txtSoNguoi";
            this.txtSoNguoi.Size = new System.Drawing.Size(287, 24);
            this.txtSoNguoi.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(17, 50);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 35);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(164, 50);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(90, 35);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(17, 105);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 35);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(164, 105);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 35);
            this.button5.TabIndex = 3;
            this.button5.Text = "Xuất Excel";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(17, 160);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 35);
            this.button6.TabIndex = 4;
            this.button6.Text = "Làm mới";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(179, 229);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 30);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(17, 233);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(150, 24);
            this.txtTim.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvListPhong);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(8, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1071, 240);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Danh sách phòng";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Location = new System.Drawing.Point(6, 316);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1089, 255);
            this.panel4.TabIndex = 2;
            // 
            // PhongForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1113, 611);
            this.Controls.Add(this.tabControl1);
            this.Name = "PhongForm";
            this.Text = "PhongForm";
            this.Load += new System.EventHandler(this.PhongForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPhong.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPhong)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabLoaiPhong.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListLoaiPhong)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPhong;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvListPhong;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cbTrangThai;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cbLoaiPhong;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSoPhong;
        private System.Windows.Forms.TabPage tabLoaiPhong;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView dgvListLoaiPhong;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TextBox txtSoNguoi;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TextBox txtGiaPhong;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txtTenLoaiPhong;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}