using Microsoft.EntityFrameworkCore;
using test_aeb.Models;


namespace test_aeb.Context
{
    public class ToDo_Context: DbContext
    {
        public ToDo_Context(DbContextOptions<ToDo_Context> options) : base(options)
        {

        }
        public DbSet<ToDo_model> ToDo_models { get; set; }
    }
}
