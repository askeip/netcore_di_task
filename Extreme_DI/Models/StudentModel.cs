using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Extreme_DI.Models
{
    public class StudentModel
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int StudentID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [Range(1, 8)]
        public int Course { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Group { get; set; }
    }
}
