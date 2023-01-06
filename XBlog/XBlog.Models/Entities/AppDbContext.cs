using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBlog.Models.Entities
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Authur> Authurs { get; set; }
        public virtual DbSet<Category> ArticleCategories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Coment> Coments { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<LocalGovernment> LocalGovernments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail>  OrderDetails { get; set; }
        public virtual DbSet<Payment>  Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<ViewTracker>  ViewTrackers { get; set; }
    }
}
