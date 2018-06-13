using System;
namespace TuVotoCuenta.Domain
{
    public class GetRecordVoteCountResponse : HttpResponseBase
    {
		public int Approvals { get; set; }

        public int Disapprovals { get; set; }
    }
}
