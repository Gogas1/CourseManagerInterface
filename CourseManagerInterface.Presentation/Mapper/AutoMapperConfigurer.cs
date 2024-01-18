using AutoMapper;
using CourseManagerInterface.Presentation.Mapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
