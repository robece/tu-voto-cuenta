using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuVotoCuenta.Domain;

namespace TuVotoCuenta.Interfaces.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchResult>> SearchAsync(string entity, string municipality, string locality);
    }
}
