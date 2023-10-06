using MySql.Data.MySqlClient;
using System.Data;

namespace dbkonstruktion.Models
{
	public class KampanjModel
	{
		private IConfiguration _configuration;
		private string connectionString;

		public KampanjModel(IConfiguration configuration)
		{
			_configuration = configuration;
			connectionString = _configuration["ConnectionString"];
		}

		public DataTable GetKampanjer(int kampanjID)
		{
			MySqlConnection dbcon = new MySqlConnection(connectionString);
			dbcon.Open();
			MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM kampanj WHERE nr=@inputname;", dbcon);
			adapter.SelectCommand.Parameters.AddWithValue("@inputname", kampanjID);
			DataSet ds = new DataSet();
			adapter.Fill(ds, "result");
			DataTable customerTable = ds.Tables["result"];
			dbcon.Close();

			return customerTable;
		}
	}
}

