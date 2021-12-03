using Catalog.Dtos;
using Catalog.Models;

namespace Catalog
{
    public static class Extenstions
    {
        public static FormDto AsDto(this Form form)
        {
            return new FormDto{
                Id = form.Id,
                Kurs = form.Kurs,
                Grupp = form.Grupp,
                Moment = form.Moment,
                Lärare = form.Lärare,
                Lokal = form.Lokal,
                Information = form.Information,
                StartTid = form.StartTid,
                SlutTid = form.SlutTid,
                Bild = form.Bild
            };
        }
    }
}