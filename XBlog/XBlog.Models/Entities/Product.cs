using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBlog.Models.Models;

namespace XBlog.Models.Entities
{
    public class Product:BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrlOne { get; set; }
        public string ImageUrlTwo { get; set; }
        public string ImageUrlThree { get; set; }
        public string Discription { get; set; }
        public Guid ActicleId { get; set; }
        public Guid SellerId { get; set; }
        public Rating ProductRating { get; set; }
        public virtual Authur Seller { get; set; }
        public virtual Article Article { get; set; }
    }
}
