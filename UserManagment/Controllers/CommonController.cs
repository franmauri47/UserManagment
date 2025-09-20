using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.UserManagment.Controllers;

[ApiController]
[Route("/v1/[Controller]/[Action]")]
public class CommonController : ControllerBase
{
    private ISender? _sender;

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
