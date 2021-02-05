using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;

namespace bc.PowFunctions
{
    public class Challenger
    {
        
    }

    public class Miner
    {
        public int nonce = 0;
        public void MineBlock(int difficulty, string hash)
        {
            var comparison = new string('0', difficulty + 1);
            var key = hash.Substring(0, difficulty);
            var sw = new Stopwatch();
            sw.Start();
            while(hash.Substring(0, difficulty) != comparison)
            {
                nonce++;
                hash = CalculateHash(hash, nonce.ToString());
            }
            sw.Stop();

            System.Console.WriteLine($"{sw.Elapsed}");
        }

        private string CalculateHash(string input0, string input1)
        {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(ToByteArray(input0, input1));

            return ConvertToString(hash);
        }

        private string ConvertToString(byte[] array)
        {
            var result = "";

            for (int i = 0; i < array.Length; i++)
            {
                result+=$"{array[i]:X2}";
            }

            return result;
        }

        internal byte[] ToByteArray(string input0, string input1)
        {
            var xs = new XmlSerializer(typeof(string));
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, input0+input1);
                return ms.ToArray();
            }
        }
    }
}