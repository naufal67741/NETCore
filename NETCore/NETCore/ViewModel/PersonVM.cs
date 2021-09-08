using NETCore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.ViewModel
{
    public class PersonVM
    {
        [Required]
        public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        /*public string RoleName { get; set; }*/
        public ICollection<AccountRole> AccountRoles { get; internal set; }
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(1000000, 100000000)]
        public int Salary { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public enum Gender
        {
            Male,
            Female
        }

        [Range(0, 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { internal get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string GPA { get; set; }

        [Required]
        public int UniversityId { get; set; }
    }
}
