using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class AddVoteRequest : HttpRequestBase
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("isApproval")]
        public bool IsApproval { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
