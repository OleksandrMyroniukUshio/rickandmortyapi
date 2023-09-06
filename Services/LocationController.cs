using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RickAndMortyAPI.Entities;
using System.Xml.Linq;

namespace RickAndMortyAPI.Services
{
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet]
        [EndpointSummary("Get all locations.")]
        [Tags("Locations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Route("api/locations")]
        public async Task<IActionResult> GetLocationsAsync()
        {
            var locations = new List<LocationDTO>();
            foreach (var location in await _locationService.GetLocationsAsync())
            {
                locations.Add(location);
            }
            return Ok(locations);
        }
        [HttpGet]
        [EndpointSummary("Get locations by id.")]
        [Tags("Locations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("api/location")]
        public async Task<IActionResult> GetLocationsByIdAsync([FromQuery(Name = "locationID")] int locationID)
        {
            var locations = await _locationService.GetLocationAsync(locationID);
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(locations);
        }
    }
}
