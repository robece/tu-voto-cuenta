namespace TuVotoCuenta.Domain
{
    public class SignUpAccountRequest : HttpRequestBase
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}