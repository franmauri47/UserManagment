using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Dtos;

public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DomicileDto? DomicileData { get; set; }
}
