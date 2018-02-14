using PracaMagisterska.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Models.Api
{
    public class StatystkiNajlepszychGraczyModel
    {

        public StatystykiGracza NajlepszyStrzelec { get; set; }

        public StatystykiGracza NajlepszyPuenter { get; set; }

    }

    public class StatystykiGracza
    {
        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public double SredniaOcen { get; set; }

        public List<double> ListaOcen { get; set; }

        public List<string> ListaDat { get; set; }

    }
}