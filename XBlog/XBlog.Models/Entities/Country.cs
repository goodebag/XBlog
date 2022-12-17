using System;
using System.Collections.Generic;

#nullable disable

namespace XBlog.Models.Entities
{
    public  class Country
    {
        public int CountryId { get; set; }
        public string Sortname { get; set; }
        public string Name { get; set; }
        public int Phonecode { get; set; }

    }
}
