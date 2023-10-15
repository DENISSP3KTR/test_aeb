using Microsoft.EntityFrameworkCore;
using TestAEB.Models;


namespace TestAEB.Context
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ToDo_DB");
        }
        public DbSet<ToDoModel> ToDoModels { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
