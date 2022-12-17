using System;
using System.Collections.Generic;

#nullable disable

namespace XBlog.Models.Entities
{
    public partial class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public bool? Activated { get; set; }

    }
}
