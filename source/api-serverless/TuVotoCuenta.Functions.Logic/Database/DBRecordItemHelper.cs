using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuVotoCuenta.Functions.Domain.Models.CosmosDB;

namespace TuVotoCuenta.Functions.Logic.Database
{
    public class DBRecordItemHelper : DBHelper
    {
        private IMongoCollection<RecordItem> collection = null;

        public DBRecordItemHelper(MongoDBConnectionInfo databaseInfo) : base(databaseInfo)
        {
            collection = GetMongoDatabase().GetCollection<RecordItem>(GetMongoDBConnectionInfo().RecordItemCollection);
        }

        public async Task CreateRecordItem(RecordItem recordItem)
        {
            await collection.InsertOneAsync(recordItem);
        }

        public RecordItem GetRecordItem(string hash)
        {
            RecordItem result = null;
            try
            {
                result = collection.AsQueryable<RecordItem>().Where<RecordItem>(sb => sb.hash == hash).SingleOrDefault();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
            }
            return result;
        }

        public async Task<List<RecordItem>> GetRecordItemListAsync(string entity, string municipality, string locality)
        {
            List<RecordItem> result = null;
            try
            {
                var filter = new FilterDefinitionBuilder<RecordItem>().Eq<string>(record => record.entity, entity);
                filter = filter & new FilterDefinitionBuilder<RecordItem>().Eq<string>(record => record.municipality, municipality);
                filter = filter & new FilterDefinitionBuilder<RecordItem>().Eq<string>(record => record.locality, locality);
                result = await collection.FindSync<RecordItem>(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceWarning($"EXCEPTION: {ex.Message}. STACKTRACE: {ex.StackTrace}");
            }
            return result;
        }
    }
}