using TestAEB.Models;

namespace TestAEB.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ToDoModel, ToDoDTO>();
        }
    }
}
