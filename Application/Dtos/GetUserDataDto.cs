namespace Application.Dtos;

public class GetUserDataDto
{
    public UserDto User { get; set; } = null!;
    public DomicileDto? Domicile { get; set; }
}
