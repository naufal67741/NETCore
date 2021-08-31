﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Education> Educations { get; set; }
    }
}