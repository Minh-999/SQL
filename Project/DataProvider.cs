using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffee_Cub_Management
{
    class DataProvider
    {
        private string connectSource = @"Data Source=DESKTOP-ELSJEOJ\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

        public DataTable ExecuteQuery(string query)
        {
            SqlConnection connect = new SqlConnection(connectSource);

            connect.Open();
            SqlCommand command = new SqlCommand(query, connect);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            connect.Close();

            return data;
        }

        public int ExecuteNonQuery(string query)
        {
            int data = 0;
            SqlConnection connect = new SqlConnection(connectSource);

            connect.Open();
            SqlCommand command = new SqlCommand(query, connect);
            data = command.ExecuteNonQuery();
            connect.Close();

            return data;
        }

        public SqlDataReader ExecuteReader(string query)
        {
            SqlConnection connect = new SqlConnection(connectSource);

            connect.Open();
            SqlCommand command = new SqlCommand(query, connect);

            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }

        public object ExecuteScalar(string query)
        {
            object data = 0;

            SqlConnection connect = new SqlConnection(connectSource);

            connect.Open();
            SqlCommand command = new SqlCommand(query, connect);

            data = command.ExecuteScalar();
            connect.Close();

            return data;
        }
    }
}


