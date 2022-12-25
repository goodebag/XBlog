using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Entities
{
    public class Article:BaseEntity
    {
        public Guid Id { get; set; }
        public string Headline { get; set; }
        public string Body { get; set; }
        public string HeadlineImageUrl { get; set; }
        public Guid AuthurId { get; set; }
        public virtual Authur  Authur { get; set; }
        public virtual ICollection<Coment> Coments { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
    }
}
