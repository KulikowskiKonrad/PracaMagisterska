using NLog;
using PracaMagisterska.BazaDanych;
using PracaMagisterska.Helpers;
using PracaMagisterska.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracaMagisterska.Controllers
{

    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //Logger logger = LogManager.GetCurrentClassLogger();
            //LogHelper.Log.Error("jakiś błąd");
            //LogHelper.Log.Trace("jakiś ślad");
            //try
            //{
            //    throw new Exception("jakiś błąd");
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.Log.Error(ex);
            //}

            //ViewBag.Test = "asdasd";

            if (Request.IsAuthenticated == true && Session["uzytkownik"] == null)
            {
                //long idUzytkownika=long.Parse(Request.LogonUserIdentity.UserClaims.Where(x => x.Type == "UserId").Single().Value);
                string login = User.Identity.Name;
                UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                Uzytkownik uzytkownik = uzytkownikRepozytorium.Pobierz(login);
                Session["uzytkownik"] = uzytkownik;
            }
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}