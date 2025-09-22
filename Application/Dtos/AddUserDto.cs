using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos;

public class AddUserDto : IMapTo<User>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public AddDomicileDto? DomicileData { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddUserDto, User>()
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));
    }
}
