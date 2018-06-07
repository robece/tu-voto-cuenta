using MongoDB.Bson;

namespace TuVotoCuenta.Functions.Domain.Models.CosmosDB
{
    public class RecordVote
    {
        public ObjectId _id { get; set; }
        public string username { get; set; }
        public string hash { get; set; }
        public bool isApproval { get; set; }
        public string transactionId { get; set; }
        public string createdDate { get; set; }
    }
}