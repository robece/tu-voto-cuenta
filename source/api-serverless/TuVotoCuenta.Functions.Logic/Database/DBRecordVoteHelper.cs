using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;

namespace TuVotoCuenta.Functions.Logic.Database
{
    public class DBRecordVoteHelper : DBHelper
    {
        private IMongoCollection<RecordVote> collection = null;

        public DBRecordVoteHelper(MongoDBConnectionInfo databaseInfo) : base(databaseInfo)
        {
            collection = GetMongoDatabase().GetCollection<RecordVote>(GetMongoDBConnectionInfo().RecordVoteCollection);
        }

        public async Task CreateRecordVote(RecordVote recordVote)
        {
            await collection.InsertOneAsync(recordVote);
        }

        public RecordVote GetRecordVote(string hash, string username)
        {
            RecordVote result = null;
            try
            {
                result = collection.AsQueryable<RecordVote>().Where<RecordVote>(sb => sb.hash == hash && sb.username == username).SingleOrDefault();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
            }
            return result;
        }

        public async Task<List<RecordVote>> GetRecordVoteList(string hash)
        {
            List<RecordVote> result = null;
            try
            {
                var filter = new FilterDefinitionBuilder<RecordVote>().Eq<string>(vote => vote.hash, hash);
                result = await collection.FindSync<RecordVote>(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
            }
            return result;
        }
    }
}