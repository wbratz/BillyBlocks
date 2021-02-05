using System.Threading.Tasks;
using bc.Models;

namespace bc.Retievers
{
    public interface IRetrieveData
    {
        Task<Data> GetData();
    }
}