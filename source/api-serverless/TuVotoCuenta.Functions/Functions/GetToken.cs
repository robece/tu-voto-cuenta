using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Linq;
using TuVotoCuenta.Functions.Logic.Helpers;

namespace TuVotoCuenta.Functions.Functions
{
    public static class GetToken
    {
        [FunctionName("GetToken")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            token = SecurityHelper.Encrypt(token, Settings.SECURITY_SEED);

            return (ActionResult)new OkObjectResult($"Your token: {token}");
        }
    }
}