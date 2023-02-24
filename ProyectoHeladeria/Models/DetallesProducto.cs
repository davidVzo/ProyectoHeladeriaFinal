using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoHeladeria.Models
{
    public class DetallesProducto
    {
        public int idDetalleVentas { get; set; }
        public int Productos_idProductos { get; set; }

        public int Ventas_idVentas { get; set; }
        public int cantidad { get; set; }

        public double precio_venta { get; set; }

        // productos

        public int idProductos { get; set; }
        public string nombreProducto { get; set; }
        public double costo { get; set; }
        public string adereso { get; set; }
        public string sabor { get; set; }
        public string imagen { get; set; }


    }
}
