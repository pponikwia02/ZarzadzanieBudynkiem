using System.ComponentModel.DataAnnotations;

namespace IO.MainApp
{
    public class Sala
    {
        [Key]
        public int IdSali { get; set; }
        public required string NrSali { get; set; }
        public required string Rezerwujacy { get; set; }
        public required string Od { get; set; }
        public required string Do { get; set; }
    }
}
