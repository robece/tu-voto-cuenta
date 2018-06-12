using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class GetRecordVoteListRequest : HttpRequestBase
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
