using PracaMagisterska.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracaMagisterska.Extensions;
using PracaMagisterska.BazaDanych;
using PracaMagisterska.Repozytoria;

namespace PracaMagisterska.Models
{
    public class EdytujGreViewModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Typ gry")]
        public TypGry TypGry { get; set; }

        [StringLength(200, ErrorMessage = "Niepoprawna ilość znaków", MinimumLength = 3)]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Miejsce { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Termin")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Data { get; set; }

        public List<SelectListItem> ListaTypowGry { get; set; }

        public List<SelectListItem> ListaGraczy { get; set; }

        public List<UczestnikGryViewModel> ListaUczestnikow { get; set; }

        public EdytujGreViewModel()
        {
            ListaUczestnikow = new List<UczestnikGryViewModel>();
            ListaUczestnikow.Add(new UczestnikGryViewModel());
            ListaUczestnikow.Add(new UczestnikGryViewModel());

            ListaGraczy = new List<SelectListItem>();
            GraczRepozytorium graczRepozytorium = new GraczRepozytorium();
            List<Gracz> pobraniGracze = graczRepozytorium.PobierzWszystkich();
            foreach (Gracz gracz in pobraniGracze)
            {
                ListaGraczy.Add(new SelectListItem()
                {
                    Text = gracz.Imie.ToString() + " " + gracz.Nazwisko.ToString(),
                    Value = gracz.Id.ToString()
                });
            }
            ListaTypowGry = new List<SelectListItem>();
            ListaTypowGry.Add(new SelectListItem()
            {
                Text = TypGry.Inny.ToString(),
                Value = TypGry.Inny.ToString()
            });
            ListaTypowGry.Add(new SelectListItem()
            {
                Text = TypGry.MistrzostwaPolski.PobierzOpisEnuma(),
                Value = TypGry.MistrzostwaPolski.ToString()
            });
            ListaTypowGry.Add(new SelectListItem()
            {
                Text = TypGry.PucharPolski.PobierzOpisEnuma(),
                Value = TypGry.PucharPolski.ToString()
            });
            ListaTypowGry.Add(new SelectListItem()
            {
                Text = TypGry.Rankingowy.ToString(),
                Value = TypGry.Rankingowy.ToString()
            });
            ListaTypowGry.Add(new SelectListItem()
            {
                Text = TypGry.Trening.ToString(),
                Value = TypGry.Trening.ToString()
            });
        }
    }
    public class UczestnikGryViewModel
    {

        public long? Id { get; set; }

        [Display(Name = "Imię przeciwnika")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Niepoprawna ilość znaków")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string ImiePrzeciwnika { get; set; }

        [Display(Name = "Nazwisko przeciwnika")]
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
        public string NazwiskoPrzeciwnika { get; set; }

        [Display(Name = "Gracz")]
        [Required(ErrorMessage = "Pole wymagane")]
        public long? GraczId { get; set; }
    }
}