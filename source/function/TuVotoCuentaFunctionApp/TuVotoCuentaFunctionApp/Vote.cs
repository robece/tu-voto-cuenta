using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using TuVotoCuentaFunctionApp.DTOs;

namespace TuVotoCuentaFunctionApp
{
    public static class Vote
    {
        [FunctionName(nameof(Vote))]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function,
            "post",
            Route = null)]RecordItem item,
            [DocumentDB("TuVotoCuenta", 
            "Votes", 
            CreateIfNotExists =true,
            Id = "id", 
            ConnectionStringSetting = "CosmosDbConnectionString")]IAsyncCollector<RecordItem> votes,
            TraceWriter log)
        {
            
            item.UID = Guid.NewGuid().ToString().Replace("-",string.Empty);
            item.CreatedDate = DateTime.Now.ToLongDateString();
            await votes.AddAsync(item);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
