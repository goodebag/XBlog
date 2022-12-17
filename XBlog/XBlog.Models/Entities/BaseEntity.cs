using System;
using System.Collections.Generic;
using System.Text;

namespace XBlog.Models.Entities
{
    public class BaseEntity
    {
        public string UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
