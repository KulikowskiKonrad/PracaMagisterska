using PracaMagisterska.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Models
{
    public class StatystykiViewModel
    {
        public StatystykiViewModel()
        {
            ListaProponowanychZawodnikow = new List<StatystykiZawodnika>();
        }

        [Display(Name = "Data początkowa")]
        public DateTime DataOd { get; set; }

        [Display(Name = "Data końcowa")]
        public DateTime DataDo { get; set; }

        public List<StatystykiZawodnika> ListaStatystykZawodnikow { get; set; }

        [Range(0, 3, ErrorMessage = "Niepoprawna wartość")]
        [Display(Name = "Ilość puenterów")]
        public byte? IloscPuenterow { get; set; }

        [Range(0, 3, ErrorMessage = "Niepoprawna wartość")]
        [Display(Name = "Ilość strzelców")]
        public byte? IloscStrzelcow { get; set; }

        public List<StatystykiZawodnika> ListaProponowanychZawodnikow { get; set; }
    }


    public class StatystykiZawodnika
    {

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public double SredniaOcen { get; set; }

        public double IloscSpotkan { get; set; }

        public PozycjaGracza Pozycja { get; set; }

    }
}