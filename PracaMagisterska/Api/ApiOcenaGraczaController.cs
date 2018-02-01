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
    public class ApiOcenaGraczaController : ApiController
    {
        [HttpPost]
        public bool Zapisz([FromBody] List<OcenaGracza> model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OcenaGraczaRepozytorium ocenaGraczaRepozytorium = new OcenaGraczaRepozytorium();
                    List<OcenaGracza> listaZRepozytorium = ocenaGraczaRepozytorium.PobierzOcenyPoUczestniku(model[0].UczestnikGryId);

                    foreach (OcenaGracza ocena in model)
                    {
                        OcenaGracza ocenaZBazy = listaZRepozytorium.Where(x => x.NumerZadania == ocena.NumerZadania).SingleOrDefault(); // to poazwala na okreslenie czy wiersz 
                        //bedzie aktualizowany czy bedzie dodawany
                        if (ocenaZBazy != null) // jezeli wartosc z wiersza jest rozna od null to aktualizuje a jezeli nie istnieje to zapisuje nowymi danymi
                        {
                            ocena.Id = ocenaZBazy.Id; //przypisuje  id jezewli juz istnieje
                        }
                        ocenaGraczaRepozytorium.Zapisz(ocena); //zapisuje
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public List<OcenaGraczaModel> Pobierz([FromUri]long uczestnikId)
        {
            try
            {
                OcenaGraczaRepozytorium ocenaGraczaRepozytorium = new OcenaGraczaRepozytorium();
                List<OcenaGracza> listaOcena = ocenaGraczaRepozytorium.PobierzOcenyPoUczestniku(uczestnikId);
                List<OcenaGraczaModel> rezultat = new List<OcenaGraczaModel>();
                foreach (OcenaGracza ocena in listaOcena)
                {
                    rezultat.Add(new OcenaGraczaModel()
                    {
                        DecyzjaGracza = ocena.DecyzjaGracza,
                        DecyzjaTrenera = ocena.DecyzjaTrenera,
                        Id = ocena.Id,
                        NumerZadania = ocena.NumerZadania,
                        Ocena = ocena.Ocena
                    });
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
