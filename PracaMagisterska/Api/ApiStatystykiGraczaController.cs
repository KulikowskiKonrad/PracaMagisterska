using Microsoft.AspNet.Identity;
using PracaMagisterska.BazaDanych;
using PracaMagisterska.Models.Api;
using PracaMagisterska.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PracaMagisterska.Api
{
    public class ApiStatystykiGraczaController : ApiController
    {

        [HttpGet]
        public StatystkiNajlepszychGraczyModel PobierzStatystykeNjlepszychGraczy()
        {
            try
            {
                UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                GraczRepozytorium graczRepozytorium = new GraczRepozytorium();
                Uzytkownik uzytkownik = uzytkownikRepozytorium.Pobierz(User.Identity.Name);
                StatystkiNajlepszychGraczyModel rezultat = graczRepozytorium.PobierzStatystykeNjlepszychGraczy(uzytkownik.Id);

                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
