using AutoMapper;
using CQRS_with_event_Sourcing_pattern.Commands;
using CQRS_with_event_Sourcing_pattern.Repositories;
using CQRS_with_Redis.Requests;
using CQRSlite.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
namespace CQRS_with_event_Sourcing_pattern.Controllers
{

    [ApiController]
    [System.Web.Http.Route("employees")]
    public class EmployeeController: ControllerBase
    {
        #region Read
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}")]
        public IActionResult GetByID(int id)
        {
            var employee = _employeeRepo.GetByID(id);

            //It is possible for GetByID() to return null.
            //If it does, we return HTTP 400 Bad Request
            if (employee == null)
            {
                return BadRequest("No Employee with ID " + id.ToString() + " was found.");
            }

            //Otherwise, we return the employee
            return Ok(employee);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("all")]
        public IActionResult GetAll()
        {
            var employees = _employeeRepo.GetAll();
            return Ok(employees);
        }
        #endregion

        #region Write
        private IMapper _mapper;
        private ICommandSender _commandSender;

        public EmployeeController(ICommandSender commandSender, IMapper mapper)
        {
            _commandSender = commandSender;
            _mapper = mapper;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("create")]
        public IActionResult Create(CreateEmployeeRequest request)
        {
            var command = _mapper.Map<CreateEmployeeCommand>(request);
            _commandSender.Send(command);

            var assignCommand = new AssignEmployeeToLocationCommand(request.LocationID, request.EmployeeID);
            _commandSender.Send(assignCommand);
            return Ok();
        }
        #endregion
    }
}