using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuovoJWT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NuovoJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok("api funzionante");
        }

        [HttpPost("Login")]
        public IActionResult Login(Autenticazione param)
        {
            //regole di login leggere, si possono aumentare
            if ((param.UserName == "admin") && (param.Password == "admin"))
            {
                param.Tipo = "amministratore";
            }
            if ((param.UserName == "utente") && (param.Password == "utente"))
            {
                param.Tipo = "utente";
            }
            if (param.Tipo != "")
            {
                //compongo il mio token definendo i claims
                //genero il token e popolo le pro della fig.1 ossia i claims
                List<Claim> ListaClaims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub,param.UserName), //il sub lo definisce il cliente
                    new Claim("UserType",param.Tipo),
                    new Claim("Personalizzato","tutto quello che voglio"),
                    new Claim("campo1","valore..."),
                    new Claim("campo2","valore..."),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString() )//identificativo univoco
                };
                //definisco la chiave x la cifratura del mio algoritmo
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("la mia mia chiave"));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("corso_talentform!corso_talentform"));
                //abbino la mia chiave all algoritmo
                var credenziali = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //creo il token
                var mioTokenJwt = new JwtSecurityToken(issuer: "mio sito", audience: "indirizzo api",//issuer e audience stabiliti in program.cs
                    claims: ListaClaims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credenziali
                    );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(mioTokenJwt) });
            }
            else
            {
                return Unauthorized();
            }
        }//login



        //[HttpPost("login")]
        //public ActionResult login(Autenticazione account)
        //{
        //    //scrivere algortmo per la reale autenticazione
        //    return Ok();
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}

