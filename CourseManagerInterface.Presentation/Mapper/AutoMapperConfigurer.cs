using AutoMapper;
using CourseManagerInterface.Presentation.Mapper.Profiles;

namespace CourseManagerInterface.Presentation.Mapper
{
    public static class AutoMapperConfigurer
    {
        public static IMapper GetConfuguredMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AddIncomeProfile>();
            }).CreateMapper();
        }
    }
}
