namespace PE.Repository.Services
{
    using PE.Common.Entities;
    using PE.Repository.Interfaces;

    // this is just a demo to repository. My Idea is to separate Data from the Services working with it so we can do it with repository pattern (generic one as
    // well but not much time) so we can change the DB instead of modifing the services. Here can be added. Much more can be done but we can discuss it later
    // For the demo I will not use db
    public class Repository : IRepository
    {
        public static Dictionary<int, Employee> Employees { get; } = new Dictionary<int, Employee>();
        public static Dictionary<int, Project> Projects { get; } = new Dictionary<int, Project>();

        public IEnumerable<Employee> NormalizedData(IEnumerable<Employee> rawData)
        {
            foreach (var employee in rawData)
            {
                if (Employees.ContainsKey(employee.EmployeeId))
                {
                    foreach (var project in employee.Projects)
                    {
                        Employees[employee.EmployeeId].Projects.Add(project);
                    }
                }
                else
                {
                    Employees.Add(employee.EmployeeId, employee);
                }
                this.AddProjectInMemory(employee);
            }

            return Employees.Values.ToList();
        }

        private void AddProjectInMemory(Employee employee)
        {
            foreach (var project in employee.Projects)
            {
                if (Projects.ContainsKey(project.ProjectId))
                {
                    Projects[project.ProjectId].ProjectContributors.Add(new EmployeePartial() { DateFrom = project.DateFrom, DateTo = project.DateTo, EmployeeId = employee.EmployeeId });
                }
                else
                {
                    Projects.Add(project.ProjectId, new Project
                    {
                        ProjectId = project.ProjectId,
                        ProjectContributors = new HashSet<EmployeePartial>
                        {
                            new EmployeePartial
                            {
                                EmployeeId = employee.EmployeeId
                            }
                        }
                    });
                }
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return Employees.Select(kv => kv.Value);
        }

        public IEnumerable<Project> GetProjects()
        {
            return Projects.Select(kv => kv.Value);
        }
    }
}