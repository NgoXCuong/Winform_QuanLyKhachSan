using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyKhachSan.BLL;

namespace QuanLyKhachSan.UI
{
    public partial class ThongKeForm : Form
    {
        // Service để lấy dữ liệu
        private readonly ThongKeService thongKeService = new ThongKeService();

        public ThongKeForm()
        {
            InitializeComponent();
            this.Load += FormThongKe_Load;
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            // 1. Cấu hình ComboBox
            cboLoaiThongKe.Items.Clear();
            cboLoaiThongKe.Items.AddRange(new string[] { "Theo Ngày", "Theo Tháng", "Theo Năm" });
            cboLoaiThongKe.SelectedIndex = 0; // Mặc định chọn Theo Tháng
            cboLoaiThongKe.SelectedIndexChanged += cboLoaiThongKe_SelectedIndexChanged;

            // 2. Tải dữ liệu tổng quan (4 ô vuông trên cùng)
            LoadTongQuan();

            // 3. Tải biểu đồ mặc định
            LoadBieuDoDoanhThu(cboLoaiThongKe.SelectedItem.ToString());

            // 4. Tải biểu đồ tròn (Ví dụ: Tỷ trọng doanh thu)
            LoadBieuDoTyLe();
        }

        private void LoadTongQuan()
        {
            try
            {
                // Hiển thị số liệu tổng quan
                // Sử dụng N0 để format số có dấu phẩy (ví dụ: 1,000,000)
                lbTongDoanhThu.Text = thongKeService.GetTongDoanhThu().ToString("N0") + " đ";
                lbTongKhachHang.Text = thongKeService.GetSoKhach().ToString() + " Khách hàng";
                lbTongSoPhong.Text = thongKeService.GetSoPhong().ToString() + " Phòng";
                lbTongNhanVien.Text = thongKeService.GetSoNhanVien().ToString() + " Nhân viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê tổng quan: " + ex.Message);
            }
        }

        private void LoadBieuDoDoanhThu(string loaiThongKe)
        {
            try
            {
                // Xóa các series cũ để vẽ lại
                chart1.Series.Clear();
                chart1.Titles.Clear();

                // Tạo Series mới
                Series series = new Series("DoanhThu");
                series.ChartType = SeriesChartType.Column; // Biểu đồ cột
                series.Color = Color.DodgerBlue; // Màu cột
                series.IsValueShownAsLabel = true; // Hiển thị giá trị trên đầu cột
                series.LabelFormat = "#,##0"; // Format số tiền trên cột

                // Đặt tiêu đề cho biểu đồ
                Title title = new Title();
                title.Font = new Font("Arial", 12, FontStyle.Bold);
                title.ForeColor = Color.Blue;

                // Lấy dữ liệu từ Service dựa trên lựa chọn
                if (loaiThongKe == "Theo Ngày")
                {
                    title.Text = "BIỂU ĐỒ DOANH THU THEO NGÀY (30 NGÀY GẦN NHẤT)";
                    // Giả sử Service trả về Dictionary<DateTime, decimal>
                    var data = thongKeService.GetDoanhThuTheoNgay();
                    foreach (var item in data)
                    {
                        // AddXY(Tên trục X, Giá trị trục Y)
                        series.Points.AddXY(item.Key.ToString("dd/MM"), item.Value);
                    }
                    chart1.ChartAreas[0].AxisX.Title = "Ngày";
                }
                else if (loaiThongKe == "Theo Tháng")
                {
                    title.Text = "BIỂU ĐỒ DOANH THU THEO THÁNG (TRONG NĂM NAY)";
                    // Giả sử Service trả về Dictionary<int, decimal> (Key là tháng 1-12)
                    var data = thongKeService.GetDoanhThuTheoThang();
                    foreach (var item in data)
                    {
                        series.Points.AddXY("T" + item.Key, item.Value);
                    }
                    chart1.ChartAreas[0].AxisX.Title = "Tháng";
                }
                else if (loaiThongKe == "Theo Năm")
                {
                    title.Text = "BIỂU ĐỒ DOANH THU CÁC NĂM";
                    // Giả sử Service trả về Dictionary<int, decimal> (Key là Năm)
                    var data = thongKeService.GetDoanhThuTheoNam();
                    foreach (var item in data)
                    {
                        series.Points.AddXY(item.Key.ToString(), item.Value);
                    }
                    chart1.ChartAreas[0].AxisX.Title = "Năm";
                }

                chart1.Titles.Add(title);
                chart1.Series.Add(series);

                // CẤU HÌNH TRỤC Y (Sửa đoạn này)
                chart1.ChartAreas[0].RecalculateAxesScale(); // Tính toán lại tỉ lệ
                chart1.ChartAreas[0].AxisY.Minimum = 0;      // Luôn bắt đầu từ 0
                chart1.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
                chart1.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0";

                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

                // Tùy chọn: Nếu muốn trục Y hiện số chẵn đẹp (Ví dụ: 3 triệu, 4 triệu...)
                chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu đồ doanh thu: " + ex.Message);
            }
        }

        // Hàm phụ để load biểu đồ tròn (Chart 2) - Nếu bạn có dữ liệu này
        private void LoadBieuDoTyLe()
        {
            try
            {
                // 1. Xóa dữ liệu cũ
                chart2.Series.Clear();
                chart2.Titles.Clear();

                // 2. Đặt tiêu đề
                Title title = new Title();
                title.Text = "CƠ CẤU DOANH THU";
                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                title.ForeColor = Color.Blue;
                chart2.Titles.Add(title);

                // 3. Tạo Series biểu đồ tròn
                Series series = new Series("TyLe");
                series.ChartType = SeriesChartType.Pie; // Kiểu biểu đồ tròn

                // 4. Lấy dữ liệu từ Service
                decimal tongTienPhong = thongKeService.GetTongTienPhong();
                decimal tongTienDichVu = thongKeService.GetTongTienDichVu();

                // Kiểm tra nếu chưa có dữ liệu thì không vẽ để tránh lỗi
                if (tongTienPhong == 0 && tongTienDichVu == 0) return;

                // 5. Thêm dữ liệu vào biểu đồ
                // AddXY(Tên hiển thị, Giá trị)
                int index1 = series.Points.AddXY("Tiền Phòng", tongTienPhong);
                int index2 = series.Points.AddXY("Dịch Vụ", tongTienDichVu);

                // 6. Cấu hình hiển thị
                // Hiển thị phần trăm trên miếng bánh
                series.Label = "#PERCENT{P1}"; // P1 là lấy 1 số thập phân (VD: 70.5%)
                series.LegendText = "#VALX";   // Hiển thị tên (Tiền Phòng, Dịch Vụ) ở chú thích

                // Màu sắc (Tùy chọn)
                series.Points[index1].Color = Color.FromArgb(65, 140, 240); // Xanh dương
                series.Points[index2].Color = Color.FromArgb(252, 180, 65); // Cam

                // Hiển thị giá trị thực khi di chuột vào
                series.Points[index1].ToolTip = $"Tiền phòng: {tongTienPhong:N0} VNĐ";
                series.Points[index2].ToolTip = $"Dịch vụ: {tongTienDichVu:N0} VNĐ";

                chart2.Series.Add(series);

                // Cấu hình Legend (Chú thích)
                if (chart2.Legends.Count > 0)
                {
                    chart2.Legends[0].Alignment = StringAlignment.Center;
                    chart2.Legends[0].Docking = Docking.Bottom; // Đưa chú thích xuống dưới đáy
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu đồ tròn: " + ex.Message);
            }
        }

        private void cboLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiThongKe.SelectedItem != null)
            {
                LoadBieuDoDoanhThu(cboLoaiThongKe.SelectedItem.ToString());
            }
        }

        
    }
}
