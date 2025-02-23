using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YGKAPI.Application.Abstractions.Services.Configurations;
using YGKAPI.Application.Consts;
using YGKAPI.Application.CustomAttributes;

namespace YGKAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Authorize Definition Endpoints", Menu = AuthorizeDefinitionConstants.ApplicationServices)]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var response = _applicationService.GetAuthorizeDefinitionEndPoints(typeof(Program));
            return Ok(response);
        }
    }
}
