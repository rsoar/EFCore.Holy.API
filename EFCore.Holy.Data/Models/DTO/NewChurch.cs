using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Holy.Data.Models.DTO
{
    public class NewChurch
    {
        public string Nome { get; set; }
        public bool? IsCongregation { get; set; }
    }
}
