using AuntAlenciasCollection.Commons;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuntAlenciasCollection.Models
{
    public class Data : Model
    {
        public List<DataRow> rows { get; private set; }

        public class DataRow: ModelRow
        {
            private string _character { get; set; }
            public string character
            {
                get { return _character; }
            }
            public void setCharacter(string character)
            {
                this._character = character;
            }

            private byte[] _picture { get; set; }
            public byte[] picture
            {
                get { return _picture; }
            }
            public void setPicture(byte[] picture)
            {
                this._picture = picture;
            }

            private string _url { get; set; }
            public string url
            {
                get { return _url; }
            }
            public void setUrl(string url)
            {
                this._url = url;
            }
        }
        
        public Data()
        {
            this.rows = new List<DataRow>();
        }

        public override Result load(MySqlDataReader reader)
        {
            Result result = new Result();
            try
            {
                while (reader.Read())
                {
                    DataRow row = new DataRow();

                    result.success = Int32.TryParse(reader["id"].ToString(), out var id);
                    if (!result.success)
                    {
                        result.message = "Error converting id";
                        return result;
                    }
                    row.setID(id);

                    row.setCharacter(reader["e7character"].ToString());
                    row.setPicture((byte[])reader["picture"]);
                    row.setUrl(reader["url"].ToString());

                    rows.Add(row);
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
