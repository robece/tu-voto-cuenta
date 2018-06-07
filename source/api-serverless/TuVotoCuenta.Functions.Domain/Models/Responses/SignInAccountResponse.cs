namespace TuVotoCuenta.Functions.Domain.Models.Responses
{
    public class SignInAccountResponse
    {
        public bool IsSucceded { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public int ResultId { get; set; }
    }
}