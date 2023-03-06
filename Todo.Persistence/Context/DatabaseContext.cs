using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Persistence.Context
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            seedData(builder);
        }

        public DbSet<TodoList> TodoList { get; set; }

        private void seedData(ModelBuilder builder)
        {
            #region ROLES
            var adminRole = new IdentityRole() { Name = "Administrator", NormalizedName = "Admin" };
            var userRole = new IdentityRole() { Name = "User", NormalizedName = "User" };
            builder.Entity<IdentityRole>().HasData(userRole, adminRole);
            #endregion
            #region USER
            var adminAcct = new User()
            {
                Firstname = "Admin",
                Lastname = "Admin",
                UserName = "admin",
                Email = "admintodoapi@grr.la"
            };
            adminAcct.PasswordHash = new PasswordHasher<User>().HashPassword(adminAcct, "abcdE123");

            var peterParkerAcct = new User()
            {
                Prefix = "Dr.",
                Firstname = "Peter",
                Middlename = "Stone",
                Lastname = "Parker",
                Suffix = "III",
                UserName = "peterparker",
                Email = "peterparker@grr.la",
                Birthday = new DateTime(1990, 08, 21),
                Gender = Domain.Enums.GenderType.Male,
                CivilStatus = Domain.Enums.CivilStatusType.Married
            };
            peterParkerAcct.PasswordHash = new PasswordHasher<User>().HashPassword(peterParkerAcct, "abcdE123");

            var natAcct = new User()
            {
                Prefix = "PHD.",
                Firstname = "Natasha",
                Lastname = "Romanhoff",
                Suffix = "X",
                UserName = "nastasha",
                Email = "blackwidow@grr.la",
                Birthday = new DateTime(1985, 01, 01),
                Gender = Domain.Enums.GenderType.Female,
                CivilStatus = Domain.Enums.CivilStatusType.CoHabitation
            };
            natAcct.PasswordHash = new PasswordHasher<User>().HashPassword(natAcct, "abcdE123");

            // add account
            builder.Entity<User>().HasData(adminAcct, peterParkerAcct, natAcct);
            #endregion
            #region USER ROLES
            builder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>() { UserId = adminAcct.Id, RoleId = adminRole.Id },
               new IdentityUserRole<string>() { UserId = peterParkerAcct.Id, RoleId = userRole.Id },
               new IdentityUserRole<string>() { UserId = natAcct.Id, RoleId = userRole.Id }
               );
            #endregion
        }
    }
}