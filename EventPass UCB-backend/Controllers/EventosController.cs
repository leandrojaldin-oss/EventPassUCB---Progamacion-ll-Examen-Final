using Microsoft.AspNetCore.Mvc;
using tiendaweb_backend.Datos;
using tiendaweb_backend.Negocio;

namespace tiendaweb_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class EventosController : ControllerBase
{
    private GestionEventos _gestionEventos;

    public EventosController()
    {
        _gestionEventos = new GestionEventos();
    }

    [HttpGet("lista-eventos")]
    public IEnumerable<Evento> ListaEventos()
    {
        return _gestionEventos.ListaEventos();
    }

    [HttpGet("buscar/{id}")]
    public Evento? BuscarEvento(int id)
    {
        return _gestionEventos.BuscarPorId(id);
    }

    [HttpPost("crear")]
    public Evento Crear([FromBody] Evento nuevoEvento)
    {
        return _gestionEventos.CrearEvento(nuevoEvento);
    }

    [HttpDelete("borrar/{id}")]
    public bool Borrar(int id)
    {
        return _gestionEventos.EliminarEvento(id);
    }
    
}