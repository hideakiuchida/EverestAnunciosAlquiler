using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.Entities
{
    public class UsuarioEntity
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdRol { get; set; }
    }
}
