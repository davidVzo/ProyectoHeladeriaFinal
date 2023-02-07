using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoHeladeria.Models
{
    public class Cliente
    {
        public int idUsuario { get; set; }
        public string cedula { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public int estado { get; set; }
        public string imagen { get; set; }
        public int Perfil_idPerfil { get; set; }
    }
}
