using PracaMagisterska.BazaDanych;
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
    [Authorize(Roles = "Administrator")]
    public class NotatkaController : Controller
    {

        [HttpGet]
        public ActionResult ListaNotatek()
        {
            try
            {
                NotatkaRepozytorium notatkaRepozytorium = new NotatkaRepozytorium();
                List<Notatka> notatka = notatkaRepozytorium.PobierzWszystkie(((Uzytkownik)Session["uzytkownik"]).Id);
                return View("ListaNotatek", notatka);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
            finally
            {

            }
        }

        [HttpPost]
        public ActionResult Usun(long id)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    NotatkaRepozytorium notatkaRepozytorium = new NotatkaRepozytorium();
                    bool rezultatUsuniecia = notatkaRepozytorium.Usun(id);

                    return RedirectToAction("ListaNotatek");
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult DetaleNotatki(long? id)
        {
            try
            {
                EdytujNotatkeViewModel model = new EdytujNotatkeViewModel();
                NotatkaRepozytorium notatkaRepozytorium = new NotatkaRepozytorium();
                if (id.HasValue == true)
                {
                    Notatka pobranaNotatka = notatkaRepozytorium.Pobierz(id.Value);
                    model.Id = pobranaNotatka.Id;
                    model.DataDodania = pobranaNotatka.DataDodania;
                    model.Temat = pobranaNotatka.Temat;
                    model.Tresc = pobranaNotatka.Tresc;
                }
                return View("DetaleNotatki", model);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ZapiszDetaleNotatki(EdytujNotatkeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NotatkaRepozytorium notatkaRepozytorium = new NotatkaRepozytorium();
                    Notatka notatka = null;
                    if (model.Id.HasValue)
                    {
                        notatka = notatkaRepozytorium.Pobierz(model.Id.Value);
                    }
                    else
                    {
                        notatka = new Notatka();
                    }
                    notatka.Temat = model.Temat;
                    notatka.Tresc = model.Tresc;
                    notatka.UzytkownikId = ((Uzytkownik)Session["uzytkownik"]).Id;
                    notatka.DataDodania = DateTime.Now;
                    long? rezultatZapisu = notatkaRepozytorium.Zapisz(notatka);
                    if (rezultatZapisu != null)
                    {
                        return RedirectToAction("ListaNotatek");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View("DetaleNotatki", model);
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