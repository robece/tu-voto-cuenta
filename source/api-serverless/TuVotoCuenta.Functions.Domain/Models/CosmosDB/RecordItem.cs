using MongoDB.Bson;

namespace TuVotoCuenta.Functions.Domain.Models.CosmosDB
{
    public class RecordItem : Base.RecordItem
    {
        public ObjectId _id { get; set; }
        public string transactionId { get; set; }
        public string createdDate { get; set; }
    }
}