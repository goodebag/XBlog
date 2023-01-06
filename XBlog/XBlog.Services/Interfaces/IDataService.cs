using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xblog.Data.Implementation;
using XBlog.Models.Entities;
using XBlog.Models.Models;

namespace XBlog.Services.Interfaces
{
    public interface IDataService
    {
        Task<Jwt> SignInAsync(SignInModel model);
        Task<User> CreateAuthur(SignUpModel model);
        Task<Guid> GetAuthurId(ClaimsPrincipal User);
        Task<string> GetUserId(ClaimsPrincipal User);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Reaction> AddOrModifyReationAsync(ReationModel model, string userId);
        Task<Coment> AddCommentsAsync(CommentModel model, string userId);
        Task<Product> ModifyProductAsync(ProductEditModel model, string userId);
        Task<Order> OrderProductAsync(IEnumerable<Guid> products, string userId);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task<bool> RemoveProductFromOrderAsync(Guid orderId, Guid productId);
        Task<bool> TrackArticle(Guid articleId);
        Task<bool> AddProductToOrderAsync(Guid orderId, Guid productId);
    }
}
