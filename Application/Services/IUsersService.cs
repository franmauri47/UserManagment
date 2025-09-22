using Application.Dtos;
using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public interface IUsersService
{
    Task<List<GetUserDataDto>> GetUsersByDataAsync(string? name, string? province, string? city, CancellationToken cancellationToken = default);
    Task<int?> DeleteUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> UpdateUserDataAsync(int id, AddDomicileDto? domicileData, CancellationToken cancellationToken = default);
}
