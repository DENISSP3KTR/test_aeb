using test_aeb.Models;

namespace test_aeb.Mapper
{
    public class ToDoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Due_Time { get; set; }
        public DateTime Completion_Time { get; set; }
        public Status status { get; set; }
    }
}
