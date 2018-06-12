using System;
namespace TuVotoCuenta.Domain
{
    public class AddVoteRequest : HttpRequestBase
    {
        public string username { get; set; }
        public bool isApproval { get; set; }
        public string hash { get; set; }
    }
}
