using AutoMapper;
using CQRS_with_Redis.Events;
using CQRS_with_Redis.Models.Read;
using CQRS_with_Redis.Repositories;
using CQRSlite.Events;

namespace CQRS_with_Redis.EventHandlres
{
    public class EmployeeEventHandler : IEventHandler<EmployeeCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeEventHandler(IMapper mapper, IEmployeeRepository employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }

        public async Task Handle(EmployeeCreatedEvent message)
        {
            EmployeeRM employee = _mapper.Map<EmployeeRM>(message);
            _employeeRepo.Save(employee);
        }
    }
}
