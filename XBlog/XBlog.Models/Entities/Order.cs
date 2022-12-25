using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBlog.Models.Models;

namespace XBlog.Models.Entities
{
    public class Order:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid? PaymentId { get; set; }
        public OrderStatus  OrderStatus { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
   
}
