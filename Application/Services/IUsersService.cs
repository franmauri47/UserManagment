using Application.Dtos;

namespace Infrastructure.Services;

public interface IUsersService
{
    Task<List<GetUserDataDto>> GetUsersByDataAsync(string? name, string? province, string? city, CancellationToken cancellationToken = default);
    Task<int?> DeleteUserByIdAsync(int id, CancellationToken cancellationToken = default);
}
