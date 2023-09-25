namespace InfrastructureOrchestrator.Infrastructure.Models.DTOs
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
            this.Projects = new List<ProjectDto>();
        }
        public int EmployeeId { get; set; }

        public List<ProjectDto> Projects { get; set; }
    }
}