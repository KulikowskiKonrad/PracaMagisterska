using PracaMagisterska.BazaDanych;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class KlubRepozytorium
    {
        public List<Klub> PobierzWszystkie()
        {
            try
            {
                List<Klub> rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Klub.ToList();
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}