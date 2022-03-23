using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_proj.Modles;
using Microsoft.Extensions.Configuration;

namespace web_proj.Presistance.Contexts
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration){
            Configuration = configuration;
        }

        public DbSet<User> Users{ get; set;}
        public DbSet<Movie> Movies { get; set;}
        public DbSet<WatchList> WatchLists { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreSQL"));
    }
}