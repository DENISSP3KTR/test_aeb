using Microsoft.EntityFrameworkCore;
using TestAEB.Models;


namespace TestAEB.Context
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }
        public DbSet<ToDoModel> ToDoModels { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
