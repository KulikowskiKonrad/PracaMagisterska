using PracaMagisterska.BazaDanych;
using PracaMagisterska.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class NotatkaRepozytorium
    {

        public List<Notatka> PobierzWszystkie(long uzytkownikId)
        {
            try
            {
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    List<Notatka> rezultat = null;
                    rezultat = baza.Notatka.Where(x => x.CzyUsuniete == false && x.UzytkownikId == uzytkownikId).OrderByDescending(x => x.DataDodania).ToList();
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public bool Usun(long id)
        {
            bool rezultat = false;
            try
            {
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    Notatka notatka = null;
                    notatka = baza.Notatka.Where(x => x.Id == id).Single();
                    notatka.CzyUsuniete = true;
                    baza.SaveChanges();
                    rezultat = true;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Notatka Pobierz(long id)
        {
            try
            {
                Notatka rezultat = null;

                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Notatka.Where(x => x.Id == id).Single();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public long? Zapisz(Notatka notatka)
        {
            try
            {
                long? rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    baza.Entry(notatka).State = notatka.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = notatka.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }


    }
}