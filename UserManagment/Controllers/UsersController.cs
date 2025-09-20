using Application.Common.Dtos;
using Application.Users.Commands;
using Application.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.UserManagment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : CommonController
{
    [HttpPost]
    public async Task<ActionResult<ResponseDto>> CreateUser([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
        => await Sender.Send(new AddUserCommand(createUserDto), cancellationToken);
}
