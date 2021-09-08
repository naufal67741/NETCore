using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NETCore.Models;

namespace NETCore.Models
{
    public class Education
    {
        [Key]
        public int EducationId { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string GPA { get; set; }

        [Required]
        public int UniversityId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profiling> Profilings { get; set; }

        [JsonIgnore]
        public virtual University University { get; set; }

        public Education(string degree, string gPA)
        {
            Degree = degree;
            GPA = gPA;
        }

        public Education(string degree, string gPA, int universityId) : this(degree, gPA)
        {
            UniversityId = universityId;
        }
    }
}