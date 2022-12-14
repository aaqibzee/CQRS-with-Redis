using CQRS_with_event_Sourcing_pattern.Repositories;
using FluentValidation;

namespace CQRS_with_Redis.Requests
{
    public class CreateLocationRequest
    {
        public int LocationID { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }

    public class CreateLocationRequestValidator : AbstractValidator<CreateLocationRequest>
    {
        public CreateLocationRequestValidator(ILocationRepository locationRepo)
        {
            RuleFor(x => x.LocationID).Must(x => !locationRepo.Exists(x)).WithMessage("A Location with this ID already exists.");
            RuleFor(x => x.StreetAddress).NotNull().NotEmpty().WithMessage("The Street Address cannot be null");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("The City cannot be null");
            RuleFor(x => x.State).NotNull().NotEmpty().WithMessage("The State cannot be null");
            RuleFor(x => x.PostalCode).NotNull().NotEmpty().WithMessage("The Postal Code cannot be null");
        }
    }
}
