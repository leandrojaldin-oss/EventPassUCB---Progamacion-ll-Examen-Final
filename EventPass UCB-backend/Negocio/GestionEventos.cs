using Microsoft.Data.SqlClient;
using tiendaweb_backend.Datos;

namespace tiendaweb_backend.Negocio;

public class GestionEventos
{
    private string _connectionString =
        "Server=DESKTOP-H1RNTOQ;Database=EventPassDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public List<Evento> ListaEventos()
    {
        var lista = new List<Evento>();

        using (SqlConnection conexion = new SqlConnection(_connectionString))
        {
            conexion.Open();
            string query = "SELECT Id , Nombre , Ubicacion , PrecioEntrada  FROM Eventos";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        var evento = new Evento();
                        evento.Id = lector.GetInt32(0);
                        evento.Nombre = lector.GetString(1);
                        evento.Ubicacion = lector.GetString(2);
                        evento.PrecioEntrada = lector.GetDouble(3);

                        lista.Add(evento);
                    }
                }
            }
        }

        return lista;
    }

    public Evento CrearEvento(Evento nuevoEvento)
    {
        using (SqlConnection conexion = new SqlConnection(_connectionString))
        {
            conexion.Open();

            string query =
                "INSERT INTO Eventos (Nombre, Ubicacion, PrecioEntrada) OUTPUT INSERTED.Id VALUES (@Nombre, @Ubicacion, @PrecioEntrada)";

            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", nuevoEvento.Nombre);
                comando.Parameters.AddWithValue("@Ubicacion", nuevoEvento.Ubicacion);
                comando.Parameters.AddWithValue("@PrecioEntrada", nuevoEvento.PrecioEntrada);

                // Ejecutamos y capturamos el ID autogenerado por SQL
                nuevoEvento.Id = (int)comando.ExecuteScalar();
            }
        }

        return nuevoEvento;
    }
    
    
    // BUSCAR UN EVENTO POR ID
    public Evento BuscarPorId(int idABuscar)
    {
        Evento evento = null;
        using (SqlConnection conexion = new SqlConnection(_connectionString))
        {
            conexion.Open();
            // Usamos @Id para que SQL entienda que es una variable segura
            string query = "SELECT Id, Nombre, Ubicacion, PrecioEntrada FROM Eventos WHERE Id = @Id";
            
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@Id", idABuscar);
                
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read()) // Usamos 'if' en vez de 'while' porque solo esperamos 1 resultado
                    {
                        evento = new Evento();
                        evento.Id = lector.GetInt32(0);
                        evento.Nombre = lector.GetString(1);
                        evento.Ubicacion = lector.GetString(2);
                        evento.PrecioEntrada = lector.GetDouble(3);
                    }
                }
            }
        }
        return evento;
    }
    
    // ACTUALIZAR UN EVENTO
    public Evento ActualizarEvento(int id, Evento eventoEditado)
    {
        using (SqlConnection conexion = new SqlConnection(_connectionString))
        {
            conexion.Open();
            string query = "UPDATE Eventos SET Nombre = @Nombre, Ubicacion = @Ubicacion, PrecioEntrada = @PrecioEntrada WHERE Id = @Id";
            
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", eventoEditado.Nombre);
                comando.Parameters.AddWithValue("@Ubicacion", eventoEditado.Ubicacion);
                comando.Parameters.AddWithValue("@PrecioEntrada", eventoEditado.PrecioEntrada);
                comando.Parameters.AddWithValue("@Id", id);
                
                // ExecuteNonQuery se usa para INSERT, UPDATE y DELETE. Retorna cuántas filas se modificaron.
                int filasAfectadas = comando.ExecuteNonQuery(); 
                if (filasAfectadas == 0) return null; // Si es 0, no existía ese ID
            }
        }
        eventoEditado.Id = id;
        return eventoEditado;
    }
    
    // ELIMINAR UN EVENTO
    public bool EliminarEvento(int idABorrar)
    {
        using (SqlConnection conexion = new SqlConnection(_connectionString))
        {
            conexion.Open();
            string query = "DELETE FROM Eventos WHERE Id = @Id";
            
            using (SqlCommand comando = new SqlCommand(query, conexion))
            {
                comando.Parameters.AddWithValue("@Id", idABorrar);
                
                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0; // Si borró 1 o más filas, retorna true (éxito)
            }
        }
    }

}
