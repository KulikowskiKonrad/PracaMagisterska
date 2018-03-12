using PracaMagisterska.BazaDanych;
using PracaMagisterska.Enums;
using PracaMagisterska.Helpers;
using PracaMagisterska.Models;
using PracaMagisterska.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracaMagisterska.Controllers
{
    public class GraController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Statystyki(DateTime? dataOd, DateTime? dataDo, byte? iloscPuenterow, byte? iloscStrzelcow, PlecGracza? plec)
        {
            try
            {
                if (!dataOd.HasValue)
                {
                    dataOd = DateTime.Today.AddMonths(-6);
                }
                if (!dataDo.HasValue)
                {
                    dataDo = DateTime.Today;
                }
                GraRepozytorium graRepozytorium = new GraRepozytorium();
                List<StatystykiZawodnika> listaStatystykGraczy = graRepozytorium.PobierzStatytstyki(dataOd.Value, dataDo.Value, plec);
                StatystykiViewModel statystykiViewModel = new StatystykiViewModel()
                {
                    DataDo = dataDo.Value,
                    DataOd = dataOd.Value,
                    Plec = plec,
                    ListaStatystykZawodnikow = listaStatystykGraczy
                };

                if (iloscPuenterow.HasValue)
                {
                    statystykiViewModel.ListaProponowanychZawodnikow.AddRange(listaStatystykGraczy.Where(x => x.Pozycja == PozycjaGracza.Puenter).Take(iloscPuenterow.Value).ToList());
                }
                if (iloscStrzelcow.HasValue)
                {
                    statystykiViewModel.ListaProponowanychZawodnikow.AddRange(listaStatystykGraczy.Where(x => x.Pozycja == PozycjaGracza.Strzelec).Take(iloscStrzelcow.Value).ToList());
                }
                statystykiViewModel.ListaProponowanychZawodnikow = statystykiViewModel.ListaProponowanychZawodnikow.OrderByDescending(x => x.SredniaOcen).ToList();

                return View(statystykiViewModel);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult StatystykiTreningu(long idGry)
        {
            try
            {
                GraRepozytorium graRepozytorium = new GraRepozytorium();
                List<StatystykiTreninguViewModel> gra = graRepozytorium.PobierzListeStatystykTreningu(idGry);
                return View(gra);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Usun(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GraRepozytorium graRepozytorium = new GraRepozytorium();
                    bool rezultatUsuniecia = graRepozytorium.Usun(id);
                    return RedirectToAction("ListaGier");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult ListaGier()
        {
            try
            {
                GraRepozytorium graRepozytorium = new GraRepozytorium();
                List<Gra> listaGier = graRepozytorium.PobierzWszystkie(((Uzytkownik)Session["uzytkownik"]).Id);
                return View(listaGier);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult DetaleGry(long? id)
        {
            try
            {
                EdytujGreViewModel model = new EdytujGreViewModel();
                model.Id = id;
                GraRepozytorium graRepozytorium = new GraRepozytorium();
                if (id.HasValue)
                {
                    Gra pobranaGra = graRepozytorium.Pobierz(id.Value);
                    model.Data = pobranaGra.Data;
                    model.Miejsce = pobranaGra.Miejsce;
                    model.TypGry = (TypGry)pobranaGra.Typ;
                    model.KategoriaWiekowa = (KategoriaWiekowa)pobranaGra.KategoriaWiekowaGry;
                    model.UzupelnijListeGraczy();
                    if (pobranaGra.UczestnicyGry.Any())
                    {
                        model.ListaUczestnikow.Clear();
                    }
                    foreach (UczestnikGry uczestnik in pobranaGra.UczestnicyGry.Where(x => !x.CzyUsuniety))
                    {
                        model.ListaUczestnikow.Add(new UczestnikGryViewModel()
                        {
                            GraczId = uczestnik.GraczId,
                            ImiePrzeciwnika = uczestnik.ImiePrzeciwnika,
                            NazwiskoPrzeciwnika = uczestnik.NazwiskoPrzeciwnika,
                            Id = uczestnik.Id
                        });
                    }
                }
                else
                {
                    model.TypGry = TypGry.Trening;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ZapiszDetaleGry(EdytujGreViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    GraRepozytorium graRepozytorium = new GraRepozytorium();
                    Gra gra = null;
                    if (model.Id.HasValue)
                    {
                        gra = graRepozytorium.Pobierz(model.Id.Value);
                    }
                    else
                    {
                        gra = new Gra();
                        gra.Typ = (byte)model.TypGry;
                        gra.KategoriaWiekowaGry = (byte)model.KategoriaWiekowa;
                    }
                    gra.Miejsce = model.Miejsce;
                    gra.Data = (DateTime)model.Data;
                    gra.UzytkownikId = ((Uzytkownik)Session["uzytkownik"]).Id;
                    long? rezultatZapisu = graRepozytorium.Zapisz(gra);

                    if (model.Id.HasValue)
                    {
                        UczestnikGry uczestnikGry = null;
                        UczestnikGryRepozytorium uczestnikGryRepozytorium = new UczestnikGryRepozytorium();
                        foreach (UczestnikGryViewModel uczestnik in model.ListaUczestnikow)
                        {
                            if (uczestnik.Id.HasValue)
                            {
                                uczestnikGry = uczestnikGryRepozytorium.Pobierz(uczestnik.Id.Value);
                            }
                            else
                            {
                                uczestnikGry = new UczestnikGry();
                                uczestnikGry.GraId = gra.Id;
                            }
                            uczestnikGry.ImiePrzeciwnika = uczestnik.ImiePrzeciwnika;
                            uczestnikGry.NazwiskoPrzeciwnika = uczestnik.NazwiskoPrzeciwnika;
                            uczestnikGry.GraczId = uczestnik.GraczId.Value;//uczestnik gry jest z tabeli a uczestnik jest z ViewModel czyli tego co podal uzytkownik
                            uczestnik.Id = uczestnikGryRepozytorium.Zapisz(uczestnikGry);
                        }
                        List<UczestnikGry> listaUczestnikowGryWBazie = uczestnikGryRepozytorium.PobierzListeUczestnikow(gra.Id);
                        List<UczestnikGry> uczestnicyDoUsuniecia = listaUczestnikowGryWBazie.Where(x => !model.ListaUczestnikow.Where(y => y.Id == x.Id).Any()).ToList();
                        // w liscie uczestnikowGryDoUsuniecia przypisuje sie elementy z listy uczestnikowGryWBazie ktorych id nie wystepuje w ViewModel na liscie uczestnmikow gry
                        foreach (UczestnikGry uczestnik in uczestnicyDoUsuniecia)
                        {
                            uczestnik.CzyUsuniety = true;
                            long? rezultatZapisuUczestnika = uczestnikGryRepozytorium.Zapisz(uczestnik);
                        }
                    }
                    if (rezultatZapisu != null)
                    {
                        if (model.Id.HasValue)
                        {
                            return RedirectToAction("ListaGier");
                        }
                        else
                        {
                            return RedirectToAction("DetaleGry", new { id = gra.Id });
                        }
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return RedirectToAction("DetaleGry", model);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }
    }
}