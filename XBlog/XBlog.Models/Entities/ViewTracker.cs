using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Entities
{
    public class ViewTracker:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid AuthurId { get; set; }
        public long ViewCount { get; set; }
        public long ViewSetlementCount { get; set; }
    }
}
