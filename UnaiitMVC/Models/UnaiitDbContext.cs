using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UnaiitMVC.Models.Faculty;
using UnaiitMVC.Models.Grade;
using UnaiitMVC.Models.School;

namespace UnaiitMVC.Models
{
    public class UnaiitDbContext : IdentityDbContext<AppUser>
    {
        public UnaiitDbContext(DbContextOptions<UnaiitDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var model in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = model.GetTableName() ?? model.ClrType.Name;
                if (tableName.StartsWith("AspNet"))
                {
                    model.SetTableName(tableName.Substring(6));
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyBlog;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<SchoolTable> School { get; set; } = default!;
        public DbSet<FacultyTable> Faculty { get; set; } = default!;
        public DbSet<GradeTable> Grade { get; set; } = default!;
        public DbSet<AppUser> AppUser { get; set; } = default!;
    }
}
