using PracaMagisterska.BazaDanych;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class OcenaGraczaRepozytorium
    {

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
                return null;
            }
        }

    }
}