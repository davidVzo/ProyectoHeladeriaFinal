using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoHeladeria.Models
{
    public class Ventas
    {
        public int idVentas { get; set; }
        public string numeroVenta { get; set; }
        public string fecha { get; set; }
        public double precioTotal { get; set; }
        public int Usuario_idUsuario { get; set; }
        public int Clientes_idUsuario { get; set; }



    }
}
