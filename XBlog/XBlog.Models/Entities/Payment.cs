using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBlog.Models.Models;

namespace XBlog.Models.Entities
{
    public class Payment:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public PaymentGateway Gateway { get; set; }
        public PaymentType  PaymentType { get; set; }
        public Status Status { get; set; }
        public string Reference { get; set; }
    }
}
