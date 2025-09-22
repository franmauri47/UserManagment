using Application.Common.Dtos;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commands;

public record AddUserCommand(AddUserDto data) : IRequest<ResponseDto>;

public class AddUserCommandHandler(
    IGenericRepository<User> userRepository,
    IGenericRepository<Domicile> domicileRepository,
    IMapper mapper,
    ILogger<AddUserCommandHandler> logger) : IRequestHandler<AddUserCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = mapper.Map<User>(request.data);
            var domicile = mapper.Map<Domicile>(request.data.DomicileData);

            var userResult = await userRepository.AddAsync(user, cancellationToken);

            domicile.UserId = userResult.Id;
            var domicileResult = await domicileRepository.AddAsync(domicile, cancellationToken);

            return new ResponseDto
            {
                ErrorCode = 0,
                ErrorDescription = string.Empty,
                Data = new GetUserDataDto
                {
                    User = mapper.Map<UserDto>(userResult),
                    Domicile = mapper.Map<DomicileDto>(domicileResult)
                }
            };
        }
        catch (Exception ex)
        {
            string errorMessage = $"An error occurred while adding a new user: {ex.Message}";
            logger.LogError(errorMessage);
            return new ResponseDto<GetUserDataDto>
            {
                ErrorCode = -1,
                ErrorDescription = errorMessage,
                Data = null
            };
        }
    }
}
