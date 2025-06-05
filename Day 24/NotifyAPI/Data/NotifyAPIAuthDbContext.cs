using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotifyAPI.Models.Domains;

namespace NotifyAPI.Data
{
    public class NotifyAPIAuthDbContext : IdentityDbContext
    {
        public NotifyAPIAuthDbContext(DbContextOptions<NotifyAPIAuthDbContext> options) : base(options)
        {

        }

        public DbSet<Document> Documents { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var HRRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var StaffRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = HRRoleId,
                    ConcurrencyStamp = HRRoleId,
                    Name = "HR",
                    NormalizedName = "HR".ToUpper()
                },
                new IdentityRole
                {
                    Id = StaffRoleId,
                    ConcurrencyStamp = StaffRoleId,
                    Name = "Staff",
                    NormalizedName = "Staff".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}