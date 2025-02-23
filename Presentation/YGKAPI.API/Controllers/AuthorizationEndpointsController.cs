using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YGKAPI.Application.Consts;
using YGKAPI.Application.CustomAttributes;
using YGKAPI.Application.Enums;
using YGKAPI.Application.Features.Commands.Endpoint.AssignRoleEndpoint;
using YGKAPI.Application.Features.Queries.Endpoint.GetEndpointsByRoleId;
using YGKAPI.Application.Features.Queries.Endpoint.GetRolesToEndpoint;

namespace YGKAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AuthorizationEndpointsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.EndpointControll, ActionType = ActionType.Writing, Definition = "Assign Role to Endpoint")]
        public async Task<IActionResult> AssignRoleToEndpoint(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
        {
            assignRoleEndpointCommandRequest.Type = typeof(Program);
            var response = await _mediator.Send(assignRoleEndpointCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.EndpointControll, ActionType = ActionType.Reading, Definition = "Get Role and endpoint matches by endpoint")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
        {
            var response = await _mediator.Send(getRolesToEndpointQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.EndpointControll, ActionType = ActionType.Reading, Definition = "Get Role and endpoint matches by roleId")]
        public async Task<IActionResult> GetEndpointsByRoleId(GetEndpointsByRoleIdQueryRequest getEndpointsByRoleIdQueryRequest)
        {
            var response = await _mediator.Send(getEndpointsByRoleIdQueryRequest);
            return Ok(response);
        }
    }
}
