using EFCore.Holy.Business.Handling;
using EFCore.Holy.Business.Services;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models;
using EFCore.Holy.Data.Models.DTO;

namespace EFCore.Holy.Business
{
    public class ManagerBusiness : IManagerBusiness
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IChurchBusiness _churchBusiness;
        public ManagerBusiness(IManagerRepository repository, IChurchBusiness churchBusiness)
        {
            _managerRepository = repository;
            _churchBusiness = churchBusiness;
        }

        public async Task<bool> Add(CreateManager data)
        {
            Church church = _churchBusiness.FindById(data.IdChurch);

            if (!MailService.IsValid(data.Email))
                throw new HttpException(400, Error.InvalidMail);

            var manager = _managerRepository.FindByEmail(data.Email);

            if (manager is not null)
                throw new HttpException(400, Error.ManagerAlreadyExists);

            string newPassword = GenerateService.Str(16);

            if (RoleService.IsSecretary(data.Role) || RoleService.IsTreasurer(data.Role))
                data.Password = newPassword;

            string salt = BCrypt.Net.BCrypt.GenerateSalt(8);
            string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(data.Password, salt);

            Manager managerToCreate = new()
            {
                IdChurch = church.Id,
                Name = data.Name,
                Email = data.Email,
                Password = encryptedPassword,
                Role = data.Role
            };

            _managerRepository.Add(managerToCreate);

            if (RoleService.IsSecretary(data.Role) || RoleService.IsTreasurer(data.Role))
            {
                List<string> _to = new() { data.Email };
                var mail = new MailData(
                    to: _to,
                    subject: "Holy App",
                    body: @$"E-mail: {data.Email} Password: {newPassword}"
                    )
                { };
                var result = await MailService.SendAsync(mail, new CancellationToken());

                if (!result)
                    throw new HttpException(500, Error.FailedSendEmail);
            }
            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Manager> FindAll()
        {
            return _managerRepository.FindAll();
        }

        public Manager FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
