using EFCore.Holy.Data.Models;
using EFCore.Holy.Data.Models.DTO;

namespace EFCore.Holy.Data.Interfaces
{
    public interface IChurchBusiness
    {
        List<Church> ToList();
        Church FindById(int id);
        bool Add(NewChurch church);
    }
}
