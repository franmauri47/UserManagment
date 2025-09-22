using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos;

public class UpdateDomicileDto : AddDomicileDto, IMapFrom<Domicile>
{
    public override void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateDomicileDto, Domicile>()
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.CreationDate, opt => opt.Ignore());
    }
}
