using System.Threading.Tasks;
using TuVotoCuenta.iOS.Dependencies;
using TuVotoCuenta.Interfaces;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(LegalConcerns))]
namespace TuVotoCuenta.iOS.Dependencies
{
    public class LegalConcerns : ILegalConcerns
    {
        public async Task<string> ReadLegalConcerns()
        {
            string result = string.Empty;
            using (StreamReader sr = new StreamReader("LegalConcerns/LegalConcerns.txt"))
            {
                result = await sr.ReadToEndAsync();
            }
            return result;
        }
    }
}