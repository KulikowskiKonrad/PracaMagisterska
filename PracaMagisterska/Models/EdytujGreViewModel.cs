using PracaMagisterska.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracaMagisterska.Extensions;

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

        public EdytujGreViewModel()
        {
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
}