using PracaMagisterska.BazaDanych;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PracaMagisterska.Api
{
    public class ApiOcenaGraczaController : ApiController
    {
        [HttpPost]
        public bool Zapisz([FromBody] List<OcenaGracza> model)
        {

            return true;

        }


    }
}
