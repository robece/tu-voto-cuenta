using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content.Res;
using TuVotoCuenta.Droid.Dependencies;
using TuVotoCuenta.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(LegalConcerns))]
namespace TuVotoCuenta.Droid.Dependencies
{
    public class LegalConcerns : ILegalConcerns
    {
        public async Task<string> ReadLegalConcerns()
        {
            string result = string.Empty;
            using (StreamReader sr = new StreamReader(Android.App.Application.Context.Assets.Open("LegalConcerns.html")))
            {
                result = await sr.ReadToEndAsync();
            }
            return result;
        }
    }
}