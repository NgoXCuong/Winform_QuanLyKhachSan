using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class DichVuRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        // ================================
        // 🔹 Lấy tất cả dịch vụ
        // ================================
        public List<DichVuModel> GetAllDichVu()
        {
            var listDichVu = new List<DichVuModel>();
            string sql = "SELECT * FROM DichVu";
            var dataTable = connDb.ExecuteQuery(sql);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                var dichVu = new DichVuModel
                {
                    MaDV = row["MaDV"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaDV"]),
                    TenDichVu = row["TenDichVu"].ToString(),
                    DonGia = row["DonGia"] == DBNull.Value ? 0 : (decimal)row["DonGia"],
                    MoTa = row["MoTa"]?.ToString(),
                    DonViTinh = row["DonViTinh"]?.ToString(),
                    Anh = row["Anh"] as byte[],
                };
                listDichVu.Add(dichVu);
            }

            return listDichVu;
        }

        // ================================
        // 🔹 Thêm dịch vụ mới
        // ================================
        public bool ThemDichVu(DichVuModel dv)
        {
            string sql = @"INSERT INTO DichVu (TenDichVu, DonGia, MoTa, DonViTinh, Anh) 
                           VALUES (@TenDichVu, @DonGia, @MoTa, @DonViTinh, @Anh)";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDichVu", dv.TenDichVu),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@MoTa", dv.MoTa ?? (object)DBNull.Value),
                new SqlParameter("@Anh", SqlDbType.VarBinary) { Value = dv.Anh ?? (object)DBNull.Value },
                new SqlParameter("@DonViTinh", dv.DonViTinh),

            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // ================================
        // 🔹 Sửa thông tin dịch vụ
        // ================================
        public bool SuaDichVu(DichVuModel dv)
        {
            string sql = @"UPDATE DichVu SET 
                           TenDichVu = @TenDichVu, 
                           DonGia = @DonGia, 
                           MoTa = @MoTa,
                           DonViTinh = @DonViTinh,
                           WHERE MaDV = @MaDV";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDichVu", dv.TenDichVu),
                new SqlParameter("@DonGia", dv.DonGia),
                new SqlParameter("@MoTa", dv.MoTa ?? (object)DBNull.Value),
                new SqlParameter("@DonViTinh", dv.DonViTinh ?? (object)DBNull.Value),
                new SqlParameter("@MaDV", dv.MaDV)
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // ================================
        // 🔹 Xóa dịch vụ theo mã
        // ================================
        public bool XoaDichVu(int maDichVu)
        {
            string sql = "DELETE FROM DichVu WHERE MaDV = @MaDV";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDV", maDichVu)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // ================================
        // 🔹 Tìm kiếm dịch vụ
        // ================================
        public List<DichVuModel> TimDichVu(string keyword)
        {
            var listDichVu = new List<DichVuModel>();
            string sql = "SELECT * FROM DichVu WHERE 1=1";
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (int.TryParse(keyword, out int maDV))
                {
                    sql += " AND MaDV = @MaDV";
                    parameters.Add(new SqlParameter("@MaDV", maDV));
                }
                else if (decimal.TryParse(keyword, out decimal donGia))
                {
                    sql += " AND DonGia = @DonGia";
                    parameters.Add(new SqlParameter("@DonGia", donGia));
                }
                else
                {
                    sql += " AND (TenDichVu LIKE @Keyword OR MoTa LIKE @Keyword OR DonViTinh LIKE @Keyword)";
                    parameters.Add(new SqlParameter("@Keyword", "%" + keyword + "%"));
                }
            }

            var dataTable = connDb.ExecuteQuery(sql, parameters.ToArray());

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                var dichVu = new DichVuModel
                {
                    MaDV = (int)row["MaDV"],
                    TenDichVu = row["TenDichVu"].ToString(),
                    DonGia = row["DonGia"] == DBNull.Value ? 0 : (decimal)row["DonGia"],
                    MoTa = row["MoTa"]?.ToString(),
                    DonViTinh = row["DonViTinh"]?.ToString(),
                    Anh = row["Anh"] as byte[],
                };
                listDichVu.Add(dichVu);
            }

            return listDichVu;
        }

        // ================================
        // 🔹 Lấy dịch vụ theo mã
        // ================================
        public DichVuModel GetById(int maDV)
        {
            string sql = "SELECT * FROM DichVu WHERE MaDV = @MaDV";
            var parameter = new SqlParameter("@MaDV", maDV);
            var table = connDb.ExecuteQuery(sql, new[] { parameter });

            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            return new DichVuModel
            {
                MaDV = (int)row["MaDV"],
                TenDichVu = row["TenDichVu"].ToString(),
                DonGia = row["DonGia"] == DBNull.Value ? 0 : (decimal)row["DonGia"],
                MoTa = row["MoTa"]?.ToString(),
                DonViTinh = row["DonViTinh"]?.ToString(),
                Anh = row["Anh"] as byte[],
            };
        }

        // ================================
        // 🖼️ Cập nhật ảnh dịch vụ (VARBINARY)
        // ================================
        public bool CapNhatAnh(int maDV, string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            string sql = "UPDATE DichVu SET Anh = @Anh WHERE MaDV = @MaDV";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Anh", SqlDbType.VarBinary) { Value = imageBytes },
                new SqlParameter("@MaDV", maDV)
            };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        // ================================
        // 🖼️ Lấy ảnh dịch vụ theo mã
        // ================================
        public byte[] LayAnhDichVu(int maDV)
        {
            string sql = "SELECT Anh FROM DichVu WHERE MaDV = @MaDV";
            SqlParameter[] parameters = { new SqlParameter("@MaDV", maDV) };

            var result = connDb.ExecuteScalar(sql, parameters);
            return result != DBNull.Value && result != null ? (byte[])result : null;
        }

        // ================================
        // ❌ Xóa ảnh dịch vụ (đặt NULL)
        // ================================
        public bool XoaAnhDichVu(int maDV)
        {
            string sql = "UPDATE DichVu SET Anh = NULL WHERE MaDV = @MaDV";
            SqlParameter[] parameters = { new SqlParameter("@MaDV", maDV) };

            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

    }
}
