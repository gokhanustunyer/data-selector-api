using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YGKAPI.Application.CustomAttributes;
using YGKAPI.Application.Enums;
using YGKAPI.Application.Features.Commands.User.AssignRolesToUser;
using YGKAPI.Application.Features.Commands.User.CreateUser;
using YGKAPI.Application.Features.Commands.User.LoginWithGoogle;
using YGKAPI.Application.Features.Queries.User.GetAllUsers;

namespace YGKAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assign Roles To User", Menu = "Users")]
        public async Task<IActionResult> AssignRolesToUser(AssignRolesToUserCommandRequest assignRolesToUserCommandRequest)
        {
            var response = await _mediator.Send(assignRolesToUserCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Users", Menu = "Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            var response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            var response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
    }
}
