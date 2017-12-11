using PracaMagisterska.BazaDanych;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public bool Usun(long id)
        {
            try
            {
                bool rezultat = false;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    Gra gra = baza.Gra.Where(x => x.Id == id).Single();
                    gra.CzyUsuniete = true;
                    rezultat = true;
                    baza.SaveChanges();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public long? Zapisz(Gra gra)
        {
            try
            {
                long? rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    baza.Entry(gra).State = gra.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = gra.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}