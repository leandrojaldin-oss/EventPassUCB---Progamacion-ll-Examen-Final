namespace tiendaweb_backend.Datos;
public class Evento
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Ubicacion { get; set; }
    public double PrecioEntrada { get; set; }
    
    public Categoria CategoriaDelEvento { get; set; }
    
    public List<Ticket> TicketsVendidos { get; set; } = new List<Ticket>();
}