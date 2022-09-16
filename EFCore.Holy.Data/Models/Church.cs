using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Holy.Data.Models
{
    public class Church
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsCongregation { get; set; } 
    }
}
