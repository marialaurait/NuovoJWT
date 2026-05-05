using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace NuovoJWT.Models
{
   
    public class FiltroUtente : Attribute, IAuthorizationFilter
    {
        private readonly string _TipoUtente;
        public FiltroUtente(string TipoUtente)
        {
            //non è dypendence injection
            _TipoUtente = TipoUtente;
        }//costruttore
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //qui vado a riconoscenre il tipo utente leggendo i claims del mio jwt
            var claims = context.HttpContext.User.Claims;
            //var tipoUtente_didattico = claims.FirstOrDefault(u=>u.Type== "quello definito nel claim")?.Value;//operatore o admin
            var tipoUtente = claims.FirstOrDefault(u => u.Type == "UserType")?.Value;
            //claims.FirstOrDefault(u => u.Type == "Personalizzato")?.Value qmi da quello che imposto io
            //sostituire "utente_loggato "con Tipo xche cosi ho stabilito nella classe di Login
            if (tipoUtente is null || tipoUtente != _TipoUtente)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);//xche statuscoderesult deve essere un int

            }
        }
    }//class

}
