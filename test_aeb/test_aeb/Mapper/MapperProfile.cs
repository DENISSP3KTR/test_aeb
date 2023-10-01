using test_aeb.Models;

namespace test_aeb.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ToDo_model, ToDoDTO>();
        }
    }
}
