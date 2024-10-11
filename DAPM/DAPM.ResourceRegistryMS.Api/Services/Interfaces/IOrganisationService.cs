namespace DAPM.ResourceRegistryMS.Api.Services.Interfaces
{
    public interface IOrganisationService
    {
        Task<Organisation> AddOrganisation(RabbitMQLibrary.Models.OrganizationDTO organisationDto);
    
    }
    
}

