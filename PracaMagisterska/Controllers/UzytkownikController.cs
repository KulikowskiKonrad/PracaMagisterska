using PracaMagisterska.BazaDanych;
using PracaMagisterska.Enums;
using PracaMagisterska.Models;
using PracaMagisterska.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PracaMagisterska.Helpers;

namespace PracaMagisterska.Controllers
{
    public class UzytkownikController : Controller
    {


        // GET: Uzytkownik
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //[Authorize(Roles = "Administrator")]
        //public ActionResult Anuluj()
        //{
        //    try
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error");
        //    }

        //}

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edytuj()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edytuj(EdytujUzytkownikaViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                    Uzytkownik uzytkownik = uzytkownikRepozytorium.Pobierz(((Uzytkownik)Session["uzytkownik"]).Id);
                    string sol = Guid.NewGuid().ToString();
                    uzytkownik.Sol = sol;
                    uzytkownik.Haslo = MD5Helper.GenerujMD5(model.Haslo + sol);
                    long? rezultatEdycji = uzytkownikRepozytorium.Zapisz(uzytkownik);
                    if (rezultatEdycji != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View("Edytuj", model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Zaloguj()
        {
            try
            {
                UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                return View("Logowanie");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Zaloguj(LogowanieViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var result = new ApplicationSignInManager(HttpContext.GetOwinContext()).PasswordSignIn(model);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            return RedirectToAction("Index", "Home");

                        case SignInStatus.LockedOut:
                        case SignInStatus.RequiresVerification:
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("Haslo", "Niepoprawny login lub hasło");
                            return View("Logowanie", model);
                    }

                }
                else
                {
                    return View("Logowanie", model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
        [HttpGet]
        public ActionResult Rejestracja()
        {
            return View("Rejestracja");
        }

        [HttpPost]
        public ActionResult Rejestracja(RejestracjaViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                    Uzytkownik pobranyUzytkownik = uzytkownikRepozytorium.Pobierz(model.Login);
                    if (pobranyUzytkownik == null)
                    {
                        string sol = Guid.NewGuid().ToString(); //robie sol jako GUID i zamieniam na string

                        Uzytkownik uzytkownik = new Uzytkownik()
                        {
                            Sol = sol,
                            Login = model.Login,
                            Haslo = MD5Helper.GenerujMD5(model.Haslo + sol), //generujemy md5 z polaczenia hasla i soli (losowego ciagu znakow) wywoluje metode statyczna z klasy
                                                                             //MD5Helper
                            Rola = (byte)RolaUzytkownika.Administrator
                        };
                        long? rezultatZapisu = uzytkownikRepozytorium.Zapisz(uzytkownik);
                        if (rezultatZapisu != null)
                        {
                            return RedirectToAction("ListaGier", "Gra");
                        }
                        else
                        {
                            return View("Error");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Login", "Login jest już zajęty");
                        return View("Rejestracja", model);
                    }
                }
                else
                {
                    return View("Rejestracja", model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
        [HttpPost]
        public ActionResult Wyloguj()
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    if (Request.IsAuthenticated == true)
                    {
                        Session.Abandon();
                        HttpContext.GetOwinContext().Authentication.SignOut();
                    }
                    return RedirectToAction("Zaloguj");
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
    }
}


