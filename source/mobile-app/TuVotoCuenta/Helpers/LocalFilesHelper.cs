using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TuVotoCuenta.Helpers
{
    public class LocalFilesHelper
    {
        public static string ReadFileInPackage(string name)
        {
            string fileContent = string.Empty;

            var assembly = typeof(LocalFilesHelper).GetTypeInfo().Assembly;

            var resourceName = assembly.GetManifestResourceNames().Where(r => r.ToLowerInvariant().EndsWith(name.ToLowerInvariant(), StringComparison.Ordinal)).FirstOrDefault();

            if (resourceName != default(string))
            {
                using (Stream fileStream = assembly.GetManifestResourceStream(resourceName))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    using (var fileReader = new StreamReader(fileStream))
                    {
                        fileContent = fileReader.ReadToEnd();
                    }
                }
            }

            return fileContent;
        }
    }
}
