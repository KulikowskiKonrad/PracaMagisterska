using PracaMagisterska.BazaDanych;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class GraRepozytorium
    {

        public List<Gra> PobierzWszystkie()
        {
            try
            {
                List<Gra> listaGier = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    listaGier = baza.Gra.Where(x => !x.CzyUsuniete).ToList();
                }
                return listaGier;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Gra Pobierz(long id)
        {
            try
            {
                Gra rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Gra.Where(x => x.Id == id).Single();
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