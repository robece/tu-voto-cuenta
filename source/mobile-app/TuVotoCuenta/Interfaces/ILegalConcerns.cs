using System;
using System.Threading.Tasks;

namespace TuVotoCuenta.Interfaces
{
    public interface ILegalConcerns
    {
        Task<string> ReadLegalConcerns();
    }
}