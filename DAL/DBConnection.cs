using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HomeStayDorm.DAL
{
    /// <summary>
    /// Lớp hỗ trợ kết nối CSDL dùng chung cho toàn bộ dự án.
    /// Tất cả các lớp DAL khác sẽ sử dụng các phương thức trong lớp này để gọi Stored Procedure.
    /// </summary>
    public class DBConnection
    {
        private readonly string _connectionString;

        public DBConnection()
        {
            // Lấy chuỗi kết nối từ file App.config (chỉ cần sửa 1 chỗ khi đổi máy)
            _connectionString = ConfigurationManager.ConnectionStrings["HomeStayDormDB"].ConnectionString;
        }

        /// <summary>
        /// Tạo và trả về đối tượng SqlConnection.
        /// </summary>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Thực thi Stored Procedure dạng Insert/Update/Delete.
        /// </summary>
        /// <param name="spName">Tên Stored Procedure</param>
        /// <param name="parameters">Mảng các SqlParameter (có thể null nếu không có tham số)</param>
        /// <returns>Số dòng bị ảnh hưởng</returns>
        public int ExecuteNonQuery(string spName, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Thực thi Stored Procedure dạng Select (lấy dữ liệu danh sách).
        /// </summary>
        /// <param name="spName">Tên Stored Procedure</param>
        /// <param name="parameters">Mảng các SqlParameter (có thể null nếu không có tham số)</param>
        /// <returns>DataTable chứa kết quả, dễ dàng dùng cho DataGridView WinForms</returns>
        public DataTable ExecuteQuery(string spName, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt); // Tự động mở và đóng kết nối
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// Thực thi Stored Procedure dạng Select một giá trị đơn (ví dụ: COUNT(*), SUM, ID vừa thêm).
        /// </summary>
        /// <param name="spName">Tên Stored Procedure</param>
        /// <param name="parameters">Mảng các SqlParameter (có thể null nếu không có tham số)</param>
        /// <returns>Giá trị trả về dạng object (cần cast sang kiểu mong muốn)</returns>
        public object ExecuteScalar(string spName, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
