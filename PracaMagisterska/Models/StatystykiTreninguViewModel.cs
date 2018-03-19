using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Models
{
    public class StatystykiTreninguViewModel
    {
        public int[] OcenyZadan { get; set; }

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public double SredniaOcen { get; set; }

        public string ImiePrzeciwnika { get; set; }

        public string NazwiskoPrzeciwnika { get; set; }
    }


}