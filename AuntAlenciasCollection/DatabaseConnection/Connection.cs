using AuntAlenciasCollection.Commons;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntAlenciasCollection.DatabaseConnection
{
    public class Connection
    {
        private string server = "localhost";
        private string database = "AAC";
        private string username = "alencia";
        private string password = "4unt!@CUT3";
        
        public Result connect()
        {
            Result result = new Result();
            try
            {
                string connectionString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + username + ";password=" + password;
                MySqlConnection connection =  new MySqlConnection(connectionString);
                connection.Open();

                string query = "select * from user";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["discordID"]);
                }

                connection.Close();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return result;
        }
    }
}
