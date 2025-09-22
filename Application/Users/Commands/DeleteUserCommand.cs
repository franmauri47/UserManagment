using Application.Common.Dtos;
using Application.Dtos;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commands;

public record DeleteUserCommand(int Id) : IRequest<ResponseDto>;

public class DeleteUserCommandHandler(IUsersService usersService, ILogger<DeleteUserCommandHandler> logger) : IRequestHandler<DeleteUserCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var queryResult = await usersService.DeleteUserByIdAsync(request.Id, cancellationToken);
            return new ResponseDto
            {
                ErrorCode = queryResult == null ? 1 : 0,
                ErrorDescription = queryResult == null ? "User not found" : string.Empty,
                Data = queryResult
            };
        }
        catch (Exception ex)
        {
            string errorMessage = $"An error occurred while deleting user: {ex.Message}";
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
