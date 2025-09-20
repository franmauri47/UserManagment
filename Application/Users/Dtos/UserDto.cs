using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Users.Dtos;

public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
