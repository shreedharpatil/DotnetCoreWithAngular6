using Data.Repository.Models;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface ITransformerRepository
    {
        Task AddTransformer(string type, string typeId, string feederId, Transformer transformer);
    }
}
