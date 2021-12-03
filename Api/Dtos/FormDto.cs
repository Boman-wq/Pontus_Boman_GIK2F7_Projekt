using System;
using Microsoft.AspNetCore.Http;

namespace Catalog.Dtos
{
    public record FormDto
    {
        public Guid Id { get; init; }
        public string Kurs { get; set; }
        public string Grupp { get ; set; }
        public string Moment { get; set; }
        public string Lärare { get; set; }
        public string Lokal { get; set; }
        public string Information { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public string Bild { get; set; }
    }
}