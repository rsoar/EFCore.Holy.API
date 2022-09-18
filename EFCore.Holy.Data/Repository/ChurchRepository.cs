using EFCore.Holy.Data.Context;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models;

namespace EFCore.Holy.Data.Repository
{
    public class ChurchRepository : IChurchRepository
    {
        private readonly DatabaseContext _dbContext;

        public ChurchRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Church> ToList()
        {
            return _dbContext.Churchs.ToList();
        }

        public void Add(Church newChurch)
        {
            _dbContext.Churchs.Add(newChurch);
            _dbContext.SaveChanges();
        }

        public Church? FindById(int id)
        {
            return _dbContext.Churchs.Find(id);
        }
    }
}
