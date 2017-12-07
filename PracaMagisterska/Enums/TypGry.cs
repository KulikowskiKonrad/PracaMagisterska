using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Enums
{
    public enum TypGry
    {
        Trening = 1,
        [Description("Mistrzostwa Polski")]
        MistrzostwaPolski = 2,

        [Description("Puchar Polski")]
        PucharPolski = 3,
        Rankingowy = 4,
        Inny = 5
    }
}