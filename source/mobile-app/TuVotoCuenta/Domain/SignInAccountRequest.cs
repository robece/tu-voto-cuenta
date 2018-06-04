namespace TuVotoCuenta.Domain
{
    public class SignInAccountRequest : HttpResponseBase
    {
		public string username { get; set; }
        public string password { get; set; }
    }
}