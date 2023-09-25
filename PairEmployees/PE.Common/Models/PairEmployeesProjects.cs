namespace PE.Common.Models
{
    public class PairEmployeesProjects
    {
        public int ProjectId { get; set; }
        public int EmployeeOneId { get; set; }
        public int EmployeeTwoId { get; set; }
        public int TotalDaysPerProject { get; set; }
    }
}