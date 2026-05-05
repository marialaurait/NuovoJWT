using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace LezioneJWT.Models
{
    public class FiltroUtente2 : Attribute, IAuthorizationFilter
    {
        private readonly string _TipoUtente1;
        private readonly string _TipoUtente2;
        private readonly string[] _arrTipoUtente;
        public FiltroUtente2(string TipoUtente, string valore2)
        {
            //non è dypendence injection
            _TipoUtente1 = TipoUtente;
            _TipoUtente2 = valore2;
        }//costruttore

        public FiltroUtente2(string[] arrParametri)
        {
            _arrTipoUtente = arrParametri;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //qui vado a riconoscenre il tipo utente leggendo i claims del mio jwt
            var claims = context.HttpContext.User.Claims;
            //var tipoUtente_didattico = claims.FirstOrDefault(u=>u.Type== "quello definito nel claim")?.Value;//operatore o admin
            var tipoUtente = claims.FirstOrDefault(u => u.Type == "UserType")?.Value;
            //claims.FirstOrDefault(u => u.Type == "Personalizzato")?.Value qmi da quello che imposto io
            //sostituire "utente_loggato "con Tipo xche cosi ho stabilito nella classe di Login
            /*
                  if (tipoUtente is null || tipoUtente != _TipoUtente)
                  {
                    context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);//xche statuscoderesult deve essere un int

                  }
            */
            if (tipoUtente is not null)
            {
                //la mia logica
                if ((tipoUtente == _TipoUtente1) || (tipoUtente == _TipoUtente2))
                {
                }
                else
                {
                    context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                }
            }
            else
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);//xche statuscoderesult deve essere un int
            }
        }
    }//class

}
