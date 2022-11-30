namespace CQRS_with_event_Sourcing_pattern.Commands
{
    public class RemoveEmployeeFromLocationCommand : BaseCommand
    {
        public readonly int EmployeeID;
        public readonly int LocationID;

        public RemoveEmployeeFromLocationCommand(Guid id, int locationID, int employeeID)
        {
            Id = id;
            EmployeeID = employeeID;
            LocationID = locationID;
        }
    }
}
