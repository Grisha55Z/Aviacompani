using Microsoft.AspNetCore.Mvc;
using Aviacompani.Models;

namespace Aviacompani.Controllers
{
    [ApiController]
    [Route("/Flights")]
    public class FlightsController : ControllerBase
    {
        [HttpGet]

        public ActionResult GetAll()
        {
            KiselevContext db = new KiselevContext();
            return Ok(db.Flights);
        }
       
    }
}