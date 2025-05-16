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

namespace QuanLyKhachSan.UI
{
    public partial class ThongKeForm : Form
    {
        private readonly ThongKeService thongKeService = new ThongKeService();
        public ThongKeForm()
        {
            InitializeComponent();
            LoadThongKe();
        }

        private void LoadThongKe()
        {
            try
            {
                lbTongDoanhThu.Text = thongKeService.GetTongDoanhThu().ToString("N0") + " VNĐ";
                lbTongKhachHang.Text = thongKeService.GetSoKhach().ToString() + " Khách hàng";
                lbTongSoPhong.Text = thongKeService.GetSoPhong().ToString()  + " Phòng";
                lbTongNhanVien.Text = thongKeService.GetSoNhanVien().ToString() + " Nhân viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message);
            }
        }
    }
}
