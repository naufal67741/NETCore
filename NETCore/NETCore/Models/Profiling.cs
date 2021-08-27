using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public int Education_Id { get; set; }

        public Account Account { get; set; }
        public Education Education { get; set; }
    }
}
