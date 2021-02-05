using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using bc.Models;

namespace bc.PowFunctions
{
    public class Challenger
    {
    }

    public class Miner
    {
        public long nonce = 0;
        public bool isFound;

        public async Task<string> MineBlock(int difficulty, Data data)
        {
            var comparison = new string('0', difficulty + 1);
            var sw = new Stopwatch();

            var hash = await GetBlockHashAsync(data);

            sw.Start();

            var threads = 7;

            var taskList = new List<Task<string>>();

            for (int i = 0; i < threads; i++)
            {
                taskList.Add(Task.Run(() => Mine(difficulty, hash, comparison)));
            }

            var finishedTask = await Task.WhenAny(taskList);

            sw.Stop();
            isFound = true;
            hash = await finishedTask;

            System.Console.WriteLine($"{sw.Elapsed}");

            return hash;
        }

        private string Mine(int difficulty, string hash, string comparison)
        {
            while (hash.Substring(0, difficulty + 1) != comparison && !isFound)
            {
                nonce++;
                hash = CalculateHash(hash, nonce.ToString());
            }

            return hash;
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
                result += $"{array[i]:X2}";
            }

            return result;
        }

        internal byte[] ToByteArray(string input0, string input1)
        {
            var xs = new XmlSerializer(typeof(string));
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, input0 + input1);
                return ms.ToArray();
            }
        }

        private async Task<string> GetBlockHashAsync(Data data)
        {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(data.ToByteArray());

            return ConvertToString(hash);
        }
    }

    internal static class DataExtensions
    {
        internal static byte[] ToByteArray(this Data data)
        {
            var xs = new XmlSerializer(typeof(Data));
            using (var ms = new MemoryStream())
            {
                xs.Serialize(ms, data);
                return ms.ToArray();
            }
        }
    }
}