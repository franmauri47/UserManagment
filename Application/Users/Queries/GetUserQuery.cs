using Application.Common.Dtos;
using Domain.Entities;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.Queries;

public record GetUserQuery(string? Name, string? Province, string? City) : IRequest<ResponseDto>;

public class GetUserQueryHandler(
    IUsersService usersService,
    ILogger<GetUserQueryHandler> logger) : IRequestHandler<GetUserQuery, ResponseDto>
{
    public async Task<ResponseDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = await usersService.GetUsersByDataAsync(request.Name, request.Province, request.City, cancellationToken);
            return new ResponseDto
            {
                ErrorCode = 0,
                ErrorDescription = string.Empty,
                Data = query
            };
        }
        catch (Exception ex)
        {
            string errorMessage = $"An error occurred while searching users: {ex.Message}";
            logger.LogError(errorMessage);
            return new ResponseDto<List<User>>
            {
                ErrorCode = 1,
                ErrorDescription = ex.Message
            };
        }
    }
}
