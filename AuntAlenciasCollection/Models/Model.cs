using AuntAlenciasCollection.Commons;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntAlenciasCollection.Models
{
    public abstract class Model : iModel
    {
        public class ModelRow
        {
            private int _ID { get; set; }
            public int ID
            {
                get { return _ID; }
            }
            public void setID(int ID)
            {
                this._ID = ID;
            }
        }

        public abstract Result load(MySqlDataReader reader);

    }
}
