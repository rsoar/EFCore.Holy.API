using EFCore.Holy.Data.Context;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models;

namespace EFCore.Holy.Data.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly DatabaseContext _dbContext;
        public ManagerRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Manager> FindAll()
        {
            return _dbContext.Managers.ToList();
        }

        public void Add(Manager data)
        {
            _dbContext.Managers.Add(data);
            _dbContext.SaveChanges();
        }

        public Manager? FindById(int id)
        {
            return _dbContext.Managers.Find(id);
        }

        public Manager? FindByEmail(string email)
        {
            return _dbContext.Managers.FirstOrDefault(m => m.Email == email);
        }

        public void Delete(Manager manager)
        {
            _dbContext.Managers.Remove(manager);
            _dbContext.SaveChanges();
        }
    }
}
