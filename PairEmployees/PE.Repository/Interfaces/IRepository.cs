namespace PE.Repository.Interfaces
{
    using PE.Common.Entities;

    public interface IRepository
    {
        IEnumerable<Employee> NormalizedData(IEnumerable<Employee> rawData);

        IEnumerable<Employee> GetEmployees();

        IEnumerable<Project> GetProjects();
    }
}