using Application.Common.Dtos;
using Application.Dtos;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commands;

public record UpdateUserDomicileCommand(int UserId, UpdateDomicileDto domicileData) : IRequest<ResponseDto>;

public class UpdateUserDomicileCommandHandler(
    IGenericRepository<Domicile> domicileRepository,
    ILogger<UpdateUserDomicileCommandHandler> logger) : IRequestHandler<UpdateUserDomicileCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateUserDomicileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var persistedDomicile = (await domicileRepository.GetByFilterAsync(x => x.UserId == request.UserId, cancellationToken)).FirstOrDefault()
                ?? throw new Exception($"Domicile not found for the user Id {request.UserId}.");

            persistedDomicile.Street = request.domicileData.Street;
            persistedDomicile.DirectionNumber = request.domicileData.DirectionNumber;
            persistedDomicile.Province = request.domicileData.Province;
            persistedDomicile.City = request.domicileData.City;
            persistedDomicile.ModifiedDate = DateTime.Now;

            await domicileRepository.UpdateAsync(persistedDomicile, cancellationToken);

            return new ResponseDto
            {
                ErrorCode = 0,
                ErrorDescription = string.Empty,
                Data = persistedDomicile
            };
        }
        catch (Exception ex)
        {
            string errorMessage = $"An error occurred while updating the user's data: {ex.Message}";
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