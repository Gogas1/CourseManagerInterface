using AutoMapper;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using CourseManagerInterface.Presentation.Requests.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Mapper.Profiles
{
    public class AddIncomeProfile : Profile
    {
        public AddIncomeProfile()
        {
            CreateMap<IncomeProductRecord, RequestIncomeProduct>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
        }
    }
}
