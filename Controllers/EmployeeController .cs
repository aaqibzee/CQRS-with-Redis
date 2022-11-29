using CQRS_with_Redis.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
namespace CQRS_with_Redis.Controllers
{

    [ApiController]
    [System.Web.Http.Route("employees")]
    public class EmployeeController: ControllerBase
    {
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
    }
}
}