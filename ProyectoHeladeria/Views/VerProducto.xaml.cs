﻿using ProyectoHeladeria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoHeladeria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerProducto : ContentPage
    {
        private const string UrlDetalle = "http://192.168.56.1/heladeria/postDetalles.php";

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<DetalleVenta> _post;

        public int idDetalleVentas = -1, Productos_idProductos, Ventas_idVentas;
       // double precio_venta;

        public int Usuario_idUsuario;

        // traer el idvENTA
        public int IdVentas;

        // Insertar Venta Final
        public int cantidad = 1;

        //Calcular precio Fnal
        public double precioUnitario;
        public double PrecioFinal;





        public VerProducto(int idProducto, string nombreProducto, string adereso, double precio, string sabor,int idUsuario, int idVenta)
        {
            InitializeComponent();
            entIdProducto.Text = idProducto.ToString();

            
            spnAdereso.Text = adereso.ToString();
            spnPrecio.Text = precio.ToString().Replace(",", ".");
            spnSabor.Text = sabor.ToString();
            entIdVenta.Text = idVenta.ToString();

            spnNombreProducto.Text = nombreProducto.ToString();

            precioUnitario = precio;
            PrecioFinal = precio;
            spnPrecioTotal.Text = PrecioFinal.ToString().Replace(",", ".");

            IdVentas = idVenta;
            Usuario_idUsuario = idUsuario;
          

            //setear catidad

            lblCantidad.Text = cantidad.ToString();

        }

        private async void btnAgregarProducto_Clicked(object sender, EventArgs e)
        {
            bool res= await DisplayAlert("Confirmar ","Agregar Producto", "Aceptar","Cancelar");
            if (res == true)
            {
                //Insertar en la base 

                //////////////////////////////
                WebClient detalle = new WebClient();
                try
                {
                    var parameters = new System.Collections.Specialized.NameValueCollection();

                    parameters.Add("idDetalleVentas", "");
                    parameters.Add("Productos_idProductos", entIdProducto.Text);
                    parameters.Add("Ventas_idVentas", IdVentas.ToString());
                    parameters.Add("cantidad", lblCantidad.Text);
                    parameters.Add("precio_venta",spnPrecioTotal.Text);
                    //parameters.Add("precio_venta", precioTotal.ToString().Replace(",","."));

                    detalle.UploadValues(UrlDetalle, "POST", parameters);

                    // await DisplayAlert("Agregado Correctamente ", PrecioFinal.ToString().Replace(",", "."), " Ok","Cancel");
                    await Navigation.PushAsync(new DetalleVentas(Usuario_idUsuario));

                    // Limpiar();

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alerta ", ex.Message, " Cancelar ");
                    //mostrar errores en consola
                    Console.WriteLine(ex.Message, "error");
                }
                ///////////////
            }
            else { 
            
            
            }

        
        }

        // Calcular la cantidad

        private async void btnCantidad(object sender, EventArgs e) {
            Button operacion = (Button)sender;
            
            if (operacion.Text == "+")
            {
                //Cantidad en el label
                cantidad = cantidad + 1;
                lblCantidad.Text = cantidad.ToString();

                //Calcular precio Total
                PrecioFinal = PrecioFinal + precioUnitario;
                spnPrecioTotal.Text = PrecioFinal.ToString().Replace(",", ".");

            }
            else if (operacion.Text == "-" && cantidad >=2)
            {
                cantidad = cantidad - 1;
                lblCantidad.Text = cantidad.ToString();
                PrecioFinal = PrecioFinal - precioUnitario;
                spnPrecioTotal.Text = PrecioFinal.ToString().Replace(",", ".");
            }


        }




    }
}