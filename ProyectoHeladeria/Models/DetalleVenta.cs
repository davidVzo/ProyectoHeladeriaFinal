using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoHeladeria.Models
{
    public class DetalleVenta
    {
        public int idDetalleVentas { get; set; }
        public int Productos_idProductos { get; set; }

        public int Ventas_idVentas { get; set; }
        public int cantidad { get; set; }

        public double precio_venta { get; set; }
    }
}
