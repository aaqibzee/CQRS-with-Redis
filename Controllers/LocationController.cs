using CQRS_with_Redis.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
namespace CQRS_with_Redis.Controllers
{

    [ApiController]
    [System.Web.Http.Route("Locations")]
    public class LocationController: ControllerBase
    {
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
        public IActionResult Get()
        {
            var employees = _locationRepo.GetEmployees(id);
            return Ok(employees);
        }
    }
}