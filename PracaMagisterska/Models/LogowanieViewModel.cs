using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Models
{
    public class LogowanieViewModel
    {
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
        [Display(Name ="Hasło")]
        public string Haslo { get; set; }

        [Display(Name ="Zapamiętaj mnie")]
        public bool ZapamietajMnie { get; set; }

    }
}