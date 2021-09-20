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
    public class GenderVM
    {
        public enum Gender
        {
            Male,
            Female
        }

        [Range(0, 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }

        public int GenderCounter { get; set; }
        public int MaleCounter { get; set; }
        public int FemaleCounter { get; set; }
    }
}
