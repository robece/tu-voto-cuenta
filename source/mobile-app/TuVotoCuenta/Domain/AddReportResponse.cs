using System;
namespace TuVotoCuenta.Domain
{
    public class AddReportResponse : HttpResponseBase
    {
        public bool IsSucceded { get; set; }
        public int ResultId { get; set; }
    }
}
