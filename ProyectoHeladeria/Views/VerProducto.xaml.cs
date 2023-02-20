using Newtonsoft.Json;
using ProyectoHeladeria.Models;
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

       

        //Actualizar el precio final en Ventas 
        private const string Url = "http://192.168.56.1/heladeria/postVentasUnitario.php?idVentas={0}&precioTotal={1}";

        // double precio_venta;

        public int Usuario_idUsuario;

        // traer el idvENTA
        public int IdVentas;

        // Insertar Venta Final
        public int cantidad = 1;

        //Calcular precio Fnal
        public double precioUnitario;
        public double PrecioFinal;

        //Calcular precio Total
        public double PrecioTotal;
        





        public VerProducto(int idProducto, string nombreProducto, string adereso, double precio, string sabor, int idUsuario, int idVenta, double precioTotal)
        {
            InitializeComponent();

            
            PrecioTotal = precioTotal;
            

           entIdProducto.Text = idProducto.ToString();

            if (String.IsNullOrEmpty(adereso.ToString()))
            {
                spnAdereso.Text = "No disponible";
            }
            else {
                spnAdereso.Text = adereso.ToString();
            }

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
            bool res = await DisplayAlert("Confirmar ", "Agregar Producto", "Aceptar", "Cancelar");
            if (res == true)
            {
                PrecioTotal = PrecioTotal + PrecioFinal;
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
                    parameters.Add("precio_venta", spnPrecioTotal.Text);
                    //parameters.Add("precio_venta", precioTotal.ToString().Replace(",","."));

                    detalle.UploadValues(UrlDetalle, "POST", parameters);

                    // await DisplayAlert("Agregado Correctamente ", PrecioFinal.ToString().Replace(",", "."), " Ok","Cancel");


                    // Limpiar();

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alerta ", ex.Message, " Cancelar ");
                    //mostrar errores en consola
                    Console.WriteLine(ex.Message, "error");
                }
                ///////////////
                /// ACtualizar precio final
              //  PrecioTotalFin = PrecioTotalFin + PrecioTotalAnt;

                WebClient cliente = new WebClient();
                try
                {
                    using (var webClient = new WebClient())
                    {
                        var uri = new Uri(string.Format(Url,
                            IdVentas.ToString(),PrecioTotal.ToString( )));

                        webClient.UploadString(uri, "PUT", string.Empty);
                        await Navigation.PushAsync(new DetalleVentas(Usuario_idUsuario));


                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alerta ", ex.Message, " Cerrar");

                }



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
            else if (operacion.Text == "-" && cantidad >= 2)
            {
                cantidad = cantidad - 1;
                lblCantidad.Text = cantidad.ToString();
                PrecioFinal = PrecioFinal - precioUnitario;
                spnPrecioTotal.Text = PrecioFinal.ToString().Replace(",", ".");
            }


        }
        

    }
}