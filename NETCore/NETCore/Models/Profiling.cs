using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class Profiling
    {
        [Key] //anotasi primari key
        public string NIK { get; set; }


        [Required]
        [Range(1, 100000)]
        public int EducationId { get; set; }


        [Newtonsoft.Json.JsonIgnore]
        public virtual Account Account { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Education Education { get; set; }

        public Profiling(string nIK, int educationId)
        {
            NIK = nIK;
            EducationId = educationId;
        }
    }

}