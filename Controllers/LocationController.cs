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
    [System.Web.Http.Route("Locations")]
    public class LocationController: ControllerBase
    {
        #region Read
        private readonly ILocationRepository _locationRepo;

        public LocationController(ILocationRepository LocationRepo)
        {
            _locationRepo = LocationRepo;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}")]
        public IActionResult GetByID(int id)
        {
            var Location = _locationRepo.GetByID(id);

            //It is possible for GetByID() to return null.
            //If it does, we return HTTP 400 Bad Request
            if (Location == null)
            {
                return BadRequest("No Location with ID " + id.ToString() + " was found.");
            }

            //Otherwise, we return the Location
            return Ok(Location);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("all")]
        public IActionResult GetAll()
        {
            var Locations = _locationRepo.GetAll();
            return Ok(Locations);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}/employees")]
        public IActionResult Get(int id)
        {
            var employees = _locationRepo.GetEmployees(id);
            return Ok(employees);
        }
        #endregion 

        #region Write
        private IMapper _mapper;
        private ICommandSender _commandSender;
        private IEmployeeRepository _employeeRepo;

        public LocationController(ICommandSender commandSender, IMapper mapper, ILocationRepository locationRepo, IEmployeeRepository employeeRepo)
        {
            _commandSender = commandSender;
            _mapper = mapper;
            _locationRepo = locationRepo;
            _employeeRepo = employeeRepo;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("create")]
        public IActionResult Create(CreateLocationRequest request)
        {
            var command = _mapper.Map<CreateLocationCommand>(request);
            _commandSender.Send(command);
            return Ok();
        }
        #endregion

        #region assignLocation
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("assignemployee")]
        public IActionResult AssignEmployee(AssignEmployeeToLocationRequest request)
        {
            var employee = _employeeRepo.GetByID(request.EmployeeID);
            if (employee.LocationID != 0)
            {
                var oldLocationAggregateID = _locationRepo.GetByID(employee.LocationID).AggregateID;

                RemoveEmployeeFromLocationCommand removeCommand = new RemoveEmployeeFromLocationCommand(oldLocationAggregateID, request.LocationID, employee.EmployeeID);
                _commandSender.Send(removeCommand);
            }

            var locationAggregateID = _locationRepo.GetByID(request.LocationID).AggregateID;
            var assignCommand = new AssignEmployeeToLocationCommand(locationAggregateID, request.LocationID, request.EmployeeID);
            _commandSender.Send(assignCommand);

            return Ok();
        }
        #endregion
    }
}