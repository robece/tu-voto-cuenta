using System;
using System.IO;
using System.IO.Compression;

namespace TuVotoCuenta.Helpers
{
    public class CompressionHelper
    {
        public static byte[] CompressToMemory(byte[] fileToCompress)
        {
            byte[] result = null;

            using (MemoryStream source = new MemoryStream(fileToCompress))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (GZipStream compressionStream = new GZipStream(ms, CompressionMode.Compress))
                    {
                        source.CopyTo(compressionStream);
                    }

                    result = ms.ToArray();
                }
            }

            return result;
        }

        public static byte[] DecompressToMemory(byte[] fileToDecompress)
        {
            byte[] result = null;
            try
            {
                using (MemoryStream source = new MemoryStream(fileToDecompress))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (GZipStream decompressionStream = new GZipStream(source, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(ms);
                        }

                        result = ms.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                var a = 0;
            }

            return result;
        }
    }
}
