using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class Account
    {
        [Key] [Required]
        public string NIK { get; set; }
        [Required]
        public string Password { get; set; }

        public Person Person { get; set; }

        public Profiling Profiling { get; set; }
    }
}
