using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.DataBase
{
    public class Admin
    {
        
        public int id { get; set; }
        public required string login { get; set; }
        public required string password { get; set; }
    }
}
