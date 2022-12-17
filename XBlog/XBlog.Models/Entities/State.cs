using System;
using System.Collections.Generic;

#nullable disable

namespace XBlog.Models.Entities
{
    public partial class State
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}
