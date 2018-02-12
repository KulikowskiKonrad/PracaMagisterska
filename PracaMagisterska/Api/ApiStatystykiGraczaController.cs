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
            GraczRepozytorium graczRepozytorium = new GraczRepozytorium();
            StatystkiNajlepszychGraczyModel rezultat = graczRepozytorium.PobierzStatystykeNjlepszychGraczy();

            return rezultat;
        }

    }
}
