using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendCase.Models
{
    public class Information
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[^@]+@[^@]+\.[^@]+$", ErrorMessage = "Not a valid email adress, an email must be formatted like this: localpart@domain.any")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(0047|\+47|47)?\d{8}$", ErrorMessage = "Phone number must be a valid norwegian number starting with either: 0047, +47, 47 or nothing, followed by 8 digits")]
        public string Phone { get; set; }
        [Required]
        [AreaCode]
        public int Areacode { get; set; }
        [MaxLength(1000)]
        public string Comment { get; set; }
        public string Honeypot { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
