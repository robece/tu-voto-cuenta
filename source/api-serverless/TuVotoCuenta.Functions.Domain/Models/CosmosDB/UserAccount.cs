using MongoDB.Bson;

namespace TuVotoCuenta.Functions.Domain.Models.CosmosDB
{
    public class UserAccount
    {
        public ObjectId _id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string createdDate { get; set; }
    }
}