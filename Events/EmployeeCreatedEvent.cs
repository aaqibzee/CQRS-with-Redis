using CQRS_with_event_Sourcing_pattern.Commands;

namespace CQRS_with_event_Sourcing_pattern.Events
{
    public class EmployeeCreatedEvent : BaseEvent
    {
        public readonly int EmployeeID;
        public readonly string FirstName;
        public readonly string LastName;
        public readonly DateTime DateOfBirth;
        public readonly string JobTitle;

        public EmployeeCreatedEvent(Guid id, int employeeID, string firstName, string lastName, DateTime dateOfBirth, string jobTitle)
        {
            Id = id;
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            JobTitle = jobTitle;
        }
    }
}