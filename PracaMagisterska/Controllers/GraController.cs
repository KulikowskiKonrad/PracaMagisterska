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
    public class GraController : Controller
    {
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
                List<Gra> listaGier = graRepozytorium.PobierzWszystkie();
                return View(listaGier);
            }
            catch (Exception ex)
            {
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
                GraRepozytorium graRepozytorium = new GraRepozytorium();
                if (id.HasValue)
                {
                    Gra pobranaGra = graRepozytorium.Pobierz(id.Value);
                    model.Data = pobranaGra.Data;
                    model.Miejsce = pobranaGra.Miejsce;
                    model.TypGry = (TypGry)pobranaGra.Typ;
                }
                return View(model);
            }
            catch (Exception ex)
            {
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
                    }
                    gra.Miejsce = model.Miejsce;
                    gra.Typ = (byte)model.TypGry;
                    gra.Data = (DateTime)model.Data;
                    long? rezultatZapisu = graRepozytorium.Zapisz(gra);
                    if (rezultatZapisu != null)
                    {
                        return RedirectToAction("ListaGier", model);
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
                return View("Error");
            }
        }


    }
}