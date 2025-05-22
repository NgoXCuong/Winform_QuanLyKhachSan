using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using QuanLyKhachSan.Models;


namespace QuanLyKhachSan.DAL
{
    public class KhachHangRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();
        public List<KhachHangModel> GetAllKhachHang()
        {
            List<KhachHangModel> listKhachHang = new List<KhachHangModel>();
            string sql = "SELECT * FROM KhachHang";
            
            var dataTable = connDb.ExecuteQuery(sql);
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                KhachHangModel khachHang = new KhachHangModel
                {
                    MaKH = (int)row["MaKH"],
                    HoTen = row["HoTen"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    NgaySinh = (DateTime)row["NgaySinh"],
                    CMND = row["CMND"].ToString(),
                    SoDienThoai = row["SDT"].ToString(),
                    Email = row["Email"].ToString(),
                    DiaChi = row["DiaChi"].ToString()
                };
                listKhachHang.Add(khachHang);
            }
            return listKhachHang;
        }
        public bool ThemKhachHang(KhachHangModel kh)
        {
            string sql = @"INSERT INTO KhachHang 
                        (HoTen, GioiTinh, NgaySinh, CMND, SDT, Email, DiaChi) 
                        VALUES (@HoTen, @GioiTinh, @NgaySinh, @CMND, @SDT, @Email, @DiaChi)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@GioiTinh", kh.GioiTinh),
                new SqlParameter("@NgaySinh", kh.NgaySinh),
                new SqlParameter("@CMND", kh.CMND),
                new SqlParameter("@SDT", kh.SoDienThoai),
                new SqlParameter("@Email", kh.Email),
                new SqlParameter("@DiaChi", kh.DiaChi)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool SuaKhachHang(KhachHangModel kh)
        {
            string sql = @"UPDATE KhachHang SET HoTen = @HoTen, 
                    GioiTinh = @GioiTinh, 
                    NgaySinh = @NgaySinh,
                    CMND = @CMND,
                    SDT = @SDT,
                    Email = @Email,
                    DiaChi = @DiaChi
                    WHERE MaKH = @MaKH";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@GioiTinh", kh.GioiTinh),
                new SqlParameter("@NgaySinh", kh.NgaySinh),
                new SqlParameter("@CMND", kh.CMND),
                new SqlParameter("@SDT", kh.SoDienThoai),
                new SqlParameter("@Email", kh.Email),
                new SqlParameter("@DiaChi", kh.DiaChi)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public bool XoaKhachHang(int maKH)
        {
            string sql = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKH", maKH)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public List<KhachHangModel> TimKhachHang(string keyword)
        {
            List<KhachHangModel> listKhachHang = new List<KhachHangModel>();

            string sql = "SELECT * FROM KhachHang WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (int.TryParse(keyword, out int maKH))
                {
                    sql += " AND MaKH = @MaKH";// Nếu là số → tìm theo MãKH
                    parameters.Add(new SqlParameter("@MaKH", maKH));
                }
                else
                {
                    // Nếu không phải số → tìm gần đúng các trường khác
                    List<string> conditions = new List<string>
                {
                    "HoTen LIKE @kw", "GioiTinh LIKE @kw", "CMND LIKE @kw",
                    "SDT LIKE @kw", "Email LIKE @kw", "DiaChi LIKE @kw"
                };

                    parameters.Add(new SqlParameter("@kw", "%" + keyword + "%"));

                    if (DateTime.TryParse(keyword, out DateTime ngaySinh))
                    {
                        conditions.Add("(NgaySinh >= @NgaySinhStart AND NgaySinh < @NgaySinhEnd)");
                        parameters.Add(new SqlParameter("@NgaySinhStart", ngaySinh.Date));
                        parameters.Add(new SqlParameter("@NgaySinhEnd", ngaySinh.Date.AddDays(1)));
                    }

                    sql += " AND (" + string.Join(" OR ", conditions) + ")";
                }
            }

            var dataTable = connDb.ExecuteQuery(sql, parameters.ToArray());

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                KhachHangModel khachHang = new KhachHangModel
                {
                    MaKH = (int)row["MaKH"],
                    HoTen = row["HoTen"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    NgaySinh = (DateTime)row["NgaySinh"],
                    CMND = row["CMND"].ToString(),
                    SoDienThoai = row["SDT"].ToString(),
                    Email = row["Email"].ToString(),
                    DiaChi = row["DiaChi"].ToString()
                };
                listKhachHang.Add(khachHang);
            }
            return listKhachHang;
        }
    }
}
