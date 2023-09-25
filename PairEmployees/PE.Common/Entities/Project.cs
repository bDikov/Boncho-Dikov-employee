namespace PE.Common.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public HashSet<EmployeePartial> ProjectContributors { get; set; }
    }
}