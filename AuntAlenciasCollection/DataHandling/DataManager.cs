using AuntAlenciasCollection.Commons;
using AuntAlenciasCollection.DatabaseConnection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntAlenciasCollection.DataHandling
{
    public class DataManager
    {
        private Connection _connection { get; set; }

        public DataManager()
        {
            _connection = new Connection();
        }

        public Result savePicture(string character, byte[] picture, string url)
        {
            Result result = new Result();
            try
            {
                string query = "INSERT INTO `aac`.`data`\r\n" +
                    "(`character`,\r\n`png`,\r\n`url`)\r\n" +
                    "VALUES\r\n('" + character + "',\r\n" + picture.ToString() + ",\r\n'" + url + "');";

                result = _connection.cycle(query);
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
    }
}
