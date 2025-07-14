using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietasApiController : ControllerBase
    {
        private readonly ProyectoContext _context;

        public DietasApiController(ProyectoContext context)
        {
            _context = context;
        }

        // GET: api/DietasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dieta>>> GetDietas()
        {
            return await _context.Dieta.ToListAsync();
        }

        // GET: api/DietasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dieta>> GetDieta(int id)
        {
            var dieta = await _context.Dieta.FindAsync(id);

            if (dieta == null)
            {
                return NotFound();
            }

            return dieta;
        }

        // POST: api/DietasApi
        [HttpPost]
        public async Task<ActionResult<Dieta>> PostDieta(Dieta dieta)
        {
            _context.Dieta.Add(dieta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDieta), new { id = dieta.Id }, dieta);
        }

        // PUT: api/DietasApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDieta(int id, Dieta dieta)
        {
            if (id != dieta.Id)
            {
                return BadRequest();
            }

            _context.Entry(dieta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DietaExists(id))
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

        // DELETE: api/DietasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDieta(int id)
        {
            var dieta = await _context.Dieta.FindAsync(id);
            if (dieta == null)
            {
                return NotFound();
            }

            _context.Dieta.Remove(dieta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DietaExists(int id)
        {
            return _context.Dieta.Any(e => e.Id == id);
        }
    }
}
