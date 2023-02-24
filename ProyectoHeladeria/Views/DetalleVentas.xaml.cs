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

        private const string UrlDetallesProducto = "http://192.168.56.1/heladeria/postDetallesProducto.php?Ventas_idVentas={0}";

        //private const string UrlDetalleUnitario = "http://192.168.56.1/heladeria/postDetallesUnitario.php?Ventas_idVentas={0}";

        // actualizar venta
        private const string Url = "http://192.168.56.1/heladeria/postVentasUnitario.php?idVentas={0}&numeroVenta={1}";
        private const string UrlActualizar = "http://192.168.56.1/heladeria/postVentasUnitario.php?idVentas={0}&precioTotal={1}";
        //

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<DetallesProducto> _post;

        public int idDetalleVentas = -1, Productos_idProductos, Ventas_idVentas, cantidad;
        double precio_venta;
        string nombreProducto;
        public int Usuario_idUsuario;

        // Insertar Venta Final
        double SubTotal;

       

        public DetalleVentas(int idUsuario )
        {
            InitializeComponent();
            
            GetVenta(idUsuario);
            Usuario_idUsuario= idUsuario;
           
            


        }

        private void lstDetalles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as DetallesProducto ;
            idDetalleVentas = itemSelected.idDetalleVentas;
            Productos_idProductos = itemSelected.Productos_idProductos;
            Ventas_idVentas = itemSelected.Ventas_idVentas;
            cantidad = itemSelected.cantidad;
            precio_venta = itemSelected.precio_venta;
            nombreProducto = itemSelected.nombreProducto;
        }

      

        private async void Eliminar_Clicked(object sender, EventArgs e)
        {
            if (idDetalleVentas > 0)
            {
                bool res = await DisplayAlert("Confirmar ", "Desea eliminar "+nombreProducto.ToString(), "Aceptar", "Cancelar");

                if (res == true)
                {

                    ///////////
                    // Actualizar el precio
                    double precioRest = SubTotal- precio_venta;
                    spnSubtotal.Text= precioRest.ToString();
                    WebClient cliente = new WebClient();
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            var uri = new Uri(string.Format(UrlActualizar,
                               lblNVenta.Text,precioRest.ToString().Replace(",", ".")));

                            webClient.UploadString(uri, "PUT", string.Empty);

                          //  await Navigation.PushAsync(new Principal());

                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alerta ", ex.Message, " Cerrar");

                    }





                    /////////

                    string Uri = "http://192.168.56.1/heladeria/postDetallesProducto.php?idDetalleVentas={0}";

                    try
                    {
                        HttpClient client = new HttpClient();
                        var uri = new Uri(string.Format(Uri, idDetalleVentas.ToString()));
                        var result = await client.DeleteAsync(uri);
                        if (result.IsSuccessStatusCode)
                        {
                            GetVenta(Usuario_idUsuario);
                   

                            // await DisplayAlert("succes", " Estudiante " + nombre + " " + apellido + " eliminado;", " okey ");
                        }
                        else
                        {
                            await DisplayAlert("Error", "Error consulte con el administrador.", " Ok");
                        }

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alerta", ex.Message, "Ok");

                    }



                }
                else
                {
                }

            }
            else {
                await DisplayAlert("Error","No se ha escogido","ok");
            
            }
          
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

    


        public async void GetVenta(int IDusuario) {
            try
            {
                var uri = new Uri(string.Format(UrlVenta3, IDusuario, "pendiente"));
                var content = await client.GetStringAsync(uri);

                if (content != "false")
                {

                    Ventas post = JsonConvert.DeserializeObject<Ventas>(content);
                    
                    spnSubtotal.Text = post.precioTotal.ToString();
                    
                    lblNVenta.Text = post.idVentas.ToString();
                    /////
                    //CALCULAR 
                   
                    double subtotal = post.precioTotal;
                    SubTotal = post.precioTotal;
                    double iva;
                    double preciototal;
                    
                    iva = subtotal * 0.12;
                    spnIVA.Text = iva.ToString();
                    preciototal = subtotal + iva;
                    spnPRECIOFINAL.Text = preciototal.ToString();

                    /////////////////////////
                    try
                    {
                        var uri2 = new Uri(string.Format(UrlDetallesProducto, post.idVentas.ToString()));
                        var content2 = await client.GetStringAsync(uri2);

                        if (content2 != "false")
                        {
                            
                            
                            List<DetallesProducto> post2 =
                            JsonConvert.DeserializeObject<List<DetallesProducto>>(content2);
                            _post = new ObservableCollection<DetallesProducto>(post2);
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