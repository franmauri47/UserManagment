using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Dtos;

public class DomicileDto : IMapFrom<Domicile>
{
    public int UserId { get; set; }
    public string Street { get; set; } = null!;
    public string? DirectionNumber { get; set; }
    public string Province { get; set; } = null!;
    public string City { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domicile, DomicileDto>()
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));
    }
}
