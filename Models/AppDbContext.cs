using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;
using System.Collections.Generic;


namespace WebApplication4.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PUMASTER> PUMASTER { get; set; }

        public DbSet<USERS> USERS { get; set; }
    }
}

