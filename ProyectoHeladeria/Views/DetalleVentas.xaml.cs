using Newtonsoft.Json;
using ProyectoHeladeria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    public partial class DetalleVentas : ContentPage
    {
        // Ver Venta 
        private const string UrlVenta3 = "http://192.168.56.1/heladeria/postVentas3.php?Usuario_idUsuario={0}&numeroVenta={1}";

        private const string UrlDetalleUnitario = "http://192.168.56.1/heladeria/postDetallesUnitario.php?Ventas_idVentas={0}";

        // actualizar venta
        private const string Url = "http://192.168.56.1/heladeria/postVentasUnitario.php?idVentas={0}&numeroVenta={1}";
        //

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<DetalleVenta> _post;

        public int idDetalleVentas = -1, Productos_idProductos, Ventas_idVentas, cantidad;
        double precio_venta;

        public int Usuario_idUsuario;
        
        // Insertar Venta Final
        public double PrecioFinal;

       

        public DetalleVentas(int idUsuario )
        {
            InitializeComponent();
            
            GetVenta(idUsuario);
            Usuario_idUsuario= idUsuario;

        }

        
        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync( new ListaProducto(Usuario_idUsuario));


        }
        private async void btnCompra_Clicked(object sender, EventArgs e)
        {

            bool res = await DisplayAlert("Confirmar ", "Realizar compra", "Aceptar", "Cancelar");

            if (res == true)
            {

                WebClient cliente = new WebClient();
                try
                {
                    using (var webClient = new WebClient())
                    {
                        var uri = new Uri(string.Format(Url,
                            lblNVenta.Text, "cancelado"));

                        webClient.UploadString(uri, "PUT", string.Empty);

                        await Navigation.PushAsync(new Principal());

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

        private void lstDetalles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (DetalleVenta)e.SelectedItem;
            idDetalleVentas = obj.idDetalleVentas;
            Productos_idProductos = obj.Productos_idProductos;
            Ventas_idVentas = obj.Ventas_idVentas;
            cantidad = obj.cantidad;
            precio_venta = obj.precio_venta;

        }
       

        public async void GetVenta(int IDusuario) {
            try
            {
                var uri = new Uri(string.Format(UrlVenta3, IDusuario, "pendiente"));
                var content = await client.GetStringAsync(uri);

                if (content != "false")
                {

                    Ventas post = JsonConvert.DeserializeObject<Ventas>(content);
                    entSubtotal.Text = post.precioTotal.ToString();
                    lblNVenta.Text = post.idVentas.ToString();
                    
                    /////////////////////////
                    try
                    {
                        var uri2 = new Uri(string.Format(UrlDetalleUnitario, post.idVentas.ToString()));
                        var content2 = await client.GetStringAsync(uri2);

                        if (content2 != "false")
                        {
                            
                            
                            List<DetalleVenta> post2 =
                            JsonConvert.DeserializeObject<List<DetalleVenta>>(content2);
                            _post = new ObservableCollection<DetalleVenta>(post2);
                            lstDetalles.ItemsSource = post2;
                            
                            
                        }
                        else
                        {
                            await DisplayAlert("Alerta", "No seleccionado", "Cerrar");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    /////////////////////////


                }
                else
                {

                    await DisplayAlert("error", "e", "e");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
             
            }

        }
    }
}