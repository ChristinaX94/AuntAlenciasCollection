using AuntAlenciasCollection.Commons;
using AuntAlenciasCollection.DataHandling;
using AuntAlenciasCollection.Models;
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

        public Result cycleAppend(string query, List<MySqlParameter> parameters = null)
        {
            Result result = new Result();
            try
            {
                result = connect();
                if (!result.success)
                {
                    return result;
                }

                result = executeQuery(query, parameters);
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

        public Result cycleRead(SQLItem sql, Model model)
        {
            Result result = new Result();
            try
            {
                result = connect();
                if (!result.success)
                {
                    result.message = "Error";
                    return result;
                }

                var obj = executeReadQuery(sql, model);
                if (obj == null)
                {
                    result.message = "Error";
                    return result;
                }

                result = disconnect();
                if (!result.success)
                {
                    result.message = "Error";
                    return result;
                }

                return result;
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

        private Result executeQuery(string query, List<MySqlParameter> parameters = null)
        {
            Result result = new Result();
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                if(parameters!=null && parameters.Count > 0)
                {
                    foreach(var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                cmd.ExecuteNonQuery();

                //MySqlDataReader reader = cmd.ExecuteReader();
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

        private object executeReadQuery(SQLItem sql, Model model)
        {
            Result result = new Result();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql.query, connection);

                if (sql.parameters != null && sql.parameters.Count > 0)
                {
                    foreach (var parameter in sql.parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();
                result = model.load(reader);
                if (!result.success)
                {
                    return result;
                }

                result.success = true;

            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return result.message;
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
