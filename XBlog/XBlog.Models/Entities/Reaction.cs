using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBlog.Models.Models;

namespace XBlog.Models.Entities
{
    public class Reaction:BaseEntity
    {
            public Guid Id { get; set; }
            public ReactionType ReactionType{ get; set; }
            public Guid ActicleId { get; set; }
            public virtual User User { get; set; }
            public string UserId { get; set; }
    }
}
