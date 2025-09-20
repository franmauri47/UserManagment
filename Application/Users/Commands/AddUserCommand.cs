using Application.Common.Dtos;
using Application.Users.Dtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commands;

public record AddUserCommand(CreateUserDto data) : IRequest<ResponseDto<UserDto>>;

public class AddUserCommandHandler(
    IGenericRepository<User> userRepository,
    IMapper mapper,
    ILogger<AddUserCommandHandler> logger) : IRequestHandler<AddUserCommand, ResponseDto<UserDto>>
{
    public async Task<ResponseDto<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = new User
            {
                Name = request.data.Name,
                Email = request.data.Email,
                CreationDate = DateTime.UtcNow
            };
            
            var result = await userRepository.AddAsync(user, cancellationToken);

            return new ResponseDto<UserDto>
            {
                ErrorCode = 0,
                ErrorDescription = string.Empty,
                Data = mapper.Map<UserDto>(result)
            };
        }
        catch (Exception ex)
        {
            string errorMessage = $"An error occurred while adding a new user: {ex.Message}";
            logger.LogError(errorMessage);
            return new ResponseDto<UserDto>
            {
                ErrorCode = -1,
                ErrorDescription = errorMessage,
                Data = null
            };
        }
    }
}
