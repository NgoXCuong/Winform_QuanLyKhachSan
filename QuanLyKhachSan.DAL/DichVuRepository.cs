using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.DAL
{
    public class DichVuRepository
    {
        private readonly ConnectDB connDb = new ConnectDB();

        public List<DichVuModel> GetAllDichVu()
        {
            List<DichVuModel> listDichVu = new List<DichVuModel>();
            string sql = "SELECT * FROM DichVu";
            var dataTable = connDb.ExecuteQuery(sql);
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                DichVuModel dichVu = new DichVuModel
                {
                    MaDV = (int)row["MaDV"],
                    TenDV = row["TenDV"].ToString(),
                    DonGia = (decimal)row["DonGia"],
                    DonViTinh = row["DonViTinh"].ToString()
                };
                listDichVu.Add(dichVu);
            }
            return listDichVu;
        }

        public bool ThemDichVu(DichVuModel dv)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = @"INSERT INTO DichVu 
                            (TenDV, DonGia, DonViTinh) 
                            VALUES (@TenDV, @DonGia, @DonViTinh)";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDV", dv.TenDV),
                    new SqlParameter("@DonGia", dv.DonGia),
                    new SqlParameter("@DonViTinh", dv.DonViTinh)
                };
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
        }

        public bool SuaDichVu(DichVuModel dv)
        {
            using (SqlConnection conn = new SqlConnection(connDb.GetConnection().ConnectionString))
            {
                string sql = @"UPDATE DichVu SET TenDV = @TenDV, 
                        DonGia = @DonGia, 
                        DonViTinh = @DonViTinh
                        WHERE MaDV = @MaDV";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDV", dv.TenDV),
                    new SqlParameter("@DonGia", dv.DonGia),
                    new SqlParameter("@DonViTinh", dv.DonViTinh),
                    new SqlParameter("@MaDV", dv.MaDV)
                };
                return connDb.ExecuteNonQuery(sql, parameters) > 0;
            }
        }

        public bool XoaDichVu(int maDichVu)
        {
            string sql = "DELETE FROM DichVu WHERE MaDV = @MaDV";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDV", maDichVu)
            };
            return connDb.ExecuteNonQuery(sql, parameters) > 0;
        }

        public List<DichVuModel> TimDichVu(string keyword)
        {
            List<DichVuModel> listDichVu = new List<DichVuModel>();

            string sql = @"SELECT * FROM DichVu WHERE 
                MaDV Like @Keyword
                OR TenDV Like @Keyword
                OR DonViTinh Like @Keyword
                OR CAST(DonGia AS NVARCHAR) = @Keyword";

            if (string.IsNullOrWhiteSpace(keyword))
            {
                sql = "SELECT * FROM DichVu"; 
            }

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", keyword ?? string.Empty),
            };

            var dataTable = connDb.ExecuteQuery(sql, parameters);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                DichVuModel dichVu = new DichVuModel
                {
                    MaDV = (int)row["MaDV"],
                    TenDV = row["TenDV"].ToString(),
                    DonGia = (decimal)row["DonGia"],
                    DonViTinh = row["DonViTinh"].ToString()
                };
                listDichVu.Add(dichVu);
            }
            return listDichVu;

        }

    }
}
