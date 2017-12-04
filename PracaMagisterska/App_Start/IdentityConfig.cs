using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using PracaMagisterska.BazaDanych;
using PracaMagisterska.Repozytoria;
using PracaMagisterska.Models;
using PracaMagisterska.Enums;
using System.Web.SessionState;

namespace PracaMagisterska
{


    public class ApplicationSignInManager
    {
        private HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
        private IOwinContext OwinContext { get; set; }
        public ApplicationSignInManager(IOwinContext owinContext)
        {
            OwinContext = owinContext;
        }

        public SignInStatus PasswordSignIn(LogowanieViewModel model)
        {
            SignInStatus result = SignInStatus.Failure;

            Uzytkownik user = new UzytkownikRepozytorium().Pobierz(model.Login, model.Haslo);
            if (user != null)
            {
                result = SignInStatus.Success;
                var ident = new ClaimsIdentity(
                  new[] {
                      new Claim(ClaimTypes.NameIdentifier, model.Login),
                      new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                      new Claim(ClaimTypes.Name, model.Login),
                      new Claim(ClaimTypes.Role, ((RolaUzytkownika)user.Rola).ToString()),
                      new Claim("UserId", user.Id.ToString())
                  },
                  DefaultAuthenticationTypes.ApplicationCookie);

                OwinContext.Authentication.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = model.ZapamietajMnie ? (DateTime?)DateTime.UtcNow.AddDays(7) : null // jezeli wybrano zapamietaj mnie to uzytkownik bedzie zalogowany przez 7 dni
                }, ident);
                result = SignInStatus.Success;
                Session["uzytkownik"] = user;

                //SessionHelper.LoggedUser = user;
                //SessionHelper.UserId = user.Id;
            }

            return result;
        }
    }
}
