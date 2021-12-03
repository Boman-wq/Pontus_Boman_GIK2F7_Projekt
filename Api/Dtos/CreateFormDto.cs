using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Catalog.Dtos
{
    public record CreateFormDto
    {
        [Required]
        public string Kurs { get; set; }
        
        [Required]
        public string Grupp { get ; set; }

        [Required]
        public string Moment { get; set; }

        [Required]
        public string Lärare { get; set; }
        
        [Required]
        public string Lokal { get; set; }

        [Required]
        public string Information { get; set; }

        [Required]
        public DateTime StartTid { get; set; }

        [Required]
        public DateTime SlutTid { get; set; }
        public string Bild { get; set; }
    }
}