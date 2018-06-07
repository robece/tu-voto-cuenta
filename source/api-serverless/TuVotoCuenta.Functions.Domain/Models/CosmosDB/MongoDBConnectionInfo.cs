namespace TuVotoCuenta.Functions.Domain.Models.CosmosDB
{
    public class MongoDBConnectionInfo
    {
        public string ConnectionString { get; set; }
        public string DatabaseId { get; set; }
        public string UserAccountCollection { get; set; }
        public string RecordItemCollection { get; set; }
        public string RecordVoteCollection { get; set; }
    }
}