using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBlog.Models.Entities;

namespace XBlog.Models.Models
{
    public class CommentModel
    {
        public string Body { get; set; }
        public Guid ActicleId { get; set; }
    }
}