using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xblog.Data.Interface;
using XBlog.Models.Entities;
using XBlog.Models.Models;
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
        public async Task<Article> CreateArticleAsync(ArticleCreationModel model, Guid authurId)
        {

                DateTime time = DateTime.UtcNow;
                Article article = new Article()
                {
                    AuthurId = authurId,
                    Headline = model.Headline,
                    Body = model.Body,
                    HeadlineImageUrl = model.ImageUrl,
                    CreatedAt = time,
                    UpdatedAt = time,
                    CategoryId = model.ArticleCategoryId
                };
                article= await  _UnitOfWork.GetRepository<Article>().AddAsync(article);
                if (model.Product is not null)
                {
                    Product  product = new Product()
                    {
                        Name = model.Product.Name,
                        Discription =model.Product.Description,
                        CreatedAt = time,
                        UpdatedAt=time,
                        ActicleId = article.Id,
                        SellerId = authurId,
                        Price= model.Product.Price,
                    };
                 product = await _UnitOfWork.GetRepository<Product>().AddAsync(product);
                 article.ProductId = product.Id;
                 await _UnitOfWork.GetRepository<Product>().UpdateAsync(product);

                }
                return article;
        }
        public async Task<Article> GetArticleByIdAsync(Guid articleId)
        {
            var article = _UnitOfWork.GetRepository<Article>().GetQueryableList(x => x.Id == articleId, include: x => x.Include(y => y.Coments).Include(y => y.Reactions).Include(x => x.Product)).FirstOrDefault();
            return article;
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
