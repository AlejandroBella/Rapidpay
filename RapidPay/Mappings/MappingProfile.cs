using AutoMapper;
using RapidPay.Business.Entities;
using RapidPay.Data.Model;
using RapidPay.View.Entities;

namespace RapidPay.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CardHolder, CardHolderView>().ReverseMap();
            CreateMap<CardHolderModel, CardHolder>().ReverseMap();
            CreateMap<CardModel, Card>().ReverseMap();
            CreateMap<CardView, Card>().ReverseMap();
            CreateMap<BalanceModel, Balance>().ReverseMap();
            CreateMap<BalanceDetailModel, BalanceDetail>().ReverseMap();
            CreateMap<Balance,BalanceView>().ReverseMap();
        }
    }
}

