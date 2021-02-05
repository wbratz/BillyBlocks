using System.IO;
using System.Xml.Serialization;
using bc.Models;

namespace bc.ExtensionMethods
{
    public static class MiscExtensions
    {
        internal static byte[] ToByteArray(this string input)
        {
            var xs = new XmlSerializer(typeof(string));
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, input);
                return ms.ToArray();
            }
        }

        internal static byte[] ToByteArray(this Data input)
        {
            var xs = new XmlSerializer(typeof(Data));
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, input);
                return ms.ToArray();
            }
        }

        internal static string ConvertToString(this byte[] array)
        {
            var result = "";

            for (int i = 0; i < array.Length; i++)
            {
                result += $"{array[i]:X2}";
            }

            return result;
        }
    }
}