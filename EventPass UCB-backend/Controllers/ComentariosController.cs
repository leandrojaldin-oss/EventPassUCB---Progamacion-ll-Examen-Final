using Microsoft.AspNetCore.Mvc;
using tiendaweb_backend.Datos;
using tiendaweb_backend.Negocio;

namespace tiendaweb_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ComentariosController : ControllerBase
{
    private GestionComentarios _gestionComentarios;

    public ComentariosController()
    {
        _gestionComentarios = new GestionComentarios();
    }

    [HttpGet]
    public IEnumerable<Comentario> ConsultarTodos()
    {
        return _gestionComentarios.ObtenerTodos();
    }   

    [HttpGet("evento/{idEvento}")]
    public IEnumerable<Comentario> ConsultarPorEvento(int idEvento)
    {
        return _gestionComentarios.ObtenerPorEvento(idEvento);
    }

    [HttpGet("{id}")]
    public Comentario ConsultarPorId(int id)
    {
        return _gestionComentarios.BuscarPorId(id);
    }

    [HttpPost]
    public IActionResult Registrar([FromBody] Comentario nuevoComentario)
    {
        var resultado = _gestionComentarios.CrearComentario(nuevoComentario);
        if (resultado == null)
        {
            return BadRequest();
        }
        return Ok(resultado);
    }

    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var exito = _gestionComentarios.EliminarComentario(id);
        if (exito)
        {
            return Ok();
        }
        return NotFound();
    }
}