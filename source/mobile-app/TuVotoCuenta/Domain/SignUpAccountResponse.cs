namespace TuVotoCuenta.Domain
{
	public class SignUpAccountResponse
    {
        public bool IsSucceded { get; set; }
        public string Account { get; set; }
        public string Image { get; set; }
		public int ResultId { get; set; }
    }
}