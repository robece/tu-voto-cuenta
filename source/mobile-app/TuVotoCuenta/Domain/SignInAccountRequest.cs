namespace TuVotoCuenta.Domain
{
    public class SignInAccountRequest : HttpRequestBase
    {
		public string username { get; set; }
        public string password { get; set; }
    }
}