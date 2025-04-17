using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
