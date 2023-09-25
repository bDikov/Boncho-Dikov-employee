namespace PE.BusinessLogic.Services
{
    using PE.BusinessLogic.Interfaces;
    using PE.Common.Entities;
    using PE.Common.Models;
    using PE.Repository.Interfaces;

    public class PairingService : IPairingService
    {
        private readonly IRepository repository;
        private readonly IEnumerable<Employee> employees;
        private readonly IEnumerable<Project> projects;

        public PairingService(IRepository repository)
        {
            this.repository = repository;
            this.projects = this.repository.GetProjects();
            this.employees = this.repository.GetEmployees();
        }

        public IEnumerable<PairEmployeesProjects> GetPairEmployeesProjects()
        {
            var result = new List<PairEmployeesProjects>();
            foreach (var project in projects)
            {
                if (project.ProjectContributors.Count > 0)
                {
                    result.AddRange(GetCommonEmployeesProjects(project, employees));
                }
            }

            return result;
        }

        private IEnumerable<PairEmployeesProjects> GetCommonEmployeesProjects(Project project, IEnumerable<Employee> employees)
        {
            var collectionPairs = new List<PairEmployeesProjects>();
            var remainingEmployees = employees.ToList();
            if (project.ProjectContributors.Count >= 2)
            {
                GetCommonProjectsForEmployeesHelper(project, remainingEmployees, new List<Employee>(), collectionPairs);
            }

            return collectionPairs;
        }

        private void GetCommonProjectsForEmployeesHelper(Project project, List<Employee> remainingEmployees, List<Employee> currentPair, List<PairEmployeesProjects> collectionPairs)
        {
            if (currentPair.Count == 2)
            {
                var totalDaysWorkedTogether = CalculateTotalDaysWorkedTogetherPerProject(currentPair[0], currentPair[1], project.ProjectId);
                if (totalDaysWorkedTogether > 0)
                {
                    var pairEmployeesProjects = new PairEmployeesProjects
                    {
                        ProjectId = project.ProjectId,
                        EmployeeOneId = currentPair[0].EmployeeId,
                        EmployeeTwoId = currentPair[1].EmployeeId,
                        TotalDaysPerProject = totalDaysWorkedTogether
                    };

                    if (!collectionPairs.Any(p => p.EmployeeOneId == pairEmployeesProjects.EmployeeTwoId && p.EmployeeTwoId == pairEmployeesProjects.EmployeeOneId))
                    {
                        collectionPairs.Add(pairEmployeesProjects);
                    }
                }
                return;
            }

            for (int i = 0; i < remainingEmployees.Count; i++)
            {
                var employee = remainingEmployees[i];
                currentPair.Add(employee);
                remainingEmployees.RemoveAt(i);
                GetCommonProjectsForEmployeesHelper(project, remainingEmployees, currentPair, collectionPairs);
                remainingEmployees.Insert(i, employee);
                currentPair.RemoveAt(currentPair.Count - 1);
            }
        }

        private int CalculateTotalDaysWorkedTogetherPerProject(Employee empIdOne, Employee empIdTwo, int projectId)
        {
            var dateOneFrom = empIdOne.Projects.Where(x => x.ProjectId == projectId).First().DateFrom;
            var dateTwoFrom = empIdTwo.Projects.Where(x => x.ProjectId == projectId).First().DateFrom;

            var dateOneTo = empIdOne.Projects.Where(x => x.ProjectId == projectId).First().DateTo;
            var dateTwoTo = empIdTwo.Projects.Where(x => x.ProjectId == projectId).First().DateTo;

            if (!IsSmallerData(dateOneFrom, dateTwoTo) || !IsSmallerData(dateTwoFrom, dateOneTo))
            {
                return 0;
            }

            var dateFrom = this.GetBiggerDate(dateOneFrom, dateTwoFrom);
            var dateTo = this.GetSmallestDate(dateOneTo, dateTwoTo);

            TimeSpan duration = dateTo.Subtract(dateFrom);
            return duration.Days;
        }

        private bool IsSmallerData(DateTime dateStart, DateTime dateEnd)
        {
            if (dateStart > dateEnd)
            {
                return false;
            }
            return true;
        }

        public DateTime GetSmallestDate(DateTime date1, DateTime date2)
        {
            return date1 < date2 ? date1 : date2;
        }

        public DateTime GetBiggerDate(DateTime date1, DateTime date2)
        {
            return date1 > date2 ? date1 : date2;
        }
    }
}