using PracaMagisterska.BazaDanych;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class UczestnikGryRepozytorium
    {
        public UczestnikGry Pobierz(long id)
        {
            try
            {
                UczestnikGry rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.UczestnikGry.Where(x => x.Id == id && !x.CzyUsuniety).Single();
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<UczestnikGry> PobierzListeUczestnikow(long graId)
        {
            try
            {
                List<UczestnikGry> rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.UczestnikGry.Where(x => x.GraId == graId).ToList();
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public long? Zapisz(UczestnikGry uczestnikGry)
        {
            try
            {
                long? rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    baza.Entry(uczestnikGry).State = uczestnikGry.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = uczestnikGry.Id;
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