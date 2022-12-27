using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xblog.Data.Interface;
using XBlog.Models.Entities;
using XBlog.Services.Interfaces;

namespace XBlog.Services.Implementations
{
    public class ArticleService: IArticleService
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Article>> GetAllArticleAsync(Guid authurId)
        {
            var articles = _UnitOfWork.GetRepository<Article>().GetQueryableList(x => x.AuthurId == authurId, include: x => x.Include(y => y.Coments).Include(y => y.Reactions).Include(x=>x.Product)).ToList();
            return articles;
        }
        public async Task<IEnumerable<Article>> GetArticlesByCategoryAsync(Guid categoryId)
        {
            var articles = _UnitOfWork.GetRepository<Article>().GetQueryableList(x => x.CategoryId == categoryId, include: x => x.Include(y => y.Coments).Include(y => y.Reactions).Include(x => x.Product)).ToList();
            return articles;
        }
        public async Task<IEnumerable<Article>> GetArticlesByAuthurAsync(string authur)
        {
            var articles = _UnitOfWork.GetRepository<Article>().GetQueryableList(x => x.Authur.Name == authur, include: x => x.Include(y => y.Coments).Include(y => y.Reactions).Include(x => x.Product)).ToList();
            return articles;
        }
        public async Task<IEnumerable<Article>> GetArticlesByHeadlineAsync(string headline)
        {
            var articles = _UnitOfWork.GetRepository<Article>().GetQueryableList(x => x.Headline.Contains(headline), include: x => x.Include(y => y.Coments).Include(y => y.Reactions).Include(x => x.Product)).ToList();
            return articles;
        }
    }
}
