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
        Wybijajacy = 1,
        [Description("Strzelec")]
        Rzucajacy = 2
    }
}