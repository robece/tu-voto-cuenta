using System.Collections.Generic;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;

namespace TuVotoCuenta.Functions.Domain.Models.Responses
{
    public class GetRecordVoteCountResponse
    {
        public bool IsSucceded { get; set; }
        public int ResultId { get; set; }
        public int Approvals { get; set; }
        public int Disapprovals { get; set; }
    }
}