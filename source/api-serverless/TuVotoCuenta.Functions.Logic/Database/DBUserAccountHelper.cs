using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;

namespace TuVotoCuenta.Functions.Logic.Database
{
    public class DBUserAccountHelper : DBHelper
    {
        private IMongoCollection<UserAccount> collection = null;

        public DBUserAccountHelper(MongoDBConnectionInfo databaseInfo) : base(databaseInfo)
        {
            collection = GetMongoDatabase().GetCollection<UserAccount>(GetMongoDBConnectionInfo().UserAccountCollection);
        }

        public async Task CreateUserAccount(UserAccount userAccount)
        {
            await collection.InsertOneAsync(userAccount);
        }

        public UserAccount GetUser(string username)
        {
            UserAccount result = null;
            try
            {
                result = collection.AsQueryable<UserAccount>().Where<UserAccount>(sb => sb.username == username).SingleOrDefault();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
            }
            return result;
        }
    }
}