using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Models
{
    public class Lesson
    {
        public string Id { get; set; }
        public string Kurs { get; set; }
        public string Grupp { get; set; }
        public string Moment { get; set; }
        public string Lärare { get; set; }
        public string Lokal { get; set; }
        public string Information { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public string Bild { get; set; }
        public string BildPath { get; set; }
        public string PStartTid { get; set; }
        public string PSlutTid { get; set; }
    }

}

