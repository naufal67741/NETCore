using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class University
    {
        [Key]
        public int UniversityId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }
    }
}