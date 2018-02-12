using PracaMagisterska.BazaDanych;
using PracaMagisterska.Enums;
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

        public StatystkiNajlepszychGraczyModel PobierzStatystykeNjlepszychGraczy()
        {
            try
            {
                StatystkiNajlepszychGraczyModel rezultat = new StatystkiNajlepszychGraczyModel();
                using (PracaMagisterskaEntities baza = new PracaMagisterskaEntities())
                {
                    var statystykiGraczy = baza.OcenaGracza
                        .Where(x => x.Ocena > 0 && !x.UczestnikGry.CzyUsuniety && !x.UczestnikGry.Gracz.CzyUsuniety)
                        .GroupBy(x => new { x.UczestnikGry.Gracz.Imie, x.UczestnikGry.Gracz.Nazwisko, x.UczestnikGry.GraczId, x.UczestnikGry.Gracz.Pozycja })
                        .Select(x => new
                        {
                            x.Key.Imie,
                            x.Key.Nazwisko,
                            Srednia = x.Average(y => y.Ocena),
                            x.Key.Pozycja
                        })
                        .OrderByDescending(x => x.Srednia)
                        .ToList();
                    var najlepszyPuenter = statystykiGraczy.Where(x => x.Pozycja == (byte)PozycjaGracza.Wybijajacy).FirstOrDefault();
                    if (najlepszyPuenter != null)
                    {
                        rezultat.NajlepszyPuenter = new StatystykiGracza()
                        {
                            Imie = najlepszyPuenter.Imie,
                            Nazwisko = najlepszyPuenter.Nazwisko,
                            SredniaOcen = najlepszyPuenter.Srednia
                        };
                    }
                    var najlepszyStrzelec = statystykiGraczy.Where(x => x.Pozycja == (byte)PozycjaGracza.Rzucajacy).FirstOrDefault();
                    if (najlepszyStrzelec != null)
                    {
                        rezultat.NajlepszyStrzelec = new StatystykiGracza()
                        {
                            Imie = najlepszyStrzelec.Imie,
                            Nazwisko = najlepszyStrzelec.Nazwisko,
                            SredniaOcen = najlepszyStrzelec.Srednia
                        };
                    }

                }
                return rezultat;
            }
            catch (Exception ex)
            {
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
                return null;
            }
        }



    }
}