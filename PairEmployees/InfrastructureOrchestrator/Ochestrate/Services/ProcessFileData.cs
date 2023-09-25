namespace InfrastructureOrchestrator.Ochestrate.Services
{
    using AutoMapper;
    using Common.Models;
    using InfrastructureOrchestrator.Infrastructure.Models.DTOs;
    using InfrastructureOrchestrator.Ochestrate.Interfaces;
    using Microsoft.AspNetCore.Http;
    using PE.Common.Entities;
    using PE.Common.Interfaces;
    using PE.Repository.Interfaces;
    using System.Collections.Generic;

    public class ProcessFileData : IProcessFileData
    {
        private readonly ICsvSerializer csvSerializer;
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public ProcessFileData(ICsvSerializer csvSerializer, IMapper mapper, IRepository repository)
        {
            this.mapper = mapper;
            this.csvSerializer = csvSerializer;
            this.repository = repository;
        }

        InternalResult<IEnumerable<EmployeeDto>> IProcessFileData.ProcessFile(IFormFile file, CancellationToken cancellationToken)
        {
            var data = new List<EmployeeFile>();
            if (file != null)
            {
                using (var stream = file.OpenReadStream())
                {
                    data = csvSerializer.DeserializeAll<EmployeeFile>(stream).ToList();
                }

                var result = mapper.Map<IEnumerable<EmployeeDto>>(repository.NormalizedData(mapper.Map<IEnumerable<Employee>>(data)));
                return new InternalResult<IEnumerable<EmployeeDto>>(result, 200);
            }
            return new InternalResult<IEnumerable<EmployeeDto>>(null, 500);
        }
    }
}