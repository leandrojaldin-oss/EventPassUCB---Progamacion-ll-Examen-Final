    using tiendaweb_backend.Datos;

    namespace tiendaweb_backend.Negocio;

    public class GestionComentarios
    {
        private static List<Comentario> _comentariosDB = new List<Comentario>();
        private GestionEventos _gestionEventos = new GestionEventos();

        public List<Comentario> ObtenerTodos()
        {
            return _comentariosDB;
        }

        public List<Comentario> ObtenerPorEvento(int idEvento)
        {
            var resultado = new List<Comentario>();
            for (int i = 0; i < _comentariosDB.Count; i++)
            {
                if (_comentariosDB[i].IdEvento == idEvento)
                {
                    resultado.Add(_comentariosDB[i]);
                }
            }

            return resultado;
        }
        
        public Comentario BuscarPorId(int id)
        {
            for (int i = 0; i < _comentariosDB.Count; i++)
            {
                if (_comentariosDB[i].Id == id)
                {
                    return _comentariosDB[i];
                }
            }

            return null;
        }
        public Comentario CrearComentario(Comentario nuevoComentario)
        {
            if (nuevoComentario.Texto == null || nuevoComentario.Texto == "")
            {
                return null; 
            }

            var eventoExiste = _gestionEventos.BuscarPorId(nuevoComentario.IdEvento);
            if (eventoExiste == null)
            {
                return null; 
            }

            nuevoComentario.Id = _comentariosDB.Count + 1;
            nuevoComentario.FechaCreacion = DateTime.Now;

            _comentariosDB.Add(nuevoComentario);
            return nuevoComentario;
        }
        
        public Comentario ActualizarComentario(int id, string nuevoTexto)
        {
            if (nuevoTexto == null || nuevoTexto == "")
            {
                return null;
            }

            var comentarioExistente = BuscarPorId(id);
            if (comentarioExistente != null)
            {
                comentarioExistente.Texto = nuevoTexto;
                return comentarioExistente;
            }
            return null;
        }
        
        public bool EliminarComentario(int id)
        {
            var comentarioExistente = BuscarPorId(id);
            if (comentarioExistente != null)
            {
                _comentariosDB.Remove(comentarioExistente);
                return true;
            }
            return false;
        }
    }