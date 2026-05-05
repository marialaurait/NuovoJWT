using Microsoft.AspNetCore.Http;			
using Microsoft.AspNetCore.Mvc;

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
           // if ((((System.Security.Claims.Claim).ToString() == "".Items[1]).Value == "amministratore" )
           // if ((this.User.Identity.Claims[1]).Type.ToString()  == "amministratore" )
            risultato.Messaggio = "validazione utente superata come utente semplice";
            //c'è il codice per eseguire la funzione come utente semplice
            return Ok(risultato);
        }//Funzione_Utente

        
    }
}


