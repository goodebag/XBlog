using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using XBlog.Models.Models;

namespace XBlog.Models.Entities
{
    public class User:IdentityUser
    {
        public string FullName { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public Role Role { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string? Organisation { get; set; }
        public string? OrganisationId { get; set; }
        public string? OrganisationRole { get; set; }
    }
}
