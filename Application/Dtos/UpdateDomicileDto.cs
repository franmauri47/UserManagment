using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dtos;

public class UpdateDomicileDto : IMapFrom<Domicile>
{
    public required string Street { get; set; }
    public string? DirectionNumber { get; set; }
    public required string Province { get; set; }
    public required string City { get; set; }
}
