using Application.Users.Commands;
using Application.Users.Queries;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Users.Commands;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUsersService> _usersServiceMock;
    private readonly Mock<ILogger<DeleteUserCommandHandler>> _loggerMock;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _usersServiceMock = new Mock<IUsersService>();
        _loggerMock = new Mock<ILogger<DeleteUserCommandHandler>>();
        _handler = new DeleteUserCommandHandler(_usersServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsSuccess_WhenUserIsDeleted()
    {
        // Arrange
        var userId = 1;
        var expectedData = userId;
        _usersServiceMock
            .Setup(s => s.DeleteUserByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedData);

        var request = new DeleteUserCommand(userId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(0, result.ErrorCode);
        Assert.Equal(string.Empty, result.ErrorDescription);
        Assert.Equal(expectedData, result.Data);
    }

    [Fact]
    public async Task Handle_ReturnsError_WhenUserNotFound()
    {
        // Arrange
        var userId = 2;
        _usersServiceMock
            .Setup(s => s.DeleteUserByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((int?)null);

        var request = new DeleteUserCommand(userId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(1, result.ErrorCode);
        Assert.Equal("User not found", result.ErrorDescription);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Handle_ReturnsError_WhenServiceThrowsException()
    {
        // Arrange
        var userId = 3;
        _usersServiceMock
            .Setup(s => s.DeleteUserByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Service failure"));

        var request = new DeleteUserCommand(userId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(-1, result.ErrorCode);
        Assert.Contains("Service failure", result.ErrorDescription);
        Assert.Null(result.Data);

        // Verify logger was called
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Service failure")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
            Times.Once);
    }

}
