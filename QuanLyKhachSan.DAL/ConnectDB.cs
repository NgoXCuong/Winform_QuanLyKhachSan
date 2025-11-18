using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAL
{
    public class ConnectDB
    {
        //string connectionString = "Server=localhost;Database=DACN_QuanLyKhachSan;User Id=sa;Password=123;";
        string connectionString = "Server=localhost;Database=DACN_QLKhachSan;User Id=sa;Password=123;";

        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể kết nối cơ sở dữ liệu: " + ex.Message);
            }
            return conn;
        }

        // Thực thi lệnh không trả về kết quả (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        // Truy vấn dữ liệu trả về một giá trị đơn (COUNT, MAX, MIN,...)
        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        // Truy vấn dữ liệu và trả về DataTable
        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        // Truy vấn và trả về danh sách đối tượng
        public List<T> ExecuteQueryToList<T>(string query, Func<DataRow, T> map, params SqlParameter[] parameters)
        {
            List<T> list = new List<T>();
            DataTable dt = ExecuteQuery(query, parameters);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(map(row));
            }
            return list;
        }

        // Thực thi một câu lệnh INSERT và lấy ID mới
        public int ExecuteInsertAndGetId(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                // Thực thi câu lệnh INSERT
                cmd.ExecuteNonQuery();

                // Trả về ID vừa được tạo
                cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Thực thi các câu lệnh trong một transaction
        public void ExecuteTransaction(List<string> queries, List<SqlParameter[]> parametersList)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    for (int i = 0; i < queries.Count; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand(queries[i], conn, transaction))
                        {
                            if (parametersList[i] != null)
                                cmd.Parameters.AddRange(parametersList[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
