using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Entities
{
    public class OrderDetail : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
