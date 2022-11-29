using CQRS_with_Redis.Models.Read;

namespace CQRS_with_Redis.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<EmployeeRM>
    {
        IEnumerable<EmployeeRM> GetAll();
    }
}
