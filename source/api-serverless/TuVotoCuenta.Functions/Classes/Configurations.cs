using TuVotoCuenta.Functions.Domain.Models.CosmosDB;

namespace TuVotoCuenta.Functions.Classes
{
    public class Configurations
    {
        public static MongoDBConnectionInfo GetMongoDbConnectionInfo()
        {
            MongoDBConnectionInfo mongoDbConnectionInfo = new MongoDBConnectionInfo()
            {
                ConnectionString = Settings.COSMOSDB_CONNECTIONSTRING,
                DatabaseId = Settings.COSMOSDB_DATABASEID,
                UserAccountCollection = Settings.COSMOSDB_USERACCOUNTCOLLECTION,
                RecordItemCollection = Settings.COSMOSDB_RECORDITEMCOLLECTION,
                RecordVoteCollection = Settings.COSMOSDB_RECORDVOTECOLLECTION,
            };
            return mongoDbConnectionInfo;
        }
    }
}