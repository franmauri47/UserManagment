using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos;

public class AddDomicileDto : IMapTo<Domicile>
{
    public required string Street { get; set; }
    public string? DirectionNumber { get; set; }
    public required string Province { get; set; }
    public required string City { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<AddUserDto, User>()
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());
    }
}
