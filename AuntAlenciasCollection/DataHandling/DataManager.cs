using AuntAlenciasCollection.Commons;
using AuntAlenciasCollection.DatabaseConnection;
using AuntAlenciasCollection.Models;
using MySql.Data.MySqlClient;
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
                var parameters = new List<MySqlParameter>();
                
                string query = "INSERT INTO aac.data (e7character,picture,url) VALUES(@character, @picture, @url);";

                parameters.Add(new MySqlParameter("@character", character));
                parameters.Add(new MySqlParameter("@url", url));

                var imageParameter = new MySqlParameter("@picture", MySqlDbType.LongBlob, picture.Length);
                imageParameter.Value = picture;  

                parameters.Add(imageParameter); 
                


                result = _connection.cycleAppend(query, parameters);
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

        public string loadPictureURL(string character)
        {
            Result result = new Result();
            try
            {
                Data dataTable = this.loadPicture(character);

                if (dataTable == null ||
                    dataTable.rows.Count == 0)
                {
                    result.message = "Error";
                    return null;
                }

                return dataTable.rows.Select(x=>x.url).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return null;
        }

        public byte[] loadPicturePNG(string character)
        {
            Result result = new Result();
            try
            {
                Data dataTable = this.loadPicture(character);
                
                if (dataTable == null ||
                    dataTable.rows.Count==0)
                {
                    result.message = "Error";
                    return null;
                }

                return dataTable.rows.Select(x => x.picture).FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return null;
        }

        private Data loadPicture(string character)
        {
            Result result = new Result();
            try
            {
                var parameters = new List<MySqlParameter>();

                string query = "select * from aac.data where e7character = @character limit 1";

                parameters.Add(new MySqlParameter("@character", character));

                SQLItem sql = new SQLItem(query, parameters);
                Data dataTable = new Data();

                result = _connection.cycleRead(sql, dataTable);
                if (!result.success)
                {
                    result.message = "Error";
                    return null;
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return null;
        }
    }
}
