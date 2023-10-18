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
			MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM kampanj WHERE CAST(nr AS CHAR) LIKE @inputname;", dbcon);
			adapter.SelectCommand.Parameters.AddWithValue("@inputname", "%" + kampanjID + "%");
			DataSet ds = new DataSet();
			adapter.Fill(ds, "result");
			DataTable customerTable = ds.Tables["result"];
			dbcon.Close();

			return customerTable;
		}

        public void CreateKampanj(int kampanjID, string sldatum, string kommentar, string undanflykt)
        {
            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();

            MySqlCommand sqlCmd = new MySqlCommand("CALL skapaKampanj(@sldatum, @kampanjID);", dbcon);
            sqlCmd.Parameters.AddWithValue("@kampanjID", kampanjID);
            sqlCmd.Parameters.AddWithValue("@sldatum", sldatum);
            sqlCmd.ExecuteNonQuery();

            MySqlCommand sqlCmd2 = new MySqlCommand("insert into kampanjUndanflykter(undanflykt, kampanj) values (@undanflykt, @kampanjID);", dbcon);
            sqlCmd2.Parameters.AddWithValue("@kampanjID", kampanjID);
            sqlCmd2.Parameters.AddWithValue("@undanflykt", undanflykt);
            sqlCmd2.ExecuteNonQuery();

            MySqlCommand sqlCmd3 = new MySqlCommand("insert into kampanjdetaljer(kampanjNr, succesrate, kommentar) values (@kampanjID, 0.0, @kommentar);", dbcon);
            sqlCmd3.Parameters.AddWithValue("@kampanjID", kampanjID);
            sqlCmd3.Parameters.AddWithValue("@kommentar", kommentar);
            sqlCmd3.ExecuteNonQuery();

            dbcon.Close();

        }

        public void ArkiveraKampanj(int kampanjID)
        {
            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();

            MySqlCommand sqlCmd = new MySqlCommand("CALL arkiveraKampanj(@kampanjID);", dbcon);
            sqlCmd.Parameters.AddWithValue("@kampanjID", kampanjID);
            sqlCmd.ExecuteNonQuery();
        }
    }
}

