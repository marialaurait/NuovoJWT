namespace NuovoJWT.Models
{
        public class Autenticazione()
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string? Tipo { get; set; } = "";// "deve diventare utente o admin";

        }

}
