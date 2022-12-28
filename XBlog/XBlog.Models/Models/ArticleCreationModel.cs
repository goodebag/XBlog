using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Models
{
    public class ArticleCreationModel
    {
        public string Headline { get; set; }
        public string Body { get; set; }
        public string? ImageUrl { get; set; }
        public ProductModel? Product { get; set; }
        public Guid ArticleCategoryId { get; set; }
    }
}
