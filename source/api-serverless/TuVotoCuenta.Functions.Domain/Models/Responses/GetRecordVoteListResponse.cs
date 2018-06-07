using System.Collections.Generic;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;

namespace TuVotoCuenta.Functions.Domain.Models.Responses
{
    public class GetRecordVoteListResponse
    {
        public bool IsSucceded { get; set; }
        public int ResultId { get; set; }
        public List<RecordVote> Votes { get; set; }
    }
}