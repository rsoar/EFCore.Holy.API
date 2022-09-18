using EFCore.Holy.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Holy.Data.Interfaces
{
    public interface IMailService
    {
        bool IsValid(string email);
        Task<bool> SendAsync(MailData mailData, CancellationToken ct);
    }
}
