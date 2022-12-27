using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Models
{
    public class SignUpModel
    {
        [Required]
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Organisation { get; set; }
        public string Address { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Role { get; set; }
        public Gender Gender { get; set; }
    }
}
