using AuntAlenciasCollection.Commons;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntAlenciasCollection.Models
{
    public interface iModel
    {
        public Result load(MySqlDataReader reader);
    }
}
