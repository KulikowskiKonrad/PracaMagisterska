using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Models
{
    public class EdytujNotatkeViewModel
    {
        public long? Id { get; set; }

        [Display(Name = "Treść")]
        [Required(ErrorMessage = "Niepoprawna ilość znaków"), MinLength(1)]
        public string Tresc { get; set; }

        [Required(ErrorMessage = "Niepoprawna ilość znaków")]
        [StringLength(80, ErrorMessage = "Niepoprawna ilość znaków", MinimumLength = 3)]
        public string Temat { get; set; }

        public DateTime DataDodania { get; set; }

        public bool CzyUsuniete { get; set; }

        public long UzytkownikId { get; set; }
    }
}