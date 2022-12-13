using AutoMapper;
using ExRatesClassLibrary;
using ExRatesServerSide.Models.ResponseModels;

namespace ExRatesServerSide.MappingProfiles
{
    public class ExRateProfile : Profile
    {
        public ExRateProfile()
        {
            CreateMap<ExRate, RateShort>()
                .ForMember(opt => opt.Date, src => src.MapFrom(src => src.Date))
                .ForMember(opt => opt.Cur_OfficialRate, src => src.MapFrom(src => src.Value))
                .ReverseMap();
        }
    }
}
