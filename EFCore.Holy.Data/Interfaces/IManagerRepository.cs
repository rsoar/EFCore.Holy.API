using EFCore.Holy.Data.Models;

namespace EFCore.Holy.Data.Interfaces
{
    public interface IManagerRepository
    {
        void Add(Manager data);
        void Delete(Manager manager);
        Manager? FindById(int id);
        Manager? FindByEmail(string email);
        List<Manager> FindAll();
    }
}
