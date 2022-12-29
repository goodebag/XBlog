using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Entities
{
    public class Coment:BaseEntity
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public Guid ActicleId { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
