﻿using EFCore.Holy.Data.Models;
using EFCore.Holy.Data.Models.DTO;

namespace EFCore.Holy.Data.Interfaces
{
    public interface IManagerBusiness
    {
        Task<bool> Add(CreateManager data);
        void Delete(int id);
        Manager FindById(int id);
        List<Manager> FindAll();
        string Login(Login login);
    }
}
