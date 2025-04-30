using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.DataBase
{
    public class AppUser
    {
        [Key]
        public int id { get; set; }
        public required string login { get; set; }
        public required string password { get; set; }
        public int UserType { get; set; } //0-wykładowca, 1- Admin

    }
}
