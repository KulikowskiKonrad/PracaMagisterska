using PracaMagisterska.BazaDanych;
using PracaMagisterska.Enums;
using PracaMagisterska.Helpers;
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

        public List<StatystykiZawodnika> PobierzStatytstyki(DateTime dataOd, DateTime dataDo, PlecGracza? plecGracza, KategoriaWiekowa? kategoriaWiekowa, long? klubId,
                    long uzytkownikId)
        {
            try
            {
                List<StatystykiZawodnika> listaStatystyk = new List<StatystykiZawodnika>();
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    List<Gra> listaGier = baza.Gra.Where(g => g.Data >= dataOd
                                && g.Data <= dataDo && g.Typ != (byte)TypGry.Trening && g.CzyUsuniete == false && g.UzytkownikId == uzytkownikId)
                            .ToList();
                    List<Gracz> listaGraczy = baza.Gracz.Where(g => g.UczestnicyGry.Where(u => u.Gra.Data >= dataOd
                              && u.Gra.Data <= dataDo && u.Gra.Typ != (byte)TypGry.Trening && u.CzyUsuniety == false && u.Gra.CzyUsuniete == false).Any()
                              && g.CzyUsuniety == false && ((PlecGracza)g.Plec == plecGracza || plecGracza == null)
                              && ((KategoriaWiekowa)g.KategoriaWiekowa == kategoriaWiekowa || kategoriaWiekowa == null)
                              && (g.KlubId == klubId || klubId == null) && g.UzytkownikId == uzytkownikId)
                        .ToList();
                    List<OcenaGracza> listaOcen = baza.OcenaGracza.Where(o => o.UczestnikGry.Gra.Data >= dataOd && o.UczestnikGry.Gra.Data <= dataDo
                        && o.UczestnikGry.Gra.Typ != (byte)TypGry.Trening && o.UczestnikGry.CzyUsuniety == false && o.UczestnikGry.Gra.CzyUsuniete == false
                        && o.UczestnikGry.Gra.UzytkownikId == uzytkownikId).ToList();

                    //switch (listaOcen.Count)
                    //{
                    //    case 0:
                    //        zrobCosGdyPusta();
                    //        break;
                    //    case 1:
                    //    case 2:
                    //        zrobCosInnegoGdy1Element();
                    //        break;
                    //    default:
                    //        zrobCosJeszczeInnego();
                    //        break;
                    //}

                    foreach (Gracz gracz in listaGraczy)
                    {
                        {
                            StatystykiZawodnika statystykiZawodnika = new StatystykiZawodnika()
                            {
                                Imie = gracz.Imie,
                                Nazwisko = gracz.Nazwisko,
                                IloscSpotkan = listaGier.Where(x => x.UczestnicyGry.Where(y => y.GraczId == gracz.Id).Any()).ToList().Count,
                                Pozycja = (PozycjaGracza)gracz.Pozycja,
                                Plec = (PlecGracza)gracz.Plec,
                                //wez liste ocen danego gracza , pobierz wszystkie id gier i  usun ich powtorzenia(distinct) dodaj na liste i zlicz
                                SredniaOcen = Math.Round(listaOcen.Where(x => x.UczestnikGry.GraczId == gracz.Id).Average(x => (byte?)x.Ocena).GetValueOrDefault(0), 2), //wez liste ocen danego gracza i wylicz srednia z ocen
                                KategoriaWiekowaGraczy = (KategoriaWiekowa)gracz.KategoriaWiekowa,
                                NazwaKlubu = gracz.KlubId.HasValue ? gracz.Klub.Nazwa : ""
                            };

                            //if (gracz.KlubId.HasValue)
                            //{
                            //    statystykiZawodnika.NazwaKlubu = ...
                            //}
                            //else
                            //{

                            //}

                            listaStatystyk.Add(statystykiZawodnika);
                        }
                    }
                    return listaStatystyk.OrderByDescending(x => x.SredniaOcen).ToList();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? PobierzIdPoprzedniejGry(byte typGry, DateTime data, long uzytkownikId)
        {
            try
            {
                long? rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    Gra gra = baza.Gra.Where(x => x.Data < data && x.Typ == typGry && x.CzyUsuniete == false && x.UzytkownikId == uzytkownikId).FirstOrDefault();
                    if (gra != null)
                    {
                        rezultat = gra.Id;
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

        public List<StatystykiGracza> PobierzListeStatystykGry(long idGry)
        {
            try
            {
                List<StatystykiGracza> listaStatystyk = new List<StatystykiGracza>();
                OcenaGraczaRepozytorium ocenaGraczaRepozytorium = new OcenaGraczaRepozytorium();

                int iloscZadan = ocenaGraczaRepozytorium.ZwrocMaksymalnyNrZadania(idGry);
                int iloscRund = ocenaGraczaRepozytorium.ZwrocMaksymalnyNrRundy(idGry);
                if (iloscZadan > 0 && iloscRund == 0)
                {
                    iloscRund = 1;
                }
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    List<UczestnikGry> uczestnicy = baza.UczestnikGry.Where(u => u.GraId == idGry).ToList();
                    List<OcenaGracza> oceny = baza.OcenaGracza.Where(o => o.UczestnikGry.GraId == idGry).ToList();
                    foreach (UczestnikGry uczestnik in uczestnicy)
                    {
                        StatystykiGracza statystykaGracza = new StatystykiGracza()
                        {
                            Imie = uczestnik.Gracz.Imie,
                            Nazwisko = uczestnik.Gracz.Nazwisko,
                            ImiePrzeciwnika = uczestnik.ImiePrzeciwnika,
                            NazwiskoPrzeciwnika = uczestnik.NazwiskoPrzeciwnika
                        };
                        if (iloscZadan != 0)
                        {
                            statystykaGracza.OcenyZadan = new int[iloscZadan * iloscRund];
                        }
                        foreach (OcenaGracza ocena in oceny.Where(o => o.UczestnikGryId == uczestnik.Id).ToList())
                        {
                            if (ocena.NumerRundy == 0)
                            {
                                ocena.NumerRundy = 1;
                            }
                            statystykaGracza.OcenyZadan[(ocena.NumerRundy * ocena.NumerZadania) - 1] = ocena.Ocena;
                        }

                        statystykaGracza.SredniaOcen = Math.Round(statystykaGracza.OcenyZadan.Where(o => o != 0).DefaultIfEmpty().Average(), 2);
                        listaStatystyk.Add(statystykaGracza);
                    }
                }
                return listaStatystyk.OrderByDescending(x => x.SredniaOcen).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
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
                    listaGier = baza.Gra.Include(x => x.UczestnicyGry).Where(x => !x.CzyUsuniete && x.UzytkownikId == uzytkownikId).OrderByDescending(x => x.Data).ToList();
                }
                return listaGier;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
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
                LogHelper.Log.Error(ex);
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
                LogHelper.Log.Error(ex);
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
                LogHelper.Log.Error(ex);
                return null;
            }
        }
    }
}