using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class AccountRole
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string NIK { get; set; }
        public virtual Account Account { get; set; }

    }
}
