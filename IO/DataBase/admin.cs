using System.ComponentModel.DataAnnotations;

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
