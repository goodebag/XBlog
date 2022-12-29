using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Models
{
    public class ProductModel
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrlOne { get; set; }
        public string ImageUrlTwo { get; set; }
        public string ImageUrlThree { get; set; }
    }
    public class ProductEditModel
    {
        public bool? IsEnable { get; set; }
        public Guid ProductId { get; set; }
        public decimal? Price { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrlOne { get; set; }
        public string? ImageUrlTwo { get; set; }
        public string? ImageUrlThree { get; set; }
    }
}
