namespace CQRS_with_event_Sourcing_pattern.Commands
{
    public class AssignEmployeeToLocationCommand : BaseCommand
    {
        public readonly int EmployeeID;
        public readonly int LocationID;

        public AssignEmployeeToLocationCommand(int locationID, int employeeID)
        {
            LocationID = locationID;
            EmployeeID = employeeID;
        }

        public AssignEmployeeToLocationCommand(Guid id, int locationID, int employeeID)
        {
            Id = id;
            EmployeeID = employeeID;
            LocationID = locationID;
        }
    }
}
