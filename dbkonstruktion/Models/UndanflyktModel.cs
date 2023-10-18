using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace dbkonstruktion.Models
{
	public class UndanflyktModel
	{
        private IConfiguration _configuration;
        private string connectionString;

        public UndanflyktModel(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["ConnectionString"];
        }


        public DataTable GetUndanflykter()
        {
            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM undanflykter;", dbcon);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "result");
            DataTable customerTable = ds.Tables["result"];
            dbcon.Close();

            return customerTable;
        }
    }
}

