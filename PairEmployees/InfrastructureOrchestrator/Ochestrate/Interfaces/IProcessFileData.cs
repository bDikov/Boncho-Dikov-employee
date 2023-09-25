namespace InfrastructureOrchestrator.Ochestrate.Interfaces
{
    using Common.Models;
    using InfrastructureOrchestrator.Infrastructure.Models.DTOs;
    using Microsoft.AspNetCore.Http;

    public interface IProcessFileData
    {
        InternalResult<IEnumerable<EmployeeDto>> ProcessFile(IFormFile file, CancellationToken cancellationToken);
    }
}