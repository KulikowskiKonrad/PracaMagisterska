using PracaMagisterska.BazaDanych;
using PracaMagisterska.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class UzytkownikRepozytorium
    {
        public Uzytkownik Pobierz(string login, string haslo)
        {
            try
            {
                Uzytkownik rezultat = null;

                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Uzytkownik.Where(x => x.Login == login && x.CzyUsuniety == false).SingleOrDefault();
                }
                if (rezultat != null)
                {
                    string hasloZakodowane = MD5Helper.GenerujMD5(haslo + rezultat.Sol);
                    if (hasloZakodowane != rezultat.Haslo)
                    {
                        rezultat = null;
                    }
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }
        public Uzytkownik Pobierz(string login)
        {
            try
            {
                Uzytkownik rezultat = null;

                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Uzytkownik.Where(x => x.Login == login && x.CzyUsuniety == false).SingleOrDefault();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public Uzytkownik Pobierz(long uzytkownikId)
        {
            try
            {
                Uzytkownik rezultat = null;

                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Uzytkownik.Where(x => x.Id == uzytkownikId).Single();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Zapisz(Uzytkownik uzytkownik)
        {
            try
            {
                long? rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    baza.Entry(uzytkownik).State = uzytkownik.Id > 0 ? EntityState.Modified : EntityState.Added;  // te linie odpowiadaja liniom ponizej; 
                    baza.SaveChanges();
                    rezultat = uzytkownik.Id;
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