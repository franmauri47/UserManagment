using Application.Common.Dtos;
using Application.Dtos;
using Application.Users.Commands;
using Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.UserManagment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : CommonController
{
    [HttpPost]
    public async Task<ActionResult<ResponseDto>> CreateUser([FromBody] AddUserDto userData, CancellationToken cancellationToken)
        => await Sender.Send(new AddUserCommand(userData), cancellationToken);

    [HttpGet]
    public async Task<ActionResult<ResponseDto>> GetUsers([FromQuery] string? name, [FromQuery] string? province, [FromQuery] string? city, CancellationToken cancellationToken)
        => await Sender.Send(new GetUserQuery(name, province, city), cancellationToken);

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ResponseDto>> DeleteUserById([FromRoute]int id, CancellationToken cancellationToken)
        => await Sender.Send(new DeleteUserCommand(id), cancellationToken);

    [HttpPut("{id:int}/Domicile")]
    public async Task<ActionResult<ResponseDto>> UpdateUserDomicile([FromRoute]int id, [FromBody] UpdateDomicileDto domicileData, CancellationToken cancellationToken)
        => await Sender.Send(new UpdateUserDomicileCommand(id, domicileData), cancellationToken);
}
