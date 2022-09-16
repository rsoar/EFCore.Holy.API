using EFCore.Holy.Data.Models;

namespace EFCore.Holy.Data.Interfaces
{
    public interface IChurchBusiness
    {
        List<Church> ToList();
        Church FindById(int id);
        bool Add(Church church);
    }
}
