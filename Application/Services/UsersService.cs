using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

internal class UsersService(MySqlDbContext context, IMapper mapper) : IUsersService
{
    public async Task<List<GetUserDataDto>> GetUsersByDataAsync(string? name, string? province, string? city, CancellationToken cancellationToken = default)
    {
        var usersQuery = context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            usersQuery = usersQuery.Where(x => x.Name == name);

        if (!string.IsNullOrEmpty(province) || !string.IsNullOrEmpty(city))
        {
            usersQuery = usersQuery.Where(u =>
                context.Domiciles.Any(d =>
                    d.UserId == u.Id &&
                    (string.IsNullOrEmpty(province) || d.Province == province) &&
                    (string.IsNullOrEmpty(city) || d.City == city)
                )
            );
        }

        var result = await usersQuery
            .Select(u => new GetUserDataDto
            {
                User = mapper.Map<UserDto>(u),
                Domicile = mapper.Map<DomicileDto>(context.Domiciles
                    .FirstOrDefault(d => d.UserId == u.Id))
            })
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<int?> DeleteUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = new User { Id = id, Name = string.Empty, Email = string.Empty };
        context.Users.Attach(user);
        context.Users.Remove(user);
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return id;
        }
        catch (DbUpdateConcurrencyException)
        {
            return null;
        }
    }

    public async Task<bool> UpdateUserDataAsync(int id, AddDomicileDto? domicileData, CancellationToken cancellationToken = default)
    {
        var domcile = await context.Domiciles.FirstOrDefaultAsync(d => d.UserId == id);
            domcile.Street = domicileData!.Street;
            domcile.Province = domicileData.Province;
            domcile.City = domicileData.City;
            domcile.DirectionNumber = domicileData.DirectionNumber;
            domcile.ModifiedDate = DateTime.UtcNow;

        context.Domiciles.Update(domcile);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
