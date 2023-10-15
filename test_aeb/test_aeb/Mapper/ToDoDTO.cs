using TestAEB.Models;

namespace TestAEB.Mapper
{
    public class ToDoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueTime { get; set; }
        public DateTime CompletionTime { get; set; }
        public status Status { get; set; }
    }
}
