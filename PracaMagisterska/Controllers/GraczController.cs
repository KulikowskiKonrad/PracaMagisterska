using PracaMagisterska.BazaDanych;
using PracaMagisterska.Enums;
using PracaMagisterska.Models;
using PracaMagisterska.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracaMagisterska.Controllers
{
    public class GraczController : Controller
    {
        //[Authorize]
        //[HttpGet]
        //public ActionResult DodajGracza()
        //{
        //    try
        //    {
        //        return View("DodajGracza");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error");
        //    }
        //}

        [Authorize]
        [HttpGet]
        public ActionResult ListaGraczy()
        {
            try
            {
                GraczRepozytorium listaGraczyRepozytorium = new GraczRepozytorium();
                List<Gracz> listaGraczy = listaGraczyRepozytorium.PobierzWszystkich();
                return View(listaGraczy);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Usun(long id)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    GraczRepozytorium graczRepozytorium = new GraczRepozytorium();
                    bool rezultatUsuniecia = graczRepozytorium.Usun(id);
                    return RedirectToAction("ListaGraczy");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult DetaleGracza(long? id)
        {
            try
            {
                EdytujGraczaViewModel model = new EdytujGraczaViewModel();
                GraczRepozytorium graczRepozytorium = new GraczRepozytorium();
                if (id.HasValue == true)
                {
                    Gracz pobranyGracz = graczRepozytorium.Pobierz(id.Value);
                    model.Id = pobranyGracz.Id;
                    model.Imie = pobranyGracz.Imie;
                    model.Nazwisko = pobranyGracz.Nazwisko;
                    model.NrLicencji = pobranyGracz.NrLicencji;
                    model.KlubId = pobranyGracz.KlubId;
                    model.Pozycja = (PozycjaGracza)pobranyGracz.Pozycja;
                }
                return View("DetaleGracza", model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ZapiszDetaleGracza(EdytujGraczaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                //if (!ModelState.IsValid)
                //if(model.KlubId.HasValue)
                //if (!model.KlubId.HasValue)
                {
                    GraczRepozytorium graczRepozytorium = new GraczRepozytorium();
                    Gracz gracz = null;
                    if (model.Id.HasValue)
                    {
                        gracz = graczRepozytorium.Pobierz(model.Id.Value);
                    }
                    else
                    {
                        gracz = new Gracz();  
                    }
                    gracz.Imie = model.Imie;
                    gracz.Nazwisko = model.Nazwisko;
                    gracz.NrLicencji = model.NrLicencji;
                    gracz.Pozycja = (byte)model.Pozycja;
                    gracz.KlubId = model.KlubId;
                    long? rezultatZapisu = graczRepozytorium.Zapisz(gracz);
                    if (rezultatZapisu != null)
                    {
                        return RedirectToAction("ListaGraczy");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View("DetaleGracza", model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        //[Authorize]
        //[HttpPost]
        //public ActionResult Zapisz(EdytujGraczaViewModel model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid == true)
        //        {
        //            GraczRepozytorium graczRepozytorium = new GraczRepozytorium();
        //            Gracz pobranyGracz = graczRepozytorium.Pobierz(model.Imie, model.Nazwisko);
        //            if (pobranyGracz == null)
        //            {
        //                Gracz gracz = new Gracz()
        //                {
        //                    Imie = model.Imie,
        //                    Nazwisko = model.Nazwisko,
        //                    NrLicencji = model.NrLicencji,
        //                    Pozycja = (byte)model.Pozycja
        //                    //Klub=model.ListaKlubow,
        //                };
        //                long? rezultatZapisu = graczRepozytorium.Zapisz(gracz);
        //                if (rezultatZapisu != null)
        //                {
        //                    return RedirectToAction("ListaGraczy", "Gracz");
        //                }
        //                else
        //                {
        //                    return View("Error");
        //                }
        //            }
        //            else
        //            {
        //                return View("DodajGracza", model);
        //            }
        //        }
        //        else
        //        {
        //            return View("DodajGracza", model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error");
        //    }
        //}
    }
}