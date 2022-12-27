using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XBlog.Models.Entities;

namespace XBlog.Services.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllArticleAsync(Guid authurId);
        Task<IEnumerable<Article>> GetArticlesByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Article>> GetArticlesByAuthurAsync(string authur);
        Task<IEnumerable<Article>> GetArticlesByHeadlineAsync(string headline);
    }
}
