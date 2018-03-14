using PracaMagisterska.BazaDanych;
using PracaMagisterska.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class OcenaGraczaRepozytorium
    {
        public int ZwrocMaksymalnyNrZadania(long graId)
        {
            try
            {
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    int rezultat = 0;
                    rezultat = baza.OcenaGracza.Where(x => x.UczestnikGry.GraId == graId).Max(x => x.NumerZadania);
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return 0;
            }
        }
        public long? Zapisz(OcenaGracza ocenaGracza)
        {
            try
            {
                long? rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    baza.Entry(ocenaGracza).State = ocenaGracza.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = ocenaGracza.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public List<OcenaGracza> PobierzOcenyPoUczestniku(long uczestnikId)
        {
            try
            {
                List<OcenaGracza> listaOcenGracza = new List<OcenaGracza>();
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    listaOcenGracza = baza.OcenaGracza.Where(x => x.UczestnikGryId == uczestnikId).ToList();
                }
                return listaOcenGracza;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

    }
}