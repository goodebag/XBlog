using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBlog.Models.Models;

namespace XBlog.Models.Entities
{
    public class Authur:BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public Rating Rating { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
