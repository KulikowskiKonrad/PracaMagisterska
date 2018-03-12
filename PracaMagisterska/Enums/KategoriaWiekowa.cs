using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Enums
{
    public enum KategoriaWiekowa
    {
        Junior = 1,

        [Description("Młodzieżowiec")]
        Mlodziezowiec = 2,

        Senior = 3,

        Weteran = 4,

        Open = 5
    }
}