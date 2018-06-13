using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class Locality
    {
        [JsonProperty("mId")]
        public int MunicipalityId { get; set; }
        [JsonProperty("lId")]
        public int LocalityId { get; set; }
        [JsonProperty("lName")]
        public string LocalityName { get; set; }

        public override string ToString()
        {
            return LocalityName;
        }
    }
}
