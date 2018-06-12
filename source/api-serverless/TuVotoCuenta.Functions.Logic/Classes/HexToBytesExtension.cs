using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace TuVotoCuenta.Functions.Logic.Classes
{
    public static class HexToBytesExtension
    {
        public static byte[] HexToBytes(this string hexEncodedBytes, int start, int end)
        {
            int length = end - start;
            const string tagName = "hex";
            string fakeXmlDocument = String.Format("<{1}>{0}</{1}>",
                                   hexEncodedBytes.Substring(start, length),
                                   tagName);
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(fakeXmlDocument));
            XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings());
            int hexLength = length / 2;
            byte[] result = new byte[hexLength];
            reader.ReadStartElement(tagName);
            reader.ReadContentAsBinHex(result, 0, hexLength);
            return result;
        }
    }
}