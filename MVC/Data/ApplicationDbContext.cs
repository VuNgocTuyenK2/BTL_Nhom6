using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
        public class ApplicationDbContext : DbContext
        {
                public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
                { }
                public DbSet<User> User { get; set; }// Quach Kieu Trang sinh code tu dong co
                public DbSet<Clothes> Clothes { get; set; } // Vu Ngoc Tuyen 2021050715
        }
}