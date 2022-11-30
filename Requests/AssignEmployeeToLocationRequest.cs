using CQRS_with_event_Sourcing_pattern.Repositories;
using FluentValidation;

namespace CQRS_with_Redis.Requests
{
    public class AssignEmployeeToLocationRequest
    {
        public int LocationID { get; set; }
        public int EmployeeID { get; set; }
    }

    public class AssignEmployeeToLocationRequestValidator : AbstractValidator<AssignEmployeeToLocationRequest>
    {
        public AssignEmployeeToLocationRequestValidator(IEmployeeRepository employeeRepo, ILocationRepository locationRepo)
        {
            RuleFor(x => x.LocationID).Must(x => locationRepo.Exists(x)).WithMessage("No Location with this ID exists.");
            RuleFor(x => x.EmployeeID).Must(x => employeeRepo.Exists(x)).WithMessage("No Employee with this ID exists.");
            RuleFor(x => new { x.LocationID, x.EmployeeID }).Must(x => !locationRepo.HasEmployee(x.LocationID, x.EmployeeID)).WithMessage("This Employee is already assigned to that Location.");
        }
    }
}
