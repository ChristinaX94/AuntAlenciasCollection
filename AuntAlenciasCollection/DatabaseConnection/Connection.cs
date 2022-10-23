using AuntAlenciasCollection.Commons;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
        private MySqlConnection connection;

        public Result cycle(string query)
        {
            Result result = new Result();
            try
            {
                result = connect();
                if (!result.success)
                {
                    return result;
                }

                result = executeQuery(query);
                if (!result.success)
                {
                    return result;
                }

                result = disconnect();
                if (!result.success)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return result;
        }

        private Result connect()
        {
            Result result = new Result();
            try
            {
                string connectionString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + username + ";password=" + password;
                connection = new MySqlConnection(connectionString);
                connection.Open();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return result;
        }

        private Result executeQuery(string query)
        {
            Result result = new Result();
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                //while (reader.Read())
                //{
                //    Console.WriteLine(reader["discordID"]);
                //}
                
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return result;
        }

        private Result disconnect()
        {
            Result result = new Result();
            try
            {
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
