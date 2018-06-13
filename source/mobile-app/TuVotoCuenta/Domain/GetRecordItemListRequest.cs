using System;
namespace TuVotoCuenta.Domain
{
    public class GetRecordItemListRequest : HttpRequestBase
    {
        public string entity { get; set; }
        public string municipality { get; set; }
        public string locality { get; set; }
    }
}
