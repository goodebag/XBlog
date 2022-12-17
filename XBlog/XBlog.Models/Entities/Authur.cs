﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Entities
{
    public class Authur:BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }    
        public virtual ICollection<Article> Articles { get; set; }
    }
}
