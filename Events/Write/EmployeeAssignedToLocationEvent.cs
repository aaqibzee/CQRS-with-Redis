using CQRS_with_Redis.Commands;

namespace CQRS_with_Redis.Events
{
    public class EmployeeAssignedToLocationEvent : BaseEvent
    {
        public readonly int NewLocationID;
        public readonly int EmployeeID;

        public EmployeeAssignedToLocationEvent(Guid id, int newLocationID, int employeeID)
        {
            Id = id;
            NewLocationID = newLocationID;
            EmployeeID = employeeID;
        }
    }
}