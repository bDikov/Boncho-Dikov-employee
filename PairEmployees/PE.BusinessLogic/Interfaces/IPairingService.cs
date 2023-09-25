using PE.Common.Entities;
using PE.Common.Models;

namespace PE.BusinessLogic.Interfaces
{
    public interface IPairingService
    {
        IEnumerable<PairEmployeesProjects> GetPairEmployeesProjects();
    }
}