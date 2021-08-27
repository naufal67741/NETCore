using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        [Required]
        public int MyProperty { get; set; }
        [Required]
        public int University_Id { get; set; }
        public ICollection<Profiling> Profilings { get; set; }

        public University University{ get; set; }
    }
}
