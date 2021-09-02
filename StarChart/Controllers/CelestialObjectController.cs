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
           
           
            if(Co.Id == Co.OrbitedObjectId)
                Co.Satellites.Add(Co);
            return Ok(Co);
            
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var Co = _context.CelestialObjects.FirstOrDefault(op => op.Name == name);
            if (Co == null)
                return NotFound();
            if (Co.Id == Co.OrbitedObjectId)
                Co.Satellites.Add(Co);
            return Ok(Co);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
          var   Co = _context.CelestialObjects.ToList();
           
        for (var i =0; i< Co.Count; i++)
            {
                if (Co[i].Id == Co[i].OrbitedObjectId)
                    Co[i].Satellites.Add(Co[i]);
            }
            return Ok(Co);

        }
        
    }
}
