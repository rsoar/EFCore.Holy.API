﻿using EFCore.Holy.Business.Handling;
using EFCore.Holy.Business.Services;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models;
using EFCore.Holy.Data.Models.DTO;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EFCore.Holy.Business
{
    public class ManagerBusiness : IManagerBusiness
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IManagerRepository _managerRepository;
        private readonly IChurchBusiness _churchBusiness;
        public ManagerBusiness(
            IHttpContextAccessor httpContext,
            IManagerRepository repository,
            IChurchBusiness churchBusiness)
        {
            _httpContext = httpContext;
            _managerRepository = repository;
            _churchBusiness = churchBusiness;
        }

        public async Task<bool> Add(CreateManager data)
        {
            var userChurchId = TokenService.GetProperty(_httpContext.HttpContext.User.Claims, "churchId");
            var userRole = TokenService.GetProperty(_httpContext.HttpContext.User.Claims, "role");

            if (!RoleService.IsShepherd(int.Parse(userRole.Value)))
                throw new HttpException(401, Error.DoesHavePermission);

            Church church = _churchBusiness.FindById(data.IdChurch);

            if (church.Id != int.Parse(userChurchId.Value) && !RoleService.IsShepherd(data.Role))
                throw new HttpException(401, Error.DoesHavePermission);

            if (!MailService.IsValid(data.Email))
                throw new HttpException(400, Error.InvalidMail);

            if (!RoleService.IsValidRole(data.Role))
                throw new HttpException(400, Error.InvalidRole);

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

        public void Delete(int id)
        {
            Manager managerById = FindById(id);

            _managerRepository.Delete(managerById);
        }

        public List<Manager> FindAll()
        {
            return _managerRepository.FindAll();
        }

        public Manager FindById(int id)
        {
            Manager? managerbyId = _managerRepository.FindById(id);

            if (managerbyId == null)
                throw new HttpException(404, Error.ManagerNotFound);

            return managerbyId;
        }

        public string Login(Login data)
        {
            if (!MailService.IsValid(data.Email))
                throw new HttpException(400, Error.InvalidMail);

            var managerByMail = _managerRepository.FindByEmail(data.Email);

            if (managerByMail == null)
                throw new HttpException(400, Error.InvalidCredentials);

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(data.Password, managerByMail.Password);

            if (!isValidPassword)
                throw new HttpException(400, Error.InvalidCredentials);

            return TokenService.GenerateToken(managerByMail);
        }
    }
}
