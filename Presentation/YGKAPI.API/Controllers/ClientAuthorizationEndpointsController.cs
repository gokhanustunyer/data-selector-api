using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YGKAPI.Application.Consts;
using YGKAPI.Application.CustomAttributes;
using YGKAPI.Application.Enums;
using YGKAPI.Application.Features.Commands.ClientEndponint.AssignRoleToEndpointsByRoleId;
using YGKAPI.Application.Features.Commands.ClientEndponint.CreateAPIClient;
using YGKAPI.Application.Features.Commands.ClientEndponint.CreateClientEndpointMenu;
using YGKAPI.Application.Features.Commands.ClientEndponint.CreateClientuthorizedEndpoint;
using YGKAPI.Application.Features.Commands.Endpoint.AssignRoleEndpoint;
using YGKAPI.Application.Features.Queries.ClientEndpoint.GetClientEndpointsByRoleId;

namespace YGKAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ClientAuthorizationEndpointsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientAuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ClientEndpointControll, ActionType = ActionType.Writing, Definition = "Save New API Client Application")]
        public async Task<IActionResult> CreateAPIClient(CreateAPIClientCommandRequest createAPIClientCommandRequest)
        {
            var response = await _mediator.Send(createAPIClientCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ClientEndpointControll, ActionType = ActionType.Writing, Definition = "Create Authorized Client Endpoint")]
        public async Task<IActionResult> CreateClientAuthorizedEndpoint(CreateClientuthorizedEndpointCommandRequest createClientuthorizedEndpointCommandRequest)
        {
            var response = await _mediator.Send(createClientuthorizedEndpointCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ClientEndpointControll, ActionType = ActionType.Writing, Definition = "Create Client Endpoint Menu")]
        public async Task<IActionResult> CreateClientEndpointMenu(CreateClientEndpointMenuCommandRequest createClientEndpointMenuCommandRequest)
        {
            var response = await _mediator.Send(createClientEndpointMenuCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ClientEndpointControll, ActionType = ActionType.Writing, Definition = "Assign Role to given endpoints by roleId")]
        public async Task<IActionResult> AssignRoleToClientEndpointByRoleId(AssignRoleToEndpointsByRoleIdCommandRequest assignRoleToEndpointsByRoleIdCommandRequest)
        {
            var response = await _mediator.Send(assignRoleToEndpointsByRoleIdCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.ClientEndpointControll, ActionType = ActionType.Reading, Definition = "Get client endpoints by roleId as menu")]
        public async Task<IActionResult> GetClientEndpointsByRoleId(GetClientEndpointsByRoleIdQueryRequest getClientEndpointsByRoleIdQueryRequest)
        { 
            var response = await _mediator.Send(getClientEndpointsByRoleIdQueryRequest);
            return Ok(response);
        }
    }
}
