namespace TuVotoCuenta.Domain
{
    public class SignInAccountResponse : HttpResponseBase
    {
        public bool IsSucceded { get; set; }
		public string Username { get; set; }
        public string Image { get; set; }
        public int ResultId { get; set; }
    }
}