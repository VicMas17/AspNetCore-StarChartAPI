using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route ("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext ctx)
        {
            _context = ctx;

        }
        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var Co = _context.CelestialObjects.Find(id);
            if (Co == null)
                return NotFound();
            Co.Satellites = _context.CelestialObjects.Where(op => op.OrbitedObjectId == id).ToList();
            return Ok(Co);
            
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var Co = _context.CelestialObjects.Where(op => op.Name == name).ToList();
            if (!Co.Any())
                return NotFound();
            foreach (var obj in Co)
            {
                obj.Satellites = _context.CelestialObjects.Where(op => op.OrbitedObjectId == obj.Id).ToList();                
            }
            return Ok(Co);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
          var   Co = _context.CelestialObjects.ToList();

             foreach (var obj in Co)
            {
                obj.Satellites = _context.CelestialObjects.Where(op => op.OrbitedObjectId == obj.Id).ToList();
            }
            return Ok(Co);

        }
        
    }
}
