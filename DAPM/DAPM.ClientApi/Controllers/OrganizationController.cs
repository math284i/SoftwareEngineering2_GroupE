using System.Reflection;
using DAPM.ClientApi.Models;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VerifierService;

namespace DAPM.ClientApi.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("organizations")]
    public class OrganizationController : ControllerBase
    {

        private readonly ILogger<OrganizationController> _logger;
        private readonly IOrganizationService _organizationService;

        public OrganizationController(ILogger<OrganizationController> logger, IOrganizationService organizationService)
        {
            _logger = logger;
            _organizationService = organizationService;
        }
        [HttpGet]
        //[HttpGet("{token}")]
        //[Verify(Roles.Admin)]
        [SwaggerOperation(Description = "Gets all peers (organizations) you are connected to. There has to be a collaboration agreement " +
            "and a handshake before you can see other organizations using this endpoint.")]
        public async Task<ActionResult<Guid>> Get()
        {
            if (!VerifierService.VerifierService.VerifyMethod(this, nameof(Get), ""))
            {
                return Unauthorized();
            }
            
            Guid id = _organizationService.GetOrganizations();
            return Ok(new ApiResponse { RequestName = "GetAllOrganizations", TicketId = id});
        }

        [HttpGet("{organizationId}")]
        // [HttpGet("{token} {organizationId}")]
        // [Verify(Roles.Admin)]
        [SwaggerOperation(Description = "Gets an organization by id. You need to have a collaboration agreement to retrieve this information.")]
        public async Task<ActionResult<Guid>> GetById(Guid organizationId)
        {
            if (!VerifierService.VerifierService.VerifyMethod(this, nameof(GetById), ""))
            {
                return Unauthorized();
            }
            Guid id = _organizationService.GetOrganizationById(organizationId);
            return Ok(new ApiResponse { RequestName = "GetOrganizationById", TicketId = id });
        }

        [HttpGet("{organizationId}/repositories")]
        //[HttpGet("{token} {organizationId}/repositories")]
        //[Verify(Roles.Admin)]
        [SwaggerOperation(Description = "Gets all the repositories of an organization by id. You need to have a collaboration agreement to retrieve this information.")]
        public async Task<ActionResult<Guid>> GetRepositoriesOfOrganization(Guid organizationId)
        {
            if (!VerifierService.VerifierService.VerifyMethod(this, nameof(GetRepositoriesOfOrganization), ""))
            {
                return Unauthorized();
            }
            Guid id = _organizationService.GetRepositoriesOfOrganization(organizationId);
            return Ok(new ApiResponse {RequestName = "GetRepositoriesOfOrganization", TicketId = id });
        }

        [HttpPost("{organizationId}/repositories")]
        //[HttpPost("{token} {organizationId}/repositories")]
        //[Verify(Roles.Admin)]
        [SwaggerOperation(Description = "Creates a new repository for an organization by id. Right now you can create repositories for any organizations, but ideally you would " +
            "only be able to create repositories for your own organization.")]
        public async Task<ActionResult<Guid>> PostRepositoryToOrganization(Guid organizationId, [FromBody] RepositoryApiDto repositoryDto)
        {
            if (!VerifierService.VerifierService.VerifyMethod(this, nameof(PostRepositoryToOrganization), ""))
            {
                return Unauthorized();
            }
            Guid id = _organizationService.PostRepositoryToOrganization(organizationId, repositoryDto.Name);
            return Ok(new ApiResponse { RequestName = "PostRepositoryToOrganization", TicketId = id });
        }

    }
}
