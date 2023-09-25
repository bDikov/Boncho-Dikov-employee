namespace InfrastructureOrchestrator.Infrastructure.Models.DTOs
{
    public class PairDto
    {
        public EmployeeDto EmployeeOne { get; set; }

        public EmployeeDto EmployeeTwo { get; set; }

        public int DaysWorkedOnProject { get; set; }
    }
}