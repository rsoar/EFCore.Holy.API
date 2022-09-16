using EFCore.Holy.Data.Context;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models;

namespace EFCore.Holy.Data.Repository
{
    public class ChurchRepository : IChurchRepository
    {
        private readonly DatabaseContext _context;
        public ChurchRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<Church> ToList()
        {
            return _context.Churchs.ToList();
        }

        public void Add(Church newChurch)
        {
            _context.Churchs.Add(newChurch);
            _context.SaveChanges();
        }

        public Church FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
