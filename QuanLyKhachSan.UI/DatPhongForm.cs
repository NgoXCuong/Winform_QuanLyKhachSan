using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKhachSan.BLL;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.UI
{
    public partial class DatPhongForm : Form
    {
        public DatPhongForm()
        {
            InitializeComponent();

        }

        public void DatPhongForm_Load(object sender, EventArgs e)
        {
            DatPhongService datPhongService = new DatPhongService();
            dgvListPhongDatPhong.DataSource = datPhongService.GetAllDatPhong();
            dgvListPhongDatPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //LoadMaKH();
            //LoadTrangThai();
            //LoadMaPhong();
            //LoadMaNV();


            dgvListPhongDatPhong.Columns["MaDatPhong"].HeaderText = "Mã đặt phòng";
            dgvListPhongDatPhong.Columns["MaKhachHang"].HeaderText = "Mã khách hàng";
            dgvListPhongDatPhong.Columns["MaPhong"].HeaderText = "Mã phòng";
            dgvListPhongDatPhong.Columns["NgayDat"].HeaderText = "Ngày đặt";
            dgvListPhongDatPhong.Columns["NgayNhan"].HeaderText = "Ngày nhận";
            dgvListPhongDatPhong.Columns["NgayTra"].HeaderText = "Ngày trả";
            dgvListPhongDatPhong.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvListPhongDatPhong.Columns["MaNV"].HeaderText = "Mã nhân viên";
            dgvListPhongDatPhong.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvListPhongDatPhong.Columns["DatCoc"].HeaderText = "Đặt cọc";

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DatPhongModel datphong = new DatPhongModel
            {
                MaKhachHang = int.Parse(cbMaKH.Text),
                MaPhong = int.Parse(cbMaPhong.Text),
                MaNV = int.Parse(cbMaNV.Text),
                DatCoc = decimal.Parse(txtDatCoc.Text),
                NgayDat = dtpBook.Value,
                NgayNhan = dtpStart.Value,
                NgayTra = dtpEnd.Value,
                TrangThai = cbTrangThai.Text,
                GhiChu = txtGhiChu.Text
            };
        }

        //private void LoadMaKH()
        //{
        //    var repo = new DatPhongService();
        //    DataTable data = repo.GetAllDatPhong();

        //    cbMaKH.DataSource = data;
        //    cbMaKH.DisplayMember = "MaKH";
        //    cbMaKH.ValueMember = "MaKH";
        //}

        //public void LoadTrangThai()
        //{
        //    var repo = new DatPhongRepository();
        //    DataTable data = repo.GetTrangThai();
        //    cbTrangThai.DataSource = data;
        //    cbTrangThai.DisplayMember = "TrangThai";
        //    cbTrangThai.ValueMember = "TrangThai";
        //}

        //public void LoadMaPhong()
        //{
        //    var repo = new DatPhongRepository();
        //    DataTable data = repo.GetMaPhong();
        //    cbMaPhong.DataSource = data;
        //    cbMaPhong.DisplayMember = "MaPhong";
        //    cbMaPhong.ValueMember = "MaPhong";
        //}
        //public void LoadMaNV()
        //{
        //    var repo = new DatPhongRepository();
        //    DataTable data = repo.GetMaNV();
        //    cbMaNV.DataSource = data;
        //    cbMaNV.DisplayMember = "MaNV";
        //    cbMaNV.ValueMember = "MaNV";
        //}
    }
}
