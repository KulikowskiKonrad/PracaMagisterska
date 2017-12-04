using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Enums
{
    public enum PozycjaGracza
    {
        [Description("Wybijający")]
        Wybijajacy = 1,
        [Description("Rzucający")]
        Rzucajacy = 2
    }
}