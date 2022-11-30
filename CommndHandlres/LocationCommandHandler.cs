using CQRS_with_event_Sourcing_pattern.Commands;
using CQRS_with_event_Sourcing_pattern.Models;
using CQRSlite.Commands;

namespace CQRS_with_event_Sourcing_pattern.CommndHandlres
{
    public class LocationCommandHandler : ICommandHandler<CreateLocationCommand>
    {
        private readonly CQRSlite.Domain.ISession _session;

        public LocationCommandHandler(CQRSlite.Domain.ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateLocationCommand command)
        {
            var location = new Location(command.Id, command.LocationID, command.StreetAddress, command.City, command.State, command.PostalCode);
            await _session.Add(location);
            await _session.Commit();
        }

        public async Task Handle(RemoveEmployeeFromLocationCommand command)
        {
            Location location = await _session.Get<Location>(command.Id);
            location.RemoveEmployee(command.EmployeeID);
            await _session.Commit();
        }
    }
}
