using CQRS_with_event_Sourcing_pattern.Models.Read;
using StackExchange.Redis;

namespace CQRS_with_event_Sourcing_pattern.Repositories.Implementations
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(IConnectionMultiplexer redisConnection) : base(redisConnection, "employee") { }

        public EmployeeRM GetByID(int employeeID)
        {
            return Get<EmployeeRM>(employeeID);
        }

        public List<EmployeeRM> GetMultiple(List<int> employeeIDs)
        {
            return GetMultiple<EmployeeRM>(employeeIDs);
        }

        public IEnumerable<EmployeeRM> GetAll()
        {
            return Get<List<EmployeeRM>>("all");
        }

        public void Save(EmployeeRM employee)
        {
            Save(employee.EmployeeID, employee);
            MergeIntoAllCollection(employee);
        }

        private void MergeIntoAllCollection(EmployeeRM employee)
        {
            List<EmployeeRM> allEmployees = new List<EmployeeRM>();
            if (Exists("all"))
            {
                allEmployees = Get<List<EmployeeRM>>("all");
            }

            //If the district already exists in the ALL collection, remove that entry
            if (allEmployees.Any(x => x.EmployeeID == employee.EmployeeID))
            {
                allEmployees.Remove(allEmployees.First(x => x.EmployeeID == employee.EmployeeID));
            }

            //Add the modified district to the ALL collection
            allEmployees.Add(employee);

            Save("all", allEmployees);
        }
    }
}
