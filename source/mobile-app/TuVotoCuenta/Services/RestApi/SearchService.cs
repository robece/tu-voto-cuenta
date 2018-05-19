using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;
using TuVotoCuenta.Interfaces.Services;

namespace TuVotoCuenta.Services.RestApi
{
    public class SearchService : ISearchService
    {
        public SearchService()
        {
        }

        public Task<IEnumerable<SearchResult>> SearchAsync(string entity, string municipality, string locality)
        {
            throw new NotImplementedException();
        }
    }
}
