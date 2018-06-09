namespace TuVotoCuenta.Domain
{
    public class SignInAccountResponse : HttpResponseBase
    {
        
		public string Username { get; set; }
        public string Image { get; set; }
    }
}