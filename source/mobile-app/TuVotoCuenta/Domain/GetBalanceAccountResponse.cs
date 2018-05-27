namespace TuVotoCuenta.Domain
{
	public class GetBalanceAccountResponse
    {
		public bool IsSucceded { get; set; }
        public decimal Amount { get; set; }
        public int ResultId { get; set; }
    }
}