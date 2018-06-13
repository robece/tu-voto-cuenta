using System;
using Newtonsoft.Json;

namespace TuVotoCuenta.Domain
{
    public class AddReportRequest : HttpRequestBase
    {
        [JsonProperty("recordItem")]
        public RecordItem RecordItem
        {
            get;
            set;
        }

    }
}
