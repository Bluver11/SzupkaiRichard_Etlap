using MySql.Data.MySqlClient;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
	public class EtelekServices
	{
		MySqlConnection connection;

		public EtelekServices()
		{
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Server = "Localhost";
			builder.Port = 3306;
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "etlapdb";

			connection = new MySqlConnection(builder.ConnectionString);
		}
		public List<Etelek> GetAll()
		{
			List<Etelek> etelek = new List<Etelek>();
			OpenConnection();
			string sql = "SELECT * FROM etlap";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					Etelek etel = new Etelek();
					etel.Id = reader.GetInt32("id");
					etel.Nev = reader.GetString("nev");
					etel.Leiras = reader.GetString("leiras");
					etel.Ar = reader.GetInt32("ar");
					etel.Kategoria = reader.GetString("kategoria");
					etelek.Add(etel);

				}
			}

			ClosedConnection();
			return etelek;


		}




		private void ClosedConnection()
		{
			if (connection.State == System.Data.ConnectionState.Open)
			{
				connection.Close();
			}
		}

		private void OpenConnection()
		{
			if (connection.State != System.Data.ConnectionState.Open)
			{
				connection.Open();
			}
		}
	}
}
