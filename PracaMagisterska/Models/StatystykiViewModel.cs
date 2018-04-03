using PracaMagisterska.Enums;
using PracaMagisterska.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracaMagisterska.Models
{
    public class StatystykiViewModel
    {
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

        [Display(Name = "Płeć")]
        public PlecGracza? Plec { get; set; }

        public List<SelectListItem> ListaPlci { get; set; }

        public List<StatystykiZawodnika> ListaProponowanychZawodnikow { get; set; }

        public List<SelectListItem> ListaKategoriiWiekowych { get; set; }

        [Display(Name = "Kategoria wiekowa")]
        public KategoriaWiekowa? KategoriaWiekowaGraczy { get; set; }

        public StatystykiViewModel()
        {
            ListaProponowanychZawodnikow = new List<StatystykiZawodnika>();
            ListaPlci = new List<SelectListItem>();
            //ListaPlci.Add(new SelectListItem()
            //{
            //    Text = "Dowolna",
            //    Value = ""
            //});
            ListaPlci.Add(new SelectListItem()
            {
                Text = PlecGracza.Mezczyzna.PobierzOpisEnuma(),
                Value = PlecGracza.Mezczyzna.ToString()
            });
            ListaPlci.Add(new SelectListItem()
            {
                Text = PlecGracza.Kobieta.PobierzOpisEnuma(),
                Value = PlecGracza.Kobieta.ToString()
            });

            ListaKategoriiWiekowych = new List<SelectListItem>();
            ListaKategoriiWiekowych.Add(new SelectListItem()
            {
                Text = KategoriaWiekowa.Junior.ToString(),
                Value = KategoriaWiekowa.Junior.ToString()
            });
            ListaKategoriiWiekowych.Add(new SelectListItem()
            {
                Text = KategoriaWiekowa.Mlodziezowiec.ToString(),
                Value = KategoriaWiekowa.Mlodziezowiec.ToString()
            });
            ListaKategoriiWiekowych.Add(new SelectListItem()
            {
                Text = KategoriaWiekowa.Open.ToString(),
                Value = KategoriaWiekowa.Open.ToString()
            });
            ListaKategoriiWiekowych.Add(new SelectListItem()
            {
                Text = KategoriaWiekowa.Senior.ToString(),
                Value = KategoriaWiekowa.Senior.ToString()
            });
            ListaKategoriiWiekowych.Add(new SelectListItem()
            {
                Text = KategoriaWiekowa.Weteran.ToString(),
                Value = KategoriaWiekowa.Weteran.ToString()
            });
        }
    }

    public class StatystykiZawodnika
    {

        public string Imie { get; set; }

        public string Nazwisko { get; set; }

        public double SredniaOcen { get; set; }

        public double IloscSpotkan { get; set; }

        public PozycjaGracza Pozycja { get; set; }

        public PlecGracza Plec { get; set; }

        public KategoriaWiekowa KategoriaWiekowaGraczy { get; set; }

    }
}