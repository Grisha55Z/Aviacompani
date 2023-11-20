using Aviacompani.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aviacompani.Controllers
{
    [ApiController]
    [Route("/flights")]
    public class FlightsController : Controller
    {
        [HttpGet]

        public ActionResult GetAll()
        {
            var db = new KiselevContext();
            return Ok(db.Flights);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            var db = new KiselevContext();
            var clo = db.Flights.SingleOrDefault(s => s.Id == id);
            if (clo == null)
                return NotFound();
            return Ok(clo);
        }
        [HttpGet]
        public IActionResult Add(Flight flight)
        {
            var db = new KiselevContext();
            db.Flights.Add(flight);
            db.SaveChanges();
            return Ok(flight);
        }
        [HttpPut]
        public IActionResult Edit(Flight clis)
        {
            var db = new KiselevContext();
            db.Flights.Add(clis);
            db.SaveChanges();
            return Ok(clis);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var db = new KiselevContext();
            var flight = db.Flights.SingleOrDefault(s => s.Id == id);
            if (flight == null)
                return NotFound();
            return Ok(flight);
        }
    }
       
}
