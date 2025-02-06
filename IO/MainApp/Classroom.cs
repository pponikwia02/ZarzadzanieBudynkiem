using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.MainApp
{
   public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; }       // np 123
        public int Seats { get; set; }         // opcjonalne
        public bool IsReserved { get; set; }   
        public string ReservedBy { get; set; } //Imię i nazwisko osoby rezerwującej
        public DateTime? ReservationStart { get; set; }  // od
        public DateTime? ReservationEnd { get; set; }   // do

    }
}
