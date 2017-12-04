using PracaMagisterska.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracaMagisterska.Models
{
    public class EdytujUzytkownikaViewModel
    {
        [Required(ErrorMessage ="Pole wymagane")]
        [Display(Name = "Hasło")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
        public string Haslo { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Haslo", ErrorMessage = "Niezgodne hasła")]
        [Display(Name = "Powtórz hasło")]
        public string PowtorzoneHaslo { get; set; }

    }
}