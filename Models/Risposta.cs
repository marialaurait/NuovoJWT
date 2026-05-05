namespace NuovoJWT.Models
{
    public class Risposta
    {
        public string Messaggio { get; set; }
        public DateTime OraEsecuzione { get; } //non la può cambiare il frontend
        public Risposta(DateTime dt)
        {
            //mi serve definire un costruttore perché la prop OraEsecuzione è solo in get
            this.OraEsecuzione = dt;
        }
    }
}
