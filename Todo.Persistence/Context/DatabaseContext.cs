using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Persistence.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            seedData(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoList> TodoList { get; set; }

        private void seedData(ModelBuilder builder)
        {
            var tony = new User { Id = 1, Firstname = "Tony", Lastname = "Stark", Gender = Domain.Enums.GenderType.Male };
            var peter = new User { Id = 2, Firstname = "Peter", Lastname = "Parker", Gender = Domain.Enums.GenderType.Male };
            var steve = new User { Id = 3, Firstname = "Steve", Lastname = "Rogers", Gender = Domain.Enums.GenderType.Male };
            builder.Entity<User>().HasData(tony, peter, steve);

            //builder.Entity<TodoList>().HasData(
            //    new TodoList() { Id = 1, Title = "Learn new Tricks", Status = Domain.Enums.TodoStatusType.Pending, Deadline = DateTime.UtcNow.AddDays(5), Remarks = "", User = tony },
            //    new TodoList() { Id = 2, Title = "Learn new Tech", Status = Domain.Enums.TodoStatusType.Pending, Deadline = DateTime.UtcNow.AddDays(7), Remarks = "", User = peter },
            //    new TodoList() { Id = 3, Title = "Watch Movies", Status = Domain.Enums.TodoStatusType.Inprogress, Deadline = DateTime.UtcNow.AddDays(3), Remarks = "", User = tony },
            //    new TodoList() { Id = 4, Title = "Learn Judo", Status = Domain.Enums.TodoStatusType.Completed, Deadline = DateTime.UtcNow.AddDays(2), Remarks = "", User = peter },
            //    new TodoList() { Id = 5, Title = "Go to Park", Status = Domain.Enums.TodoStatusType.Cancelled, Deadline = DateTime.UtcNow.AddDays(1), Remarks = "Backache again", User = steve }
            //    );
        }
    }
}