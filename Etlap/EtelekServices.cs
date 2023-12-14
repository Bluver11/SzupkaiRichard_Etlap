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

		public bool Create(Etelek etelek)
		{
			OpenConnection();
			string sql = "INSERT INTO dolgozok(nev,nem,kor,fizetes) VALUES(@nev,@leiras,@ar,@kategoria)";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@nev", etelek.Nev);
			command.Parameters.AddWithValue("@leiras", etelek.Leiras);
			command.Parameters.AddWithValue("@ar", etelek.Ar);
			command.Parameters.AddWithValue("@kategoria", etelek.Kategoria);
			int affecRows = command.ExecuteNonQuery();

			ClosedConnection();
			return affecRows == 1;

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
		public bool UpdateAll(int emeles)
		{
			OpenConnection();
			string sql = @"UPDATE etlap 
							SET		
								ar=ar * @emeles";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@emeles", emeles);
			int affecRows = command.ExecuteNonQuery();

			ClosedConnection();
			return affecRows == 1;
		}



		public bool Delete(int id)
		{
			OpenConnection();
			string sql = "DELETE FROM etlap WHERE id=@id";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@id", id);
			int affecRows = command.ExecuteNonQuery();

			ClosedConnection();
			return affecRows == 1;
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
