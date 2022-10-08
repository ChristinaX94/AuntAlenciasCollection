using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuntAlenciasCollection.Commons
{
    public class Result
    {
        public bool success;
        public string message;

        public Result(bool success = false, string message = "")        
        {
            this.success = success;
            this.message = message;
        }   

    }
}
