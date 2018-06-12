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
        public Id Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("isApproval")]
        public bool IsApproval { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("createdDate")]
        public DateTimeOffset CreatedDate { get; set; }
    }

    public class Id
    {
        [JsonProperty("Timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("Machine")]
        public long Machine { get; set; }

        [JsonProperty("Pid")]
        public long Pid { get; set; }

        [JsonProperty("Increment")]
        public long Increment { get; set; }

        [JsonProperty("CreationTime")]
        public DateTimeOffset CreationTime { get; set; }
    }
}
