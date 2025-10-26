using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
            this.Load += FormThongKe_Load;
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

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            cboLoaiThongKe.Items.Clear();
            cboLoaiThongKe.Items.AddRange(new string[] { "Ngày", "Tháng", "Năm" });
            cboLoaiThongKe.SelectedIndexChanged += cboLoaiThongKe_SelectedIndexChanged;
            cboLoaiThongKe.SelectedIndex = 0;
        }
        

        private void LoadBieuDo(string loaiThongKe)
        {
            chart1.Series.Clear();
            var series = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Column
            };

            if (loaiThongKe == "Ngày")
            {
                var data = thongKeService.GetDoanhThuTheoNgay();
                foreach (var item in data)
                {
                    series.Points.AddXY(item.Key.ToString("dd/MM/yyyy"), item.Value);
                }
                chart1.ChartAreas[0].AxisX.Title = "Ngày";
            }
            else if (loaiThongKe == "Tháng")
            {
                var data = thongKeService.GetDoanhThuTheoThang();
                foreach (var item in data)
                {
                    series.Points.AddXY("Tháng " + item.Key, item.Value);
                }
                chart1.ChartAreas[0].AxisX.Title = "Tháng";
            }
            else if (loaiThongKe == "Năm")
            {
                var data = thongKeService.GetDoanhThuTheoNam();
                foreach (var item in data)
                {
                    series.Points.AddXY(item.Key.ToString(), item.Value);
                }
                chart1.ChartAreas[0].AxisX.Title = "Năm";
            }

            chart1.Series.Add(series);
            chart1.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0";
        }

        private void cboLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBieuDo(cboLoaiThongKe.SelectedItem.ToString());
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
