using PracaMagisterska.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Models
{
    public class StatystykiGryViewModel
    {
        public StatystykiGry StatystykiGry { get; set; }

        public StatystykiGry StatystykiPoprzedniejGry { get; set; }
    }

    public class StatystykiGry
    {
        public long Id { get; set; }

        public TypGry TypGry { get; set; }

        public DateTime Data { get; set; }

        public int IloscRund { get; set; }

        public List<StatystykiGracza> ListaStatystykGracza { get; set; }
    }

    public class StatystykiGracza
    {
        public int[] OcenyZadan { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public double SredniaOcen { get; set; }

        public string ImiePrzeciwnika { get; set; }

        public string NazwiskoPrzeciwnika { get; set; }

    }
}