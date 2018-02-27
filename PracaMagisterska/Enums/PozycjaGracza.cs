using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Enums
{
    public enum PozycjaGracza
    {
        [Description("Puenter")]
        Puenter = 1,
        [Description("Strzelec")]
        Strzelec = 2
    }
}