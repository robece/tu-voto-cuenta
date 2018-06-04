namespace TuVotoCuenta.Domain
{
    public class SignUpAccountRequest : HttpResponseBase
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}