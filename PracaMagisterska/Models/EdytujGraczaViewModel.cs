using PracaMagisterska.BazaDanych;
using PracaMagisterska.Enums;
using PracaMagisterska.Repozytoria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracaMagisterska.Extensions;

namespace PracaMagisterska.Models
{
    public class EdytujGraczaViewModel
    {
        public long? Id { get; set; }

        [Display(Name = "Imię")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Niepoprawna ilość znaków")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
        public string Nazwisko { get; set; }

        [Display(Name = "Numer licencji")]
        public string NrLicencji { get; set; }

        [Display(Name = "Klub")]
        public long? KlubId { get; set; }

        public List<SelectListItem> ListaKlubow { get; set; }

        public PozycjaGracza Pozycja { get; set; }

        public List<SelectListItem> ListaPozycji { get; set; }

        public EdytujGraczaViewModel()
        {
            ListaKlubow = new List<SelectListItem>();
            KlubRepozytorium klubRepozytorium = new KlubRepozytorium();
            List<Klub> pobraneKluby = klubRepozytorium.PobierzWszystkie();
            foreach (Klub klub in pobraneKluby)
            {
                ListaKlubow.Add(new SelectListItem()
                {
                    Value = klub.Id.ToString(),
                    Text = klub.Nazwa
                });
            }

            ListaPozycji = new List<SelectListItem>();
            ListaPozycji.Add(new SelectListItem()
            {
                Text = PozycjaGracza.Puenter.PobierzOpisEnuma(),
                Value = ((byte)PozycjaGracza.Puenter).ToString()
            });
            ListaPozycji.Add(new SelectListItem()
            {
                Text = PozycjaGracza.Strzelec.PobierzOpisEnuma(),
                Value = ((byte)PozycjaGracza.Strzelec).ToString()
            });
        }
    }
}