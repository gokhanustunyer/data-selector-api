using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YGKAPI.Application.Consts;
using YGKAPI.Application.CustomAttributes;
using YGKAPI.Application.Features.Commands.DbCredentials.CreateDbCredentials;
using YGKAPI.Application.Features.Queries.DbCredentials.GetCredentialList;
using YGKAPI.Application.Features.Queries.Table.DownloadTable;
using YGKAPI.Application.Features.Queries.Table.GetTableDatas;
using YGKAPI.Application.Features.Queries.Table.GetTableDatasBySqlQuery;
using YGKAPI.Application.Features.Queries.Table.GetTables;

namespace YGKAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class DbController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DbController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Writing, Definition = "Create Credentials", Menu = AuthorizeDefinitionConstants.DbCredentials)]
        public async Task<IActionResult> CreateCredential(CreateDbCredentialsCommandRequest createDbCredentialsCommandRequest)
        {
            createDbCredentialsCommandRequest.loggedUsername = User.Identity.Name;
            var response = await _mediator.Send(createDbCredentialsCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Credential List", Menu = AuthorizeDefinitionConstants.DbCredentials)]
        public async Task<IActionResult> GetCredentialList()
        {
            GetCredentialListQueryRequest request = new() { Username = User.Identity.Name};
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Tables", Menu = AuthorizeDefinitionConstants.DbCredentials)]
        public async Task<IActionResult> GetTables([FromQuery] GetTablesQueryRequest getTablesQueryRequest)
        {
            var response = await _mediator.Send(getTablesQueryRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Table Datas", Menu = AuthorizeDefinitionConstants.DbCredentials)]
        public async Task<IActionResult> GetTableDatas([FromQuery] GetTableDatasQueryRequest getTableDatasQueryRequest)
        {
            var response = await _mediator.Send(getTableDatasQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Get Table Datas By Sql Query", Menu = AuthorizeDefinitionConstants.DbCredentials)]
        public async Task<IActionResult> GetTableDatasBySQLQuery([FromBody] GetTableDatasBySqlQueryQueryRequest getTableDatasBySqlQueryQueryRequest)
        {
            var response = await _mediator.Send(getTableDatasBySqlQueryQueryRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition = "Download Table", Menu = AuthorizeDefinitionConstants.DbCredentials)]
        public async Task<IActionResult> DownloadTable([FromBody] DownloadTableQueryRequest downloadTableQueryRequest)
        {
            var response = await _mediator.Send(downloadTableQueryRequest);
            return Ok(response);
        }
    }
}
