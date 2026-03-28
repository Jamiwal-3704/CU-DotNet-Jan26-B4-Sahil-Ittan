using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrackingService.Controllers
{
    [ApiController]
    [Route("api/tracking")]
    public class TrackingController : ControllerBase
    {
        [HttpGet("gps")]

        [Authorize(Roles = "Manager")]
        public IActionResult GetGpsData()
        {
            return Ok(new
            {
                TruckId = "TRUCK-101",
                Location = "Punjab, India",
                Speed = "65 km/h"
            });
        }
    }
}
