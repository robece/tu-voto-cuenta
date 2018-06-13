using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TuVotoCuenta.Helpers
{
	public class LocalFilesHelper
    {

        static string DEFAULTPATH = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);

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

        public static string ReadCompressedFileInPackage(string name)
        {
            string fileContent = string.Empty;

            var assembly = typeof(LocalFilesHelper).GetTypeInfo().Assembly;

            var resources = assembly.GetManifestResourceNames();

            var resourceName = assembly.GetManifestResourceNames().Where(r => r.ToLowerInvariant().EndsWith(name.ToLowerInvariant(), StringComparison.Ordinal)).FirstOrDefault();

            if (resourceName != default(string))
            {
                using (Stream fileStream = assembly.GetManifestResourceStream(resourceName))
                {
                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);

                        var decompressed = Helpers.CompressionHelper.DecompressToMemory(ms.ToArray());

                        fileContent = Encoding.UTF8.GetString(decompressed, 0, decompressed.Length);
                        fileContent = fileContent.Remove(0, 1);
                    }
                }
            }

            return fileContent;
        }


        public static void SaveFile(string fileName, byte[] data)
        {
            var filePath = Path.Combine(DEFAULTPATH, fileName);
            File.WriteAllBytes(filePath, data);
        }

        public static byte[] ReadFile(string fileName)
        {
            var filePath = Path.Combine(DEFAULTPATH, fileName);
            if (!File.Exists(filePath))
                return null;
            return File.ReadAllBytes(filePath);
        }

        public static void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(DEFAULTPATH, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


        public static string GetFileHexString(string fileName)
        {
            var file = ReadFile(fileName);
            return BitConverter.ToString(file).Replace("-", "");
        }

    }
}
