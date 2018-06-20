using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class GetRecordVoteListResponse : HttpResponseBase
    {

        [JsonProperty("votes")]
        public Vote[] Votes { get; set; }
    }

    public class Vote
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("isApproval")]
        public bool IsApproval { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }


}
