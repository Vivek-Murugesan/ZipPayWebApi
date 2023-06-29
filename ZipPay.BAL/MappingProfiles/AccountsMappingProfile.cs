using AutoMapper;
using ZipPay.BAL.Entity;
using ZipPayWebApp.DAL.Entity;

namespace ZipPayWebApp.MappingProfile
{
    public class AccountsMappingProfile : Profile
    {
        public AccountsMappingProfile()
        {
            CreateMap<ACCOUNT, AccountModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.USER_ID))
                .ForMember(dest => dest.Account_No, opt => opt.MapFrom(src => src.ACCOUNT_NO))
                .ForMember(dest => dest.Card_No, opt => opt.MapFrom(src => src.CARD_NO))
                .ForMember(dest => dest.Max_Credit_Limit, opt => opt.MapFrom(src => src.MAX_CREDIT_LIMIT))
                .ForMember(dest => dest.Statement_Date, opt => opt.MapFrom(src => src.STATEMENT_DATE))
                .ForMember(dest => dest.Due_Amount, opt => opt.MapFrom(src => src.DUE_AMOUNT))
                .ReverseMap();
        }
    }
}
