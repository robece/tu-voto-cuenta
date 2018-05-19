using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Interfaces.Services;

namespace TuVotoCuenta.Services.MockServices
{
    public class SearchMockService : ISearchService
    {

        public async Task<IEnumerable<SearchResult>> SearchAsync(string entity, string municipality, string locality)
        {
            await Task.Delay(1000);
            var data = Helpers.LocalFilesHelper.ReadFileInPackage("data.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<SearchResult>>(data);
        }
    }
}
