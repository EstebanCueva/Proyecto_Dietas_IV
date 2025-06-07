using Microsoft.AspNetCore.Mvc;
using Proyecto.Data;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class TestDbController : ControllerBase
{
    private readonly ProyectoContext _context;

    public TestDbController(ProyectoContext context)
    {
        _context = context;
    }

    // GET: api/testdb
    [HttpGet]
    public async Task<IActionResult> GetUsuariosSimples()
    {
        // Regresa todos los registros completos de la tabla Usuario
        var lista = await _context.Usuario.ToListAsync();

        return Ok(lista);
    }
    // POST: api/testdb
    [HttpPost]
    public async Task<IActionResult> CrearUsuario([FromBody] Usuario nuevoUsuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Usuario.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuarioPorId), new { id = nuevoUsuario.Id }, nuevoUsuario);
    }

    // GET auxiliar para CreatedAtAction
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioPorId(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    // PUT: api/testdb/5
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] Usuario usuarioActualizado)
    {
        if (id != usuarioActualizado.Id)
            return BadRequest("El ID no coincide.");

        var usuarioExistente = await _context.Usuario.FindAsync(id);
        if (usuarioExistente == null)
            return NotFound();

        // Actualizar propiedades (manual o con AutoMapper si prefieres)
        usuarioExistente.Name = usuarioActualizado.Name;
        usuarioExistente.Age = usuarioActualizado.Age;
        usuarioExistente.Description = usuarioActualizado.Description;
        usuarioExistente.Activity = usuarioActualizado.Activity;
        usuarioExistente.Sex = usuarioActualizado.Sex;
        usuarioExistente.Weight = usuarioActualizado.Weight;
        usuarioExistente.Height = usuarioActualizado.Height;
        usuarioExistente.TotalCalories = usuarioActualizado.TotalCalories;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/testdb/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarUsuario(int id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null)
            return NotFound();

        _context.Usuario.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}
