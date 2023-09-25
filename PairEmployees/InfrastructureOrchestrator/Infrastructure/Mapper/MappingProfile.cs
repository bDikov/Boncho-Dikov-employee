using AutoMapper;
using InfrastructureOrchestrator.Infrastructure.Models.DTOs;
using PE.Common.Entities;

namespace InfrastructureOrchestrator.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeFile, EmployeeDto>()
           .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmpID))
           .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => new List<ProjectDto>
           {
                new ProjectDto
                {
                    ProjectId = src.ProjectID,
                    DateFrom = src.DateFrom ?? DateTime.Now,
                    DateTo = src.DateTo ?? DateTime.Now
                }
           }));

            CreateMap<EmployeeFile, Employee>()
           .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmpID))
           .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => new List<ProjectDto>
           {
                new ProjectDto
                {
                    ProjectId = src.ProjectID,
                    DateFrom = src.DateFrom ?? DateTime.Now,
                    DateTo = src.DateTo ?? DateTime.Now
                }
           }));

            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
            .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects))
            .ReverseMap();

            CreateMap<ProjectPartial, ProjectDto>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.DateFrom, opt => opt.MapFrom(src => src.DateFrom))
                .ForMember(dest => dest.DateTo, opt => opt.MapFrom(src => src.DateTo))
                .ReverseMap();
        }
    }
}