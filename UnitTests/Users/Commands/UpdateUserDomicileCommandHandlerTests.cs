using Application.Dtos;
using Application.Users.Commands;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.Users.Commands;

public class UpdateUserDomicileCommandHandlerTests
{
    private readonly Mock<IGenericRepository<Domicile>> _domicileRepoMock;
    private readonly Mock<ILogger<UpdateUserDomicileCommandHandler>> _loggerMock;
    private readonly UpdateUserDomicileCommandHandler _handler;

    public UpdateUserDomicileCommandHandlerTests()
    {
        _domicileRepoMock = new Mock<IGenericRepository<Domicile>>();
        _loggerMock = new Mock<ILogger<UpdateUserDomicileCommandHandler>>();

        _handler = new UpdateUserDomicileCommandHandler(
            _domicileRepoMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ReturnsError_WhenUserNotFound()
    {
        // Arrange
        var dto = new UpdateDomicileDto
        {
            Street = "Main",
            Province = "A",
            City = "B"
        };

        _domicileRepoMock
            .Setup(r => r.GetByFilterAsync(It.IsAny<Expression<Func<Domicile, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Domicile>());

        var command = new UpdateUserDomicileCommand(999, dto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(-1, result.ErrorCode);
        Assert.Contains("not found", result.ErrorDescription, StringComparison.OrdinalIgnoreCase);
        Assert.Null(result.Data);

        _domicileRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Domicile>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ReturnsSuccess_WhenUserExistsAndDomicileUpdated()
    {
        // Arrange
        var dto = new UpdateDomicileDto
        {
            Street = "New Street",
            Province = "New Province",
            City = "New City",
            DirectionNumber = "123"
        };

        var existingDomicile = new Domicile
        {
            Id = 1,
            UserId = 1,
            Street = "Old Street",
            Province = "Old Province",
            City = "Old City",
            DirectionNumber = "10",
            CreationDate = DateTime.UtcNow.AddDays(-10),
            ModifiedDate = null
        };

        var mappedDomicile = new Domicile
        {
            Id = existingDomicile.Id,
            UserId = existingDomicile.UserId,
            Street = dto.Street,
            Province = dto.Province,
            City = dto.City,
            DirectionNumber = dto.DirectionNumber,
            CreationDate = existingDomicile.CreationDate,
            ModifiedDate = DateTime.UtcNow
        };

        _domicileRepoMock
            .Setup(r => r.GetByFilterAsync(It.IsAny<Expression<Func<Domicile, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Domicile> { existingDomicile });

        _domicileRepoMock
            .Setup(r => r.UpdateAsync(It.IsAny<Domicile>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var command = new UpdateUserDomicileCommand(1, dto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(0, result.ErrorCode);
        Assert.NotNull(result.Data);
        var updated = Assert.IsType<Domicile>(result.Data);

        Assert.Equal(existingDomicile.Id, updated.Id);
        Assert.Equal(existingDomicile.UserId, updated.UserId);
        Assert.Equal(dto.Street, updated.Street);
        Assert.Equal(dto.Province, updated.Province);
        Assert.Equal(dto.City, updated.City);
        Assert.Equal(dto.DirectionNumber, updated.DirectionNumber);

        Assert.Equal(existingDomicile.CreationDate, updated.CreationDate);
        Assert.NotNull(updated.ModifiedDate);
        Assert.True(updated.ModifiedDate > existingDomicile.CreationDate);

        _domicileRepoMock.Verify(r => r.UpdateAsync(
            It.Is<Domicile>(d =>
                d.Id == mappedDomicile.Id &&
                d.UserId == mappedDomicile.UserId &&
                d.Street == mappedDomicile.Street &&
                d.Province == mappedDomicile.Province &&
                d.City == mappedDomicile.City
            ),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsError_WhenUpdateAsyncThrows()
    {
        // Arrange
        var dto = new UpdateDomicileDto
        {
            Street = "Main",
            Province = "A",
            City = "B"
        };

        var existingDomicile = new Domicile
        {
            Id = 3,
            UserId = 1,
            Street = "Old Street",
            Province = "Old Province",
            City = "Old City",
            CreationDate = DateTime.UtcNow.AddDays(-7)
        };

        var mappedDomicile = new Domicile
        {
            Id = existingDomicile.Id,
            UserId = existingDomicile.UserId,
            Street = dto.Street,
            Province = dto.Province,
            City = dto.City,
            CreationDate = existingDomicile.CreationDate,
            ModifiedDate = DateTime.UtcNow
        };

        _domicileRepoMock
            .Setup(r => r.GetByFilterAsync(It.IsAny<Expression<Func<Domicile, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Domicile> { existingDomicile });

        _domicileRepoMock
            .Setup(r => r.UpdateAsync(It.IsAny<Domicile>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("DB error"));

        var command = new UpdateUserDomicileCommand(1, dto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(-1, result.ErrorCode);
        Assert.Contains("DB error", result.ErrorDescription);
        Assert.Null(result.Data);

        _domicileRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Domicile>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}