using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class GetRecordVoteCountRequest : HttpRequestBase
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
