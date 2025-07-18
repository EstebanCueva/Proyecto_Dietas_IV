﻿using Microsoft.AspNetCore.Mvc;
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
        var lista = await _context.Usuario.ToListAsync();
        return Ok(lista);
    }

    // POST: api/testdb
    [HttpPost]
    public async Task<IActionResult> CrearUsuario([FromBody] Usuario nuevoUsuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        double tmb = (nuevoUsuario.Sex == "Masculino")
            ? 10 * nuevoUsuario.Weight + 6.25 * nuevoUsuario.Height - 5 * nuevoUsuario.Age + 5
            : 10 * nuevoUsuario.Weight + 6.25 * nuevoUsuario.Height - 5 * nuevoUsuario.Age - 161;

        double factorActividad = nuevoUsuario.Activity switch
        {
            "Baja" => 1.2,
            "Media" => 1.55,
            "Alta" => 1.9,
            _ => 1.2
        };

        nuevoUsuario.TotalCalories = (int)(tmb * factorActividad);

        _context.Usuario.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuarioPorId), new { id = nuevoUsuario.Id }, nuevoUsuario);
    }


    // GET auxiliar para CreatedAtAction
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioPorId(string id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    // PUT: api/testdb/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarUsuario(string id, [FromBody] Usuario usuarioActualizado)
    {
        if (id != usuarioActualizado.Id)
            return BadRequest("El ID no coincide.");

        var usuarioExistente = await _context.Usuario.FindAsync(id);
        if (usuarioExistente == null)
            return NotFound();

        // Actualizar propiedades
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

    // DELETE: api/testdb/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarUsuario(string id)
    {
        var usuario = await _context.Usuario.FindAsync(id);
        if (usuario == null)
            return NotFound();

        _context.Usuario.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
