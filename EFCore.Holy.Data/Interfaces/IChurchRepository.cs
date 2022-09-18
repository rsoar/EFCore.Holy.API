using EFCore.Holy.Data.Models;

namespace EFCore.Holy.Data.Interfaces
{
    public interface IChurchRepository
    {
        List<Church> ToList();

        void Add(Church newChurch);
        Church? FindById(int id);
    }
}
