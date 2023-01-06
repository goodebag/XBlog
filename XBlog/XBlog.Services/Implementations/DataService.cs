
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xblog.Data.Implementation;
using Xblog.Data.Interface;
using XBlog.Models.Entities;
using XBlog.Models.Models;
using XBlog.Services.Interfaces;

namespace XBlog.Services.Implementations
{
    public class DataService : IDataService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly JwtAuthenticator _jwtAuthenticator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DataService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, JwtAuthenticator jwtAuthenticator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _UnitOfWork = unitOfWork;
            _jwtAuthenticator = jwtAuthenticator;
        }
        public async Task<User> CreateAuthur(SignUpModel model)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(model.EmailAddress);
                if (user == null)
                {
                    user = new User
                    {
                        FullName = $"{model.LastName}, {model.FirstName}",
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address is not null ? model.Address : "",
                        Email = model.EmailAddress,
                        UserName = model.EmailAddress,
                        PhoneNumber = model.PhoneNo,
                        Role = Role.Authur,
                        Organisation = model.Organisation is not null ? model.Organisation : "",
                        OrganisationRole = model.Role is not null ? model.Role : "",
                        Gender= model.Gender.ToString(),
                        Title = model.Gender is Gender.Male ? "Male" : "Female",
                    };
                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.FirstOrDefault().Description);
                    }
                    user = await _userManager.FindByNameAsync(model.EmailAddress);
                    var roles = await _roleManager.FindByNameAsync(Role.Authur.ToString());
                    if (roles == null)
                    {
                        await _roleManager.CreateAsync(new IdentityRole { Name = Role.Authur.ToString() });
                    }
                    await _userManager.AddToRoleAsync(user, Role.Authur.ToString());


                    Authur authur = await _UnitOfWork.GetRepository<Authur>().GetSingleByAsync(p => p.UserId == user.Id);
                    authur = new Authur()
                    {
                        Name = user.FullName,
                        UserName = model.EmailAddress,
                        UserId = user.Id
                    };
                    authur = await _UnitOfWork.GetRepository<Authur>().AddAsync(authur);

                }
                else
                {
                    throw new Exception("User with email already exist");
                }



                return user;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<Jwt> SignInAsync(SignInModel model)
        {
            User user = await _userManager.FindByNameAsync(model.Email);
            if (user is null)
            {
                throw new Exception("User not found");
            }
              bool passwordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordCorrect)
                {
                    throw new Exception("Password is incorrect");
                }
            Jwt tokenPayload =await _jwtAuthenticator.GenerateJwtToken(user);
            return tokenPayload;

        }
       
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = _UnitOfWork.GetRepository<Category>().GetAll();
            return categories;
        }
        public async Task<Guid> GetAuthurId(ClaimsPrincipal User)
        {
           string identityUserId = _userManager.GetUserId(User);
            Guid authurId = _UnitOfWork.GetRepository<Authur>().GetSingleBy(x=>x.UserId==identityUserId).Id;
            return authurId;
        }
        public async Task<string> GetUserId(ClaimsPrincipal User)
        {
            string identityUserId = _userManager.GetUserId(User);
          
            return identityUserId;
        }
        public async Task<Coment> AddCommentsAsync(CommentModel model, string userId)
        {
            var time = DateTime.UtcNow;
            var articles = await _UnitOfWork.GetRepository<Coment>().AddAsync(new Coment
            {
                UserId = userId,
                Body = model.Body,
                ActicleId = model.ActicleId,
                CreatedAt = time,
                UpdatedAt = time,
            });
            return articles;
        }
        public async Task<Reaction> AddOrModifyReationAsync(ReationModel model, string userId)
        {
            var reation = await _UnitOfWork.GetRepository<Reaction>().GetSingleByAsync(x => x.ActicleId == model.ActicleId && x.UserId == userId, disableTracking: false);
            var time = DateTime.UtcNow;
            if (reation is null)
            {
                reation = await _UnitOfWork.GetRepository<Reaction>().AddAsync(new Reaction
                {
                    UserId = userId,
                    ReactionType = model.ReactionType,
                    ActicleId = model.ActicleId,
                    CreatedAt = time,
                    UpdatedAt = time,
                });
            }
            else
            {
                reation.ReactionType = model.ReactionType;
                reation.UpdatedAt = time;
                await _UnitOfWork.SaveChangesAsync();
            }

            return reation;
        }
        public async Task<Product> ModifyProductAsync(ProductEditModel model, string userId)
        {
            var product = await _UnitOfWork.GetRepository<Product>().GetSingleByAsync(x => x.Id == model.ProductId, disableTracking: false);
            var time = DateTime.UtcNow;
            if (product is not null)
            {

                if (model.IsEnable.HasValue)
                {
                    product.IsActive = model.IsEnable.Value;
                }
                if (model.Price.HasValue)
                {
                    product.Price = model.Price.Value;
                }
                if (!string.IsNullOrWhiteSpace(model.Name))
                {
                    product.Name = model.Name;
                }
                if (!string.IsNullOrWhiteSpace(model.Description))
                {
                    product.Discription = model.Description;
                }
                product.UpdatedAt = time;
                await _UnitOfWork.SaveChangesAsync();
            }

            return product;
        }
        public async Task<Order> OrderProductAsync(IEnumerable<Guid> products, string userId)
        {
            DateTime time = DateTime.UtcNow;
            Order order = new Order()
            {
                UserId = userId,
                OrderStatus = OrderStatus.AwaitingPayment,
                CreatedAt = time,
                UpdatedAt = time
            };
            order = await _UnitOfWork.GetRepository<Order>().AddAsync(order);
            foreach (var productId in products)
            {
              await  _UnitOfWork.GetRepository<OrderDetail>().AddAsync(
                  new OrderDetail
                  {
                      OrderId=order.Id,
                      ProductId= productId,
                      CreatedAt=time,
                      UpdatedAt=time
                  });
            }
            await _UnitOfWork.SaveChangesAsync();

            return order;
        }
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            DateTime time = DateTime.UtcNow;
           var isDeleted =  _UnitOfWork.GetRepository<Order>().Delete(x=>x.Id ==orderId);
            if (isDeleted)
            {
                _UnitOfWork.GetRepository<OrderDetail>().DeleteRange(x=>x.OrderId == orderId);
            }
            await _UnitOfWork.SaveChangesAsync();
            return isDeleted;
        }
        public async Task<bool> RemoveProductFromOrderAsync(Guid orderId, Guid productId)
        {
             var isDeleted = _UnitOfWork.GetRepository<OrderDetail>().Delete(x => x.OrderId == orderId && x.Id== productId);
            await _UnitOfWork.SaveChangesAsync();
            return isDeleted;
        }
        public async Task<bool> AddProductToOrderAsync(Guid orderId, Guid productId)
        {
            var result = await _UnitOfWork.GetRepository<OrderDetail>().GetSingleByAsync(x => x.OrderId == orderId && x.Id == productId);
            if (result is null)
            {
                DateTime time = DateTime.UtcNow;
                await _UnitOfWork.GetRepository<OrderDetail>().AddAsync(
                 new OrderDetail
                 {
                     OrderId = orderId,
                     ProductId = productId,
                     CreatedAt = time,
                     UpdatedAt = time
                 });
            }
            else
            {
                throw new Exception("Product already added to Cart");
            }
            await _UnitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> TrackArticle(Guid articleId)
        {
            DateTime time = DateTime.UtcNow;
            var article = _UnitOfWork.GetRepository<Article>().GetSingleBy(x => x.Id == articleId);
            var tracker =  await _UnitOfWork.GetRepository<ViewTracker>().GetSingleByAsync(x => x.ArticleId == articleId,disableTracking: false);
            if (tracker ==null)
            {
                tracker = _UnitOfWork.GetRepository<ViewTracker>().Add(new ViewTracker
                {
                    ArticleId = articleId,
                    ViewSetlementCount = 0,
                    ViewCount  =+1,
                    AuthurId = article.AuthurId,
                    CreatedAt = time,
                    UpdatedAt = time,
                });
            }
            else
            {
                tracker.ViewCount=+1;
                tracker.UpdatedAt = time;
            }
            await _UnitOfWork.SaveChangesAsync();
            return true;

        }
    }
}
