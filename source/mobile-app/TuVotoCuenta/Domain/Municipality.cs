using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class Municipality
    {
        [JsonProperty("eId")]
        public byte EntityId { get; set; }
        [JsonProperty("mID")]
        public int MunicipalityId { get; set; }
        [JsonProperty("mName")]
        public string MunicipalityName { get; set; }

        public override string ToString()
        {
            return MunicipalityName;
        }
    }
}
