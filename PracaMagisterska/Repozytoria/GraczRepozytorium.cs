using PracaMagisterska.BazaDanych;
using PracaMagisterska.Enums;
using PracaMagisterska.Helpers;
using PracaMagisterska.Models.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracaMagisterska.Repozytoria
{
    public class GraczRepozytorium
    {

        public StatystkiNajlepszychGraczyModel PobierzStatystykeNjlepszychGraczy(long uzytkownikId)
        {
            try
            {
                StatystkiNajlepszychGraczyModel rezultat = new StatystkiNajlepszychGraczyModel();
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    var statystykiGraczy = baza.OcenaGracza
                        .Where(x => x.Ocena > 0 && !x.UczestnikGry.CzyUsuniety && !x.UczestnikGry.Gracz.CzyUsuniety && x.UczestnikGry.Gracz.UzytkownikId == uzytkownikId
                            && x.UczestnikGry.Gra.Typ != (byte)TypGry.Trening /*&& x.UczestnikGry.Gracz.Pozycja == x.RodzajZadania*/)
                        .GroupBy(x => new { x.UczestnikGry.Gracz.Imie, x.UczestnikGry.Gracz.Nazwisko, x.UczestnikGry.GraczId, x.UczestnikGry.Gracz.Pozycja })
                        .Select(x => new
                        {
                            x.Key.Imie,
                            x.Key.Nazwisko,
                            Srednia = x.Average(y => y.Ocena),
                            x.Key.Pozycja,
                            x.Key.GraczId,
                        })
                        .OrderByDescending(x => x.Srednia)
                        .ToList();
                    var najlepszyPuenter = statystykiGraczy.Where(x => x.Pozycja == (byte)PozycjaGracza.Puenter).FirstOrDefault();
                    if (najlepszyPuenter != null)
                    {
                        rezultat.NajlepszyPuenter = new StatystykiGracza()
                        {
                            Imie = najlepszyPuenter.Imie,
                            Nazwisko = najlepszyPuenter.Nazwisko,
                            SredniaOcen = Math.Round(najlepszyPuenter.Srednia, 2)
                        };


                        var listaOstatnich10Gier = baza.OcenaGracza.Where(x => x.UczestnikGry.GraczId == najlepszyPuenter.GraczId && x.Ocena > 0
                                && !x.UczestnikGry.CzyUsuniety && !x.UczestnikGry.Gracz.CzyUsuniety)
                            .GroupBy(x => new { x.UczestnikGry.Gra.Id, x.UczestnikGry.Gra.Data })
                            .Select(x => new
                            {
                                x.Key.Data,
                                x.Key.Id,
                                Srednia = x.Average(y => y.Ocena),
                            })
                            .OrderBy(x => x.Data)
                            .ThenBy(x => x.Id)
                            .Take(10)
                            .ToList();

                        rezultat.NajlepszyPuenter.ListaOcen = listaOstatnich10Gier.Select(x => Math.Round((x.Srednia), 2)).ToList();
                        rezultat.NajlepszyPuenter.ListaDat = listaOstatnich10Gier.Select(x => x.Data.ToShortDateString()).ToList();
                    }
                    var najlepszyStrzelec = statystykiGraczy.Where(x => x.Pozycja == (byte)PozycjaGracza.Strzelec).FirstOrDefault();
                    if (najlepszyStrzelec != null)
                    {
                        rezultat.NajlepszyStrzelec = new StatystykiGracza()
                        {
                            Imie = najlepszyStrzelec.Imie,
                            Nazwisko = najlepszyStrzelec.Nazwisko,
                            SredniaOcen = Math.Round(najlepszyStrzelec.Srednia, 2)
                        };

                        var listaOstatnich10Gier = baza.OcenaGracza.Where(x => x.UczestnikGry.GraczId == najlepszyStrzelec.GraczId && x.Ocena > 0
                                && !x.UczestnikGry.CzyUsuniety && !x.UczestnikGry.Gracz.CzyUsuniety)
                            .GroupBy(x => new { x.UczestnikGry.Gra.Id, x.UczestnikGry.Gra.Data })
                            .Select(x => new
                            {
                                x.Key.Data,
                                x.Key.Id,
                                Srednia = x.Average(y => y.Ocena),
                            })
                            .OrderBy(x => x.Data)
                            .ThenBy(x => x.Id)
                            .Take(10)
                            .ToList();

                        rezultat.NajlepszyStrzelec.ListaOcen = listaOstatnich10Gier.Select(x => Math.Round((x.Srednia), 2)).ToList();
                        rezultat.NajlepszyStrzelec.ListaDat = listaOstatnich10Gier.Select(x => x.Data.ToShortDateString()).ToList();
                    }



                    var statystykiGraczyWgZadan = baza.OcenaGracza
                        .Where(x => x.Ocena > 0 && !x.UczestnikGry.CzyUsuniety && !x.UczestnikGry.Gracz.CzyUsuniety && x.UczestnikGry.Gracz.UzytkownikId == uzytkownikId
                            && x.UczestnikGry.Gra.Typ != (byte)TypGry.Trening /*&& x.UczestnikGry.Gracz.Pozycja == x.RodzajZadania*/)
                        .GroupBy(x => new { x.UczestnikGry.Gracz.Imie, x.UczestnikGry.Gracz.Nazwisko, x.UczestnikGry.GraczId, x.RodzajZadania })
                        .Select(x => new
                        {
                            x.Key.Imie,
                            x.Key.Nazwisko,
                            Srednia = x.Average(y => y.Ocena),
                            x.Key.RodzajZadania,
                            x.Key.GraczId,
                        })
                        .OrderByDescending(x => x.Srednia)
                        .ToList();
                    var najlepszyPuenterWgPozycji = statystykiGraczyWgZadan.Where(x => x.RodzajZadania == (byte)RodzajZadania.Puenterskie).FirstOrDefault();
                    if (najlepszyPuenterWgPozycji != null)
                    {
                        rezultat.NajlepszyGraczNaPozycjiPuenter = new StatystykiGracza()
                        {
                            Imie = najlepszyPuenterWgPozycji.Imie,
                            Nazwisko = najlepszyPuenterWgPozycji.Nazwisko,
                            SredniaOcen = Math.Round(najlepszyPuenterWgPozycji.Srednia, 2)
                        };


                        var listaOstatnich10Gier = baza.OcenaGracza.Where(x => x.UczestnikGry.GraczId == najlepszyPuenterWgPozycji.GraczId && x.Ocena > 0
                                && !x.UczestnikGry.CzyUsuniety && !x.UczestnikGry.Gracz.CzyUsuniety)
                            .GroupBy(x => new { x.UczestnikGry.Gra.Id, x.UczestnikGry.Gra.Data })
                            .Select(x => new
                            {
                                x.Key.Data,
                                x.Key.Id,
                                Srednia = x.Average(y => y.Ocena),
                            })
                            .OrderBy(x => x.Data)
                            .ThenBy(x => x.Id)
                            .Take(10)
                            .ToList();

                        rezultat.NajlepszyGraczNaPozycjiPuenter.ListaOcen = listaOstatnich10Gier.Select(x => Math.Round((x.Srednia), 2)).ToList();
                        rezultat.NajlepszyGraczNaPozycjiPuenter.ListaDat = listaOstatnich10Gier.Select(x => x.Data.ToShortDateString()).ToList();
                    }

                    var najlepszyStrzelecWgPozycji = statystykiGraczyWgZadan.Where(x => x.RodzajZadania == (byte)RodzajZadania.Strzeleckie).FirstOrDefault();
                    if (najlepszyStrzelecWgPozycji != null)
                    {
                        rezultat.NajlepszyGraczNaPozycjiStrzelec = new StatystykiGracza()
                        {
                            Imie = najlepszyStrzelecWgPozycji.Imie,
                            Nazwisko = najlepszyStrzelecWgPozycji.Nazwisko,
                            SredniaOcen = Math.Round(najlepszyStrzelecWgPozycji.Srednia, 2)
                        };


                        var listaOstatnich10Gier = baza.OcenaGracza.Where(x => x.UczestnikGry.GraczId == najlepszyStrzelecWgPozycji.GraczId && x.Ocena > 0
                                && !x.UczestnikGry.CzyUsuniety && !x.UczestnikGry.Gracz.CzyUsuniety)
                            .GroupBy(x => new { x.UczestnikGry.Gra.Id, x.UczestnikGry.Gra.Data })
                            .Select(x => new
                            {
                                x.Key.Data,
                                x.Key.Id,
                                Srednia = x.Average(y => y.Ocena),
                            })
                            .OrderBy(x => x.Data)
                            .ThenBy(x => x.Id)
                            .Take(10)
                            .ToList();

                        rezultat.NajlepszyGraczNaPozycjiStrzelec.ListaOcen = listaOstatnich10Gier.Select(x => Math.Round((x.Srednia), 2)).ToList();
                        rezultat.NajlepszyGraczNaPozycjiStrzelec.ListaDat = listaOstatnich10Gier.Select(x => x.Data.ToShortDateString()).ToList();
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

        public List<Gracz> PobierzWszystkich(long uzytkownikId)
        {
            try
            {
                List<Gracz> rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Gracz.Include(x => x.Klub).Where(x => x.CzyUsuniety == false && x.UzytkownikId == uzytkownikId).OrderByDescending(x => x.Nazwisko).ToList();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public List<Gracz> PobierzWszystkich(long uzytkownikId, KategoriaWiekowa kategoriaWiekowa)
        {
            try
            {
                List<Gracz> rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Gracz.Include(x => x.Klub).Where(x => x.CzyUsuniety == false && x.UzytkownikId == uzytkownikId
                        && (x.KategoriaWiekowa == (byte)kategoriaWiekowa || kategoriaWiekowa == KategoriaWiekowa.Open))
                    .OrderByDescending(x => x.Nazwisko).ToList();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public Gracz Pobierz(long id)
        {
            try
            {
                Gracz rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Gracz.Where(x => x.Id == id).Single();
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public Gracz Pobierz(string imie, string nazwisko)
        {
            try
            {
                Gracz rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    rezultat = baza.Gracz.Where(x => x.Imie == imie && x.Nazwisko == nazwisko).Single();
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
                    Gracz graczZBazy = null;
                    graczZBazy = baza.Gracz.Where(x => x.Id == id).Single();
                    graczZBazy.CzyUsuniety = true;
                    baza.SaveChanges();
                    rezultat = true;
                    return rezultat;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return false;
            }
        }

        public long? Zapisz(Gracz gracz)
        {
            try
            {
                long? rezultat = null;
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    baza.Entry(gracz).State = gracz.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = gracz.Id;
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