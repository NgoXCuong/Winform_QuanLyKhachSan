using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class DatPhongRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        // =============================
        // LẤY DANH SÁCH ĐẶT PHÒNG
        // =============================
        //public List<DatPhongModel> GetAllDatPhong()
        //{
        //    var list = new List<DatPhongModel>();
        //    string sql = "SELECT * FROM DatPhong";

        //    try
        //    {
        //        var table = connDb.ExecuteQuery(sql);
        //        foreach (DataRow row in table.Rows)
        //        {
        //            list.Add(MapDatPhong(row));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Lỗi GetAllDatPhong: " + ex.Message);
        //    }

        //    return list;
        //}
        public List<DatPhongModel> GetAllDatPhong()
        {
            var list = new List<DatPhongModel>();
            string sql = "SELECT * FROM DatPhong";

            try
            {
                var table = connDb.ExecuteQuery(sql);
                foreach (DataRow row in table.Rows)
                {
                    var dp = MapDatPhong(row);

                    // 💥 Tính lại tổng tiền bao gồm cả dịch vụ
                    dp.TongTien = TinhTongTien(dp.MaDatPhong);

                    list.Add(dp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetAllDatPhong: " + ex.Message);
            }

            return list;
        }


        // =============================
        // LẤY ĐẶT PHÒNG THEO ID
        // =============================
        public DatPhongModel GetDatPhongById(int maDatPhong)
        {
            string sql = "SELECT * FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
            var param = new SqlParameter("@MaDatPhong", maDatPhong);

            try
            {
                var table = connDb.ExecuteQuery(sql, new[] { param });
                if (table.Rows.Count == 0) return null;
                return MapDatPhong(table.Rows[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetDatPhongById: " + ex.Message);
                return null;
            }
        }

        
        public int InsertDatPhong(DatPhongModel dp)
        {
            int maMoi = 0;
            string query = @"
        INSERT INTO DatPhong 
        (MaKH, MaPhong, MaNV, NgayNhanPhong, NgayTraPhong, SoNguoi, TongTien, TrangThai, GhiChu, NgayTao)
        VALUES 
        (@MaKH, @MaPhong, @MaNV, @NgayNhanPhong, @NgayTraPhong, @SoNguoi, @TongTien, @TrangThai, @GhiChu, @NgayTao);
        SELECT SCOPE_IDENTITY();";

            var parameters = new[]
            {
        new SqlParameter("@MaKH", dp.MaKH),
        new SqlParameter("@MaPhong", dp.MaPhong),
        new SqlParameter("@MaNV", dp.MaNV.HasValue ? (object)dp.MaNV.Value : DBNull.Value),
        new SqlParameter("@NgayNhanPhong", dp.NgayNhanPhong),
        new SqlParameter("@NgayTraPhong", dp.NgayTraPhong),
        new SqlParameter("@SoNguoi", dp.SoNguoi),
        new SqlParameter("@TongTien", dp.TongTien),
        new SqlParameter("@TrangThai", dp.TrangThai ?? (object)DBNull.Value),
        new SqlParameter("@GhiChu", dp.GhiChu ?? (object)DBNull.Value),
        new SqlParameter("@NgayTao", dp.NgayTao)
    };

            try
            {
                var result = connDb.ExecuteScalar(query, parameters);

                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    maMoi = id;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("⚠️ Không lấy được SCOPE_IDENTITY() (InsertDatPhong).", "Cảnh báo SQL");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "❌ Lỗi khi thực hiện InsertDatPhong:\n" + ex.Message,
                    "Lỗi SQL", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
            }

            return maMoi;
        }





        // =============================
        // CẬP NHẬT ĐẶT PHÒNG
        // =============================
        public bool UpdateDatPhong(DatPhongModel dp)
        {
            string sql = @"
                UPDATE DatPhong SET
                    MaKH = @MaKH,
                    MaPhong = @MaPhong,
                    MaNV = @MaNV,
                    NgayNhanPhong = @NgayNhanPhong,
                    NgayTraPhong = @NgayTraPhong,
                    SoNguoi = @SoNguoi,
                    TongTien = @TongTien,
                    TrangThai = @TrangThai,
                    GhiChu = @GhiChu
                WHERE MaDatPhong = @MaDatPhong";

            var parameters = new[]
            {
                new SqlParameter("@MaDatPhong", dp.MaDatPhong),
                new SqlParameter("@MaKH", dp.MaKH),
                new SqlParameter("@MaPhong", dp.MaPhong),
                new SqlParameter("@MaNV", dp.MaNV.HasValue ? (object)dp.MaNV.Value : DBNull.Value),
                new SqlParameter("@NgayNhanPhong", dp.NgayNhanPhong),
                new SqlParameter("@NgayTraPhong", dp.NgayTraPhong),
                new SqlParameter("@SoNguoi", dp.SoNguoi),
                new SqlParameter("@TongTien", dp.TongTien),
                new SqlParameter("@TrangThai", dp.TrangThai ?? (object)DBNull.Value),
                new SqlParameter("@GhiChu", string.IsNullOrEmpty(dp.GhiChu) ? (object)DBNull.Value : dp.GhiChu)
            };



            try
            {
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi UpdateDatPhong: " + ex.Message);
                return false;
            }
        }

        // =============================
        // XÓA ĐẶT PHÒNG
        // =============================
        public bool DeleteDatPhong(int maDatPhong)
        {
            string sql = "DELETE FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
            var parameters = new[] { new SqlParameter("@MaDatPhong", maDatPhong) };

            try
            {
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DeleteDatPhong: " + ex.Message);
                return false;
            }
        }

        // =============================
        // CẬP NHẬT TRẠNG THÁI PHÒNG
        // =============================
        public bool CapNhatTrangThaiPhong(int maPhong, string trangThai)
        {
            string sql = "UPDATE Phong SET TrangThai = @TrangThai WHERE MaPhong = @MaPhong";
            var parameters = new[]
            {
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@MaPhong", maPhong)
            };

            try
            {
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi CapNhatTrangThaiPhong: " + ex.Message);
                return false;
            }
        }

        // =============================
        // THÊM CHI TIẾT DỊCH VỤ
        // =============================
        public bool InsertChiTietDichVu(DatPhongDichVuModel dv)
        {
            string sql = @"
                INSERT INTO DatPhong_DichVu (MaDatPhong, MaDV, SoLuong, DonGia, NgaySuDung)
                VALUES (@MaDatPhong, @MaDV, @SoLuong, @DonGia, @NgaySuDung)";

            var parameters = new[]
            {
                new SqlParameter("@MaDatPhong", dv.MaDatPhong),
                new SqlParameter("@MaDV", dv.MaDV),
                new SqlParameter("@SoLuong", dv.SoLuong),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@NgaySuDung", dv.NgaySuDung)
            };

            try
            {
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi InsertChiTietDichVu: " + ex.Message);
                return false;
            }
        }

        // =============================
        // CẬP NHẬT TRẠNG THÁI ĐẶT PHÒNG
        // =============================
        public bool UpdateTrangThai(int maDatPhong, string trangThaiMoi)
        {
            string sql = "UPDATE DatPhong SET TrangThai = @TrangThaiMoi WHERE MaDatPhong = @MaDatPhong";
            var parameters = new[]
            {
                new SqlParameter("@TrangThaiMoi", trangThaiMoi),
                new SqlParameter("@MaDatPhong", maDatPhong)
            };

            try
            {
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi UpdateTrangThai: " + ex.Message);
                return false;
            }
        }

        // =============================
        // LẤY DỊCH VỤ THEO ĐẶT PHÒNG
        // =============================
        public List<DatPhongDichVuModel> GetDichVuByDatPhong(int maDatPhong)
        {
            var list = new List<DatPhongDichVuModel>();
            string sql = "SELECT * FROM DatPhong_DichVu WHERE MaDatPhong = @MaDatPhong";
            var param = new SqlParameter("@MaDatPhong", maDatPhong);

            try
            {
                var table = connDb.ExecuteQuery(sql, new[] { param });
                foreach (DataRow row in table.Rows)
                {
                    list.Add(new DatPhongDichVuModel
                    {
                        MaDPDV = Convert.ToInt32(row["MaDPDV"]),
                        MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                        MaDV = Convert.ToInt32(row["MaDV"]),
                        SoLuong = Convert.ToInt32(row["SoLuong"]),
                        DonGia = Convert.ToDecimal(row["DonGia"]),
                        NgaySuDung = Convert.ToDateTime(row["NgaySuDung"])
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetDichVuByDatPhong: " + ex.Message);
            }

            return list;
        }

        // =============================
        // LẤY GIÁ PHÒNG
        // =============================
        public decimal GetGiaPhong(int maPhong)
        {
            string sql = @"
                SELECT lp.GiaCoBan
                FROM Phong p
                JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
                WHERE p.MaPhong = @MaPhong";

            var param = new SqlParameter("@MaPhong", maPhong);

            try
            {
                var table = connDb.ExecuteQuery(sql, new[] { param });
                if (table.Rows.Count > 0)
                    return Convert.ToDecimal(table.Rows[0]["GiaCoBan"]);

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetGiaPhong: " + ex.Message);
                return 0;
            }
        }

        // =============================
        // MAP DỮ LIỆU
        // =============================
        private DatPhongModel MapDatPhong(DataRow row)
        {
            return new DatPhongModel
            {
                MaDatPhong = Convert.ToInt32(row["MaDatPhong"]),
                MaKH = Convert.ToInt32(row["MaKH"]),
                MaPhong = Convert.ToInt32(row["MaPhong"]),
                MaNV = row["MaNV"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["MaNV"]),
                NgayNhanPhong = Convert.ToDateTime(row["NgayNhanPhong"]),
                NgayTraPhong = Convert.ToDateTime(row["NgayTraPhong"]),
                SoNguoi = Convert.ToInt32(row["SoNguoi"]),
                TongTien = row["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongTien"]),
                TrangThai = row["TrangThai"] == DBNull.Value ? "" : row["TrangThai"].ToString(),
                GhiChu = row["GhiChu"] == DBNull.Value ? "" : row["GhiChu"].ToString(),
                NgayTao = Convert.ToDateTime(row["NgayTao"])
            };
        }

        // Xóa chi tiết dịch vụ
        public bool DeleteChiTietDichVu(int maDPDV)
        {
            string sql = "DELETE FROM DatPhong_DichVu WHERE MaDPDV = @MaDPDV";
            var param = new SqlParameter("@MaDPDV", maDPDV);
            try
            {
                return connDb.ExecuteNonQuery(sql, new[] { param }) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DeleteChiTietDichVu: " + ex.Message);
                return false;
            }
        }

        // =============================
        // TÍNH TỔNG TIỀN (PHÒNG + DỊCH VỤ)
        // =============================
        public decimal TinhTongTien(int maDatPhong)
        {
            decimal tongTienPhong = 0;
            decimal tongTienDichVu = 0;

            try
            {
                // ✅ Lấy tổng tiền phòng
                string sqlPhong = "SELECT TongTien FROM DatPhong WHERE MaDatPhong = @MaDatPhong";
                var param = new SqlParameter("@MaDatPhong", maDatPhong);
                var resultPhong = connDb.ExecuteScalar(sqlPhong, new[] { param });
                if (resultPhong != null && resultPhong != DBNull.Value)
                    tongTienPhong = Convert.ToDecimal(resultPhong);

                // ✅ Lấy tổng tiền dịch vụ
                string sqlDV = "SELECT SUM(SoLuong * DonGia) FROM DatPhong_DichVu WHERE MaDatPhong = @MaDatPhong";
                var resultDV = connDb.ExecuteScalar(sqlDV, new[] { param });
                if (resultDV != null && resultDV != DBNull.Value)
                    tongTienDichVu = Convert.ToDecimal(resultDV);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi TinhTongTien: " + ex.Message);
            }

            return tongTienPhong + tongTienDichVu;
        }

        public bool UpdateTongTien(int maDatPhong, decimal tongTien)
        {
            SqlConnection conn = null;
            try
            {
                conn = connDb.GetConnection();

                string query = "UPDATE DatPhong SET TongTien = @TongTien WHERE MaDatPhong = @MaDatPhong";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật tổng tiền đặt phòng: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close(); // 🔒 Đảm bảo đóng kết nối lại
            }
        }


        public bool DeleteDichVuByDatPhong(int maDatPhong)
        {
            string sql = "DELETE FROM DatPhong_DichVu WHERE MaDatPhong = @MaDatPhong";
            var param = new SqlParameter("@MaDatPhong", maDatPhong);

            try
            {
                return connDb.ExecuteNonQuery(sql, new[] { param }) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DeleteDichVuByDatPhong: " + ex.Message);
                return false;
            }
        }
        public bool CapNhatDichVuChoDatPhong(int maDatPhong, List<DatPhongDichVuModel> danhSachDichVu)
        {
            bool ok = true;

            try
            {
                // 🔹 Xóa tất cả dịch vụ cũ
                DeleteDichVuByDatPhong(maDatPhong);

                // 🔹 Thêm lại danh sách dịch vụ mới
                foreach (var dv in danhSachDichVu)
                {
                    var sql = @"
                INSERT INTO DatPhong_DichVu (MaDatPhong, MaDV, SoLuong, DonGia, NgaySuDung)
                VALUES (@MaDatPhong, @MaDV, @SoLuong, @DonGia, @NgaySuDung)";

                    var parameters = new[]
                    {
                new SqlParameter("@MaDatPhong", maDatPhong),
                new SqlParameter("@MaDV", dv.MaDV),
                new SqlParameter("@SoLuong", dv.SoLuong),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@NgaySuDung", dv.NgaySuDung)
            };

                    if (connDb.ExecuteNonQuery(sql, parameters) <= 0)
                    {
                        ok = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi CapNhatDichVuChoDatPhong: " + ex.Message);
                ok = false;
            }

            return ok;
        }

    }
}
