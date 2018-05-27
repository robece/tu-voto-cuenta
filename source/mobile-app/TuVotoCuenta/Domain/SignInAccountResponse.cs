namespace TuVotoCuenta.Domain
{
    public class SignInAccountResponse
    {
        public bool IsSucceded { get; set; }
        public string Account { get; set; }
		public string Fullname { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public int ResultId { get; set; }
    }
}