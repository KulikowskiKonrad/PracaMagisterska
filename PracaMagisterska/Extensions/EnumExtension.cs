using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PracaMagisterska.Extensions
{
    public static class EnumExtension
    {
        public static string PobierzOpisEnuma(this Enum wartosc)
        {
            FieldInfo fi = wartosc.GetType().GetField(wartosc.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return wartosc.ToString();
            }
        }
    }
}