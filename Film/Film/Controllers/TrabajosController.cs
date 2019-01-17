using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Film.Models;

namespace Film.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajosController : ControllerBase
    {
        private readonly ApplicatonDbContext _context;

        public TrabajosController(ApplicatonDbContext context)
        {
            _context = context;
        }

        // GET: api/Trabajos
        [HttpGet]
        public IEnumerable<Trabajo> GetTrabajo()
        {
            return _context.Trabajo;
        }

        // GET: api/Trabajos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabajo([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trabajo = await _context.Trabajo.FindAsync(id);

            if (trabajo == null)
            {
                return NotFound();
            }

            return Ok(trabajo);
        }

        // PUT: api/Trabajos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrabajo([FromRoute] string id, [FromBody] Trabajo trabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trabajo.Id)
            {
                return BadRequest();
            }

            _context.Entry(trabajo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabajoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trabajos
        [HttpPost]
        public async Task<IActionResult> PostTrabajo([FromBody] Trabajo trabajo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trabajo.Add(trabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrabajo", new { id = trabajo.Id }, trabajo);
        }

        // DELETE: api/Trabajos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrabajo([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trabajo = await _context.Trabajo.FindAsync(id);
            if (trabajo == null)
            {
                return NotFound();
            }

            _context.Trabajo.Remove(trabajo);
            await _context.SaveChangesAsync();

            return Ok(trabajo);
        }

        private bool TrabajoExists(string id)
        {
            return _context.Trabajo.Any(e => e.Id == id);
        }
    }
}