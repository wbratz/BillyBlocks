using System.Security.Cryptography;
using bc.ExtensionMethods;
using bc.Models;

namespace bc.HashingMethods
{
    public class HashSha256 : IHash
    {
        public string CalculateHash(string input)
        {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(input.ToByteArray());

            return hash.ConvertToString();
        }

        public string CalculateHash(Data input)
        {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(input.ToByteArray());

            return hash.ConvertToString();
        }
    }    
}