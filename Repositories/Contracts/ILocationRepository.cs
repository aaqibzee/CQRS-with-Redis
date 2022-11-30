using CQRS_with_event_Sourcing_pattern.Models.Read;
namespace CQRS_with_event_Sourcing_pattern.Repositories
{
    public interface ILocationRepository : IBaseRepository<LocationRM>
    {
        IEnumerable<LocationRM> GetAll();
        IEnumerable<EmployeeRM> GetEmployees(int locationID);
        bool HasEmployee(int locationID, int employeeID);
    }
}