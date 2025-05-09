using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAL
{
    public class ConnectDB
    {
        string connectionString = "Server=localhost;Database=BTL_QuanLyKhachSan;User Id=sa;Password=123;";

        // Để lại dòng đã Comment 
        //string connectionString = "Server=localhost;Database=QLKhachSan;User Id=sa;Password=123;";

        //string connectionString1 = "Data Source=Mr-Cuong\\SQLEXPRESS;Initial Catalog=QLKhachSan;Integrated Security=True";

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
    }
}
