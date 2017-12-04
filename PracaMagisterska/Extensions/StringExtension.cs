using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Extensions
{
    public static class StringExtension
    {
        public static string UsunWszystkieSpacje(this string value)
        {
            return value.Replace(" ", "");
        }
    }
}