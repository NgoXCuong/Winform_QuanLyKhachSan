using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class DatPhongRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<DatPhongModel> GetAllDatPhong()
        {
            List<DatPhongModel> listDatPhong = new List<DatPhongModel>();
            string sql = "SELECT * FROM DatPhong";
            var dataTable = connDb.ExecuteQuery(sql);
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                DatPhongModel datPhong = new DatPhongModel
                {
                    MaDatPhong = row["MaDatPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaDatPhong"]),
                    MaKhachHang = row["MaKH"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaKH"]),
                    NgayDat = row["NgayDat"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayDat"]),
                    NgayNhan = row["NgayNhanPhong"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayNhanPhong"]),
                    NgayTra = row["NgayTraPhong"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayTraPhong"]),
                    TrangThai = row["TrangThai"] == DBNull.Value ? "" : row["TrangThai"].ToString(),
                    DatCoc = row["DatCoc"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DatCoc"]),
                    MaPhong = row["MaPhong"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaPhong"]),
                    MaNV = row["MaNV"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaNV"]),
                    GhiChu = row["GhiChu"] == DBNull.Value ? "" : row["GhiChu"].ToString()
                };

                listDatPhong.Add(datPhong);
            }
            return listDatPhong;
        }

        public bool DatPhong(DatPhongModel datPhong)
        {
            string sql = "INSERT INTO DatPhong (MaKhachHang, MaPhong, NgayDat, NgayNhan, NgayTra, TrangThai, DatCoc, MaNV, GhiChu) VALUES (@MaKhachHang, @MaPhong, @NgayDat, @NgayNhan, @NgayTra, @TrangThai, @DatCoc, @MaNV, @GhiChu)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@MaKhachHang", datPhong.MaKhachHang),
                new SqlParameter("@MaPhong", datPhong.MaPhong),
                new SqlParameter("@DatCoc", datPhong.DatCoc),
                new SqlParameter("@MaNV", datPhong.MaNV),
                new SqlParameter("@GhiChu", datPhong.GhiChu),
                new SqlParameter("@NgayDat", datPhong.NgayDat),
                new SqlParameter("@NgayNhan", datPhong.NgayNhan),
                new SqlParameter("@NgayTra", datPhong.NgayTra),
                new SqlParameter("@TrangThai", datPhong.TrangThai)
            };
            return connDb.ExecuteNonQuery(sql, parameters.ToArray()) > 0;
        }

        public bool CapNhatTrangThai(int maDatPhong, string trangThaiMoi)
        {
            string sql = "UPDATE DatPhong SET TrangThai = @TrangThai WHERE MaDatPhong = @MaDatPhong";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@TrangThai", trangThaiMoi),
                new SqlParameter("@MaDatPhong", maDatPhong)
            };
            return connDb.ExecuteNonQuery(sql, parameters.ToArray()) > 0;
        }

        //Lấy mã khách hàng gán cho combobox
        public DataTable GetMaKhachHang()
        {
            string sql = "SELECT MaKH FROM KhachHang";
            return connDb.ExecuteQuery(sql);

        }
        public DataTable GetTrangThai()
        {
            string sql = "SELECT TrangThai FROM DatPhong";
            return connDb.ExecuteQuery(sql);
        }
        public DataTable GetMaPhong()
        {
            string sql = "SELECT MaPhong FROM Phong";
            return connDb.ExecuteQuery(sql);
        }
        public DataTable GetMaNV()
        {
            string sql = "SELECT MaNV FROM NhanVien";
            return connDb.ExecuteQuery(sql);
        }

    }
}
