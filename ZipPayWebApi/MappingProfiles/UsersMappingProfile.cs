using AutoMapper;
using ZipPayWebApp.BAL.Entity;
using ZipPayWebApp.DAL.Entity;

namespace ZipPayWebApp.MappingProfiles
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<USER, UserModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Email_Id, opt => opt.MapFrom(src => src.EMAIL_ID))
                .ForMember(dest => dest.Last_Name, opt => opt.MapFrom(src => src.LAST_NAME))
                .ForMember(dest => dest.First_Name, opt => opt.MapFrom(src => src.FIRST_NAME))
                .ForMember(dest => dest.Monthly_Salary, opt => opt.MapFrom(src => src.MONTHLY_SALARY))
                .ForMember(dest => dest.Monthly_Expense, opt => opt.MapFrom(src => src.MONTHLY_EXPENSE))
                .ReverseMap();
        }
    }
}
