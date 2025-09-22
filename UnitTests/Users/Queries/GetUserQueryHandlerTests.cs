using Application.Users.Queries;
using Application.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Users.Queries
{
    public class GetUserQueryHandlerTests
    {
        private readonly Mock<IUsersService> _usersServiceMock;
        private readonly Mock<ILogger<GetUserQueryHandler>> _loggerMock;
        private readonly GetUserQueryHandler _handler;

        public GetUserQueryHandlerTests()
        {
            _usersServiceMock = new Mock<IUsersService>();
            _loggerMock = new Mock<ILogger<GetUserQueryHandler>>();
            _handler = new GetUserQueryHandler(_usersServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenUsersFound()
        {
            var users = new List<GetUserDataDto> { new GetUserDataDto { User = new UserDto { Id = 1, Name = "Test", Email = "test@mail.com" } } };
            _usersServiceMock.Setup(s => s.GetUsersByDataAsync("Test", null, null, It.IsAny<CancellationToken>())).ReturnsAsync(users);
            var query = new GetUserQuery("Test", null, null);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(0, result.ErrorCode);
            Assert.NotNull(result.Data);
            Assert.IsType<List<GetUserDataDto>>(result.Data);
        }

        [Fact]
        public async Task Handle_ReturnsError_WhenExceptionThrown()
        {
            _usersServiceMock.Setup(s => s.GetUsersByDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new System.Exception("DB error"));
            var query = new GetUserQuery("Test", null, null);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(1, result.ErrorCode);
            Assert.Contains("DB error", result.ErrorDescription);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => (v.ToString() ?? string.Empty).Contains("DB error")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsAllUsers_WhenNoArgumentsProvided()
        {
            var allUsers = new List<GetUserDataDto>
            {
                new GetUserDataDto { User = new UserDto { Id = 1, Name = "User1", Email = "user1@mail.com" } },
                new GetUserDataDto { User = new UserDto { Id = 2, Name = "User2", Email = "user2@mail.com" } }
            };
            _usersServiceMock.Setup(s => s.GetUsersByDataAsync(null, null, null, It.IsAny<CancellationToken>())).ReturnsAsync(allUsers);
            var query = new GetUserQuery(null, null, null);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(0, result.ErrorCode);
            Assert.NotNull(result.Data);
            var resultList = Assert.IsType<List<GetUserDataDto>>(result.Data);
            Assert.Equal(2, resultList.Count);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoUsersFound()
        {
            _usersServiceMock.Setup(s => s.GetUsersByDataAsync("Unknown", null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<GetUserDataDto>());
            var query = new GetUserQuery("Unknown", null, null);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(0, result.ErrorCode);
            var resultList = Assert.IsType<List<GetUserDataDto>>(result.Data);
            Assert.Empty(resultList);
        }

        [Fact]
        public async Task Handle_WorksWithPartialArguments()
        {
            var users = new List<GetUserDataDto>
            {
                new GetUserDataDto { User = new UserDto { Id = 3, Name = "ProvUser", Email = "prov@mail.com" } }
            };
            _usersServiceMock.Setup(s => s.GetUsersByDataAsync(null, "ProvinceX", null, It.IsAny<CancellationToken>())).ReturnsAsync(users);

            var query = new GetUserQuery(null, "ProvinceX", null);
            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(0, result.ErrorCode);
            var resultList = Assert.IsType<List<GetUserDataDto>>(result.Data);
            Assert.Single(resultList);
            Assert.Equal("ProvUser", resultList[0].User.Name);
        }
    }
}
