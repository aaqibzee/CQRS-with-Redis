using CQRS_with_Redis.Commands;
using CQRS_with_Redis.Models;
using CQRSlite.Commands;

namespace CQRS_with_Redis.CommndHandlres
{
    public class EmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand>
    {
        private readonly CQRSlite.Domain.ISession _session;

        public EmployeeCommandHandler(CQRSlite.Domain.ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateEmployeeCommand command)
        {
            Employee employee = new Employee(command.Id, command.EmployeeID, command.FirstName, command.LastName, command.DateOfBirth, command.JobTitle);
            await _session.Add(employee);
            await _session.Commit();
        }
    }
}
