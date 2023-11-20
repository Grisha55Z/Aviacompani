using Aviacompani.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aviacompani.Controllers
{
    [ApiController]
    [Route("/planes")]
    public class PlanesController : ControllerBase
    {
        [HttpGet]

        public ActionResult GetAll()
        {
            var db = new KiselevContext();
            return Ok(db.Planes);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id) 
        {
            var db = new KiselevContext();
            var clo = db.Planes.SingleOrDefault(s => s.Id == id);
            if (clo == null)
                return NotFound();
            return Ok(clo);
        }
        [HttpPost]
        public IActionResult Add(Plane planes) 
        {
            var db = new KiselevContext();
            db.Planes.Add(planes);
            db.SaveChanges();
            return Ok(db); 
        }

        [HttpPut]
        public IActionResult Edit(Plane clis)
        {
            var db = new KiselevContext();
            db.Planes.Add(clis);
            db.SaveChanges();
            return Ok(clis);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var db = new KiselevContext();
            var Planes = db.Planes.SingleOrDefault(s => s.Id ==id);
            if (Planes == null)
                return NotFound();
            return Ok(Planes);
        }
        
    }
}