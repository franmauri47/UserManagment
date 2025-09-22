using Application.Users.Commands;
using Application.Dtos;
using Application.Common.Dtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Users.Commands;

public class AddUserCommandHandlerTests
{
    private readonly Mock<IGenericRepository<User>> _userRepoMock;
    private readonly Mock<IGenericRepository<Domicile>> _domicileRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<AddUserCommandHandler>> _loggerMock;
    private readonly AddUserCommandHandler _handler;

    public AddUserCommandHandlerTests()
    {
        _userRepoMock = new Mock<IGenericRepository<User>>();
        _domicileRepoMock = new Mock<IGenericRepository<Domicile>>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<AddUserCommandHandler>>();
        _handler = new AddUserCommandHandler(
            _userRepoMock.Object,
            _domicileRepoMock.Object,
            _mapperMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ReturnsSuccess_WhenUserAndDomicileAreAdded()
    {
        // Arrange
        var addUserDto = new AddUserDto { Name = "Test", Email = "test@mail.com", DomicileData = new AddDomicileDto { Street = "Main", Province = "A", City = "B" } };
        var user = new User { Name = "Test", Email = "test@mail.com" };
        var domicile = new Domicile { Street = "Main", Province = "A", City = "B" };
        var userResult = new User { Name = "Test", Email = "test@mail.com" };
        var domicileResult = new Domicile { Street = "Main", Province = "A", City = "B" };

        _mapperMock.Setup(m => m.Map<User>(addUserDto)).Returns(user);
        _mapperMock.Setup(m => m.Map<Domicile>(addUserDto.DomicileData)).Returns(domicile);
        _userRepoMock.Setup(r => r.AddAsync(user, It.IsAny<CancellationToken>())).ReturnsAsync(userResult);
        _domicileRepoMock.Setup(r => r.AddAsync(It.IsAny<Domicile>(), It.IsAny<CancellationToken>())).ReturnsAsync(domicileResult);
        _mapperMock.Setup(m => m.Map<UserDto>(userResult)).Returns(new UserDto());
        _mapperMock.Setup(m => m.Map<DomicileDto>(domicileResult)).Returns(new DomicileDto());

        var command = new AddUserCommand(addUserDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(0, result.ErrorCode);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public async Task Handle_ReturnsError_WhenExceptionThrown()
    {
        // Arrange
        var addUserDto = new AddUserDto { Name = "Test", Email = "test@mail.com", DomicileData = new AddDomicileDto { Street = "Main", Province = "A", City = "B" } };
        _mapperMock.Setup(m => m.Map<User>(addUserDto)).Throws(new System.Exception("Mapping error"));

        var command = new AddUserCommand(addUserDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(-1, result.ErrorCode);
        Assert.Contains("Mapping error", result.ErrorDescription);
        Assert.Null(result.Data);
    }
}
