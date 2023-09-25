namespace PE.Common.Entities
{
    public class Employee
    {
        public Employee()
        {
            this.Projects = new HashSet<ProjectPartial>();
        }

        public int EmployeeId { get; set; }

        public HashSet<ProjectPartial> Projects { get; set; }
    }
}