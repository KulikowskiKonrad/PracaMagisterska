using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Models
{
    public class StatystykiViewModel
    {
        public DateTime DataOd { get; set; }

        public DateTime DataDo { get; set; }

        public List<StatystykiZawodnika> ListaStatystykZawodnikow { get; set; }

    }

    public class StatystykiZawodnika
    {

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public double SredniaOcen { get; set; }

        public double IloscSpotkan { get; set; }
    }
}