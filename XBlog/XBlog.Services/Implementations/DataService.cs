
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
    }
}
