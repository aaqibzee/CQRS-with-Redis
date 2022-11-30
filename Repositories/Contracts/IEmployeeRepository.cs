using CQRS_with_event_Sourcing_pattern.Models.Read;

namespace CQRS_with_event_Sourcing_pattern.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<EmployeeRM>
    {
        IEnumerable<EmployeeRM> GetAll();
    }
}
