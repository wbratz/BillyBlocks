using bc.Models;

namespace bc.HashingMethods
{
    public interface IHash
    {
        string CalculateHash(string input);
        string CalculateHash(Data input);
    }
}