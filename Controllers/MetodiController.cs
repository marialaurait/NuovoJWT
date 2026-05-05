using LezioneJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace NuovoJWT.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodiController : ControllerBase
    {
        [FiltroUtente("amministratore")] //se non è admin non deve entrare
        [HttpPost("Funzione_admin")]
        public IActionResult Funzione_admin()
        {
            Risposta risultato = new Risposta(DateTime.Now);
            risultato.Messaggio = "validazione utente superata come amministratore";
            //c'è il codice per eseguire la funzione come amministratore
            return Ok(risultato);
        }//FunzioneAdmin

        //un API che è accessibile solo all utente generico
        //[FiltroUtente("utente")] //se non è admin non deve entrare
        [HttpPost("Funzione_utente")]
        public IActionResult Funzione_utente()
        {
            Risposta risultato = new Risposta(DateTime.Now);
            //aggiunto
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jsonToken != null)
            {
                // Il token è valido
                var claims = jsonToken.Claims;
                var Ruolo = claims.FirstOrDefault(c => c.Type == "UserType")?.Value;
                var TipoUtente = claims.FirstOrDefault(c => c.Type == "TipoUtente")?.Value;
                risultato.Messaggio = "validazione utente superata come utente " + Ruolo;
                //c'è il codice per eseguire la funzione come utente semplice
                return Ok(risultato);
            }
            else
            {
                risultato.Messaggio = "Token non valido";
                //// return forbidden with custom message
                // return new ObjectResult("Forbidden") { StatusCode = 403 };
                return Forbid();
            }
             //fineaggiunto
             
        }//Funzione_Utente

        [FiltroUtente2("utente", "amministratore")]
        [HttpPost("Funzione_con_filtri")]
        public IActionResult Funzione2()
        {

            Risposta risultato = new Risposta(DateTime.Now);
            risultato.Messaggio = "funzione riservata all utente generico";
            //risultato.Oraesecuzione=DateTime.Now;
            //qui mettere il codice per eseguire la funzione come amministratore
            return Ok(risultato);
        }//Funzione con 2 parametri in filtroutente



        [HttpPost("SenzaFiltro")]
        public IActionResult SenzaFiltro()
        {
            Risposta risultato = new Risposta(DateTime.Now);
            risultato.Messaggio = "";
            //voglio leeggere il conteenuto dei claims dal token passato in headeer
            //potrebbe servirmi per usi speciali
            var Auth = Request.Headers["Authorization"];
            // Esempio con la libreria JWT.io
            //leggo il token dopo la parola Bearer

            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jsonToken != null)
            {
                // Il token è valido
                var claims = jsonToken.Claims;
                var Ruolo = claims.FirstOrDefault(c => c.Type == "UserType")?.Value;
                var TipoUtente = claims.FirstOrDefault(c => c.Type == "TipoUtente")?.Value;
                switch (Ruolo)
                {
                    case "funzionario":
                        risultato.Messaggio = "Consentire le funzioni solo al funzionario";
                        break;
                    case "amministratore":
                        risultato.Messaggio = "Consentire le funzioni solo all'amministratore";
                        break;
                    default:
                        risultato.Messaggio = "Ruolo non valido";
                        return Forbid();
                        break;
                }//switch
            }
            else
            {
                risultato.Messaggio = "Token non valido";
                //// return forbidden with custom message
                // return new ObjectResult("Forbidden") { StatusCode = 403 };
                return Forbid();
            }
            return Ok(risultato);
        }//SenzaFiltro
    }
}


