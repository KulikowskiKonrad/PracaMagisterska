using PracaMagisterska.BazaDanych;
using PracaMagisterska.Enums;
using PracaMagisterska.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class GraRepozytorium
    {

        public List<StatystykiZawodnika> PobierzStatytstyki(DateTime dataOd, DateTime dataDo)
        {
            try
            {
                List<StatystykiZawodnika> listaStatystyk = new List<StatystykiZawodnika>();
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    List<Gra> listaGier = baza.Gra.Where(g => g.Data >= dataOd
                                && g.Data <= dataDo && g.Typ != (byte)TypGry.Trening && g.CzyUsuniete == false)
                            .ToList();
                    List<Gracz> listaGraczy = baza.Gracz.Where(g => g.UczestnicyGry.Where(u => u.Gra.Data >= dataOd
                              && u.Gra.Data <= dataDo && u.Gra.Typ != (byte)TypGry.Trening && u.CzyUsuniety == false && u.Gra.CzyUsuniete == false).Any()
                              && g.CzyUsuniety == false)
                        .ToList();
                    List<OcenaGracza> listaOcen = baza.OcenaGracza.Where(o => o.UczestnikGry.Gra.Data >= dataOd && o.UczestnikGry.Gra.Data <= dataDo
                        && o.UczestnikGry.Gra.Typ != (byte)TypGry.Trening && o.UczestnikGry.CzyUsuniety == false && o.UczestnikGry.Gra.CzyUsuniete == false).ToList();
                    foreach (Gracz gracz in listaGraczy)
                    {
                        {
                            StatystykiZawodnika statystykiZawodnika = new StatystykiZawodnika()
                            {
                                Imie = gracz.Imie,
                                Nazwisko = gracz.Nazwisko,
                                IloscSpotkan = listaGier.Where(x => x.UczestnicyGry.Where(y => y.GraczId == gracz.Id).Any()).ToList().Count,
                                Pozycja = (PozycjaGracza)gracz.Pozycja,
                                //wez liste ocen danego gracza , pobierz wszystkie id gier i  usun ich powtorzenia(distinct) dodaj na liste i zlicz
                                SredniaOcen = Math.Round(listaOcen.Where(x => x.UczestnikGry.GraczId == gracz.Id).Average(x => (byte?)x.Ocena).GetValueOrDefault(0), 2) //wez liste ocen danego gracza i wylicz srednia z ocen
                            };
                            listaStatystyk.Add(statystykiZawodnika);
                        }
                    }
                    return listaStatystyk.OrderByDescending(x => x.SredniaOcen).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public List<StatystykiTreninguViewModel> PobierzListeStatystykTreningu(long idGry)
        {
            try
            {
                List<StatystykiTreninguViewModel> listaStatystyk = new List<StatystykiTreninguViewModel>();
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    List<UczestnikGry> uczestnicy = baza.UczestnikGry.Where(u => u.GraId == idGry).ToList();
                    List<OcenaGracza> oceny = baza.OcenaGracza.Where(o => o.UczestnikGry.GraId == idGry).ToList();
                    foreach (UczestnikGry uczestnik in uczestnicy)
                    {
                        StatystykiTreninguViewModel statystykaGracza = new StatystykiTreninguViewModel()
                        {
                            Imie = uczestnik.Gracz.Imie,
                            Nazwisko = uczestnik.Gracz.Nazwisko,
                            ImiePrzeciwnika = uczestnik.ImiePrzeciwnika,
                            NazwiskoPrzeciwnika = uczestnik.NazwiskoPrzeciwnika,
                            OcenyZadan = new int[5]
                        };

                        foreach (OcenaGracza ocena in oceny.Where(o => o.UczestnikGryId == uczestnik.Id).ToList())
                        {
                            statystykaGracza.OcenyZadan[ocena.NumerZadania - 1] = ocena.Ocena;
                        }

                        statystykaGracza.SredniaOcen = Math.Round(statystykaGracza.OcenyZadan.Where(o => o != 0).DefaultIfEmpty().Average(), 2);
                        listaStatystyk.Add(statystykaGracza);
                    }
                }
                return listaStatystyk.OrderByDescending(x => x.SredniaOcen).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Gra> PobierzWszystkie(long uzytkownikId)
        {
            try
            {
                List<Gra> listaGier = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    listaGier = baza.Gra.Include(x => x.UczestnicyGry).Where(x => !x.CzyUsuniete && x.UzytkownikId == uzytkownikId).ToList();
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
                    rezultat = baza.Gra.Include(x => x.UczestnicyGry).Where(x => x.Id == id).Single();
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