using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
