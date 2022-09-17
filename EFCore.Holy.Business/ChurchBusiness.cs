using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models;
using EFCore.Holy.Data.Models.DTO;

namespace EFCore.Holy.Business
{
    public class ChurchBusiness : IChurchBusiness
    {
        private readonly IChurchRepository _repository;
        public ChurchBusiness(IChurchRepository churchRepository)
        {
            _repository = churchRepository;
        }

        public bool Add(NewChurch data)
        {
            Church church = new()
            {
                Name = data.Name,
                IsCongregation = !!data.IsCongregation
            };

            _repository.Add(church);

            return true;
        }

        public Church FindById(int id)
        {
            return _repository.FindById(id);
        }

        public List<Church> ToList()
        {
            return _repository.ToList();
        }
    }
}