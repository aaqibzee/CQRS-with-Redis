using AutoMapper;
using CQRS_with_Redis.Events;
using CQRS_with_Redis.Models.Read;
using CQRS_with_Redis.Repositories;
using CQRSlite.Events;

namespace CQRS_with_Redis.EventHandlres
{
    public class LocationEventHandler : IEventHandler<LocationCreatedEvent>,
                                     IEventHandler<EmployeeAssignedToLocationEvent>,
                                     IEventHandler<EmployeeRemovedFromLocationEvent>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepo;
        private readonly IEmployeeRepository _employeeRepo;
        public LocationEventHandler(IMapper mapper, ILocationRepository locationRepo, IEmployeeRepository employeeRepo)
        {
            _mapper = mapper;
            _locationRepo = locationRepo;
            _employeeRepo = employeeRepo;
        }

        public async Task Handle(LocationCreatedEvent message)
        {
            //Create a new LocationDTO object from the LocationCreatedEvent
            LocationRM location = _mapper.Map<LocationRM>(message);

            _locationRepo.Save(location);
        }

        public async Task Handle(EmployeeAssignedToLocationEvent message)
        {
            var location = _locationRepo.GetByID(message.NewLocationID);
            location.Employees.Add(message.EmployeeID);
            _locationRepo.Save(location);

            //Find the employee which was assigned to this Location
            var employee = _employeeRepo.GetByID(message.EmployeeID);
            employee.LocationID = message.NewLocationID;
            _employeeRepo.Save(employee);
        }

        public async Task Handle(EmployeeRemovedFromLocationEvent message)
        {
            var location = _locationRepo.GetByID(message.OldLocationID);
            location.Employees.Remove(message.EmployeeID);
            _locationRepo.Save(location);
        }
    }
}
