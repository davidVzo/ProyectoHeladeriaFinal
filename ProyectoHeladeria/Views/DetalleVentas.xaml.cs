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
        
        

        private const string UrlDetalle = "http://192.168.56.1/heladeria/postDetalles.php";
        // Ver Detalle

        //private readonly HttpClient client = new HttpClient();
        //public ObservableCollection<DetalleVenta> _post;
       

        //private const string UrlDetalleUnitario = "http://192.168.56.1/heladeria/postDetallesUnitario.php?Ventas_idVentas={0}";

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<DetalleVenta> _post;

        public int idDetalleVentas = -1, Productos_idProductos, Ventas_idVentas, cantidad;
        double precio_venta;

        public int Usuario_idUsuario;


        // traer el idvENTA
        public int IdVentas;
        
        // Insertar Venta Final
        public double PrecioFinal;

       

        public DetalleVentas(int idProducto, string nombreProducto, string adereso, double precio,string sabor,int idUsuario,int idVenta )
        {
            InitializeComponent();
            Get();
            entIdProducto.Text = idProducto.ToString();
            entNombreProducto.Text = nombreProducto.ToString();
            entAdereso.Text = adereso.ToString();
            entPrecio.Text = precio.ToString(); 
            entSabor.Text = sabor.ToString();
            Usuario_idUsuario = idUsuario;
            IdVentas= idVenta;
            // id venta
            lblNVenta.Text = idVenta.ToString();
           
           
        }

        
        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            //calcular costos por producto
            double cantidad = double.Parse(lblCantidad.Text);

            //double precioUnitario = Convert.ToDouble(entPrecio.Text)
            double precioUnitario = double.Parse(entPrecio.Text);
            
            double precioTotal = cantidad * precioUnitario ;
            PrecioFinal = precioTotal+PrecioFinal;
            //Calcular precio por producto

            //Insertar en la base 



            // await Navigation.PushAsync(new ListaProducto(Usuario_idUsuario,IdVentas));
            //////////////////////////////
            WebClient detalle = new WebClient();
            try
            {
                var parameters = new System.Collections.Specialized.NameValueCollection();
                
                parameters.Add("idDetalleVentas", "");
                parameters.Add("Productos_idProductos", entIdProducto.Text);
                parameters.Add("Ventas_idVentas", IdVentas.ToString());
                parameters.Add("cantidad", lblCantidad.Text);
                parameters.Add("precio_venta", precioTotal.ToString().Replace(",","."));
                //parameters.Add("precio_venta", precioTotal.ToString().Replace(",","."));


                detalle.UploadValues(UrlDetalle, "POST", parameters);
                
                //await DisplayAlert("Agregado Correctamente ", precioTotal.ToString().Replace(",", "."), " Ok");
                await DisplayAlert("Agregado Correctamente ", PrecioFinal.ToString().Replace(",", "."), " Ok");
                
                // await Navigation.PushAsync(new Login());
                //await DisplayAlert("Sucess", "Registro Ingresado del Usuario: " + entNombres.Text + " " + entApellidos.Text, " Cerrar ");
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
        private async void btnCompra_Clicked(object sender, EventArgs e)
        {

            IdVentas = 0;
            await DisplayAlert("Felicidades ", "Compra Realizada", " Ok");
            await Navigation.PushAsync(new Principal());
            /// Insertar venta
            
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
        public async void Get()
        {
            try
            {
                var content = await client.GetStringAsync(UrlDetalle);
                List<DetalleVenta> post =
                    JsonConvert.DeserializeObject<List<DetalleVenta>>(content);
                _post = new ObservableCollection<DetalleVenta>(post);
                lstDetalles.ItemsSource = post;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            ////////////ver unitario
            //try
            //{
            //    var uri = new Uri(string.Format(UrlDetalleUnitario, IdVentas));
            //    var content = await client.GetStringAsync(uri);

            //    if (content != "false")
            //    {
            //        DetalleVenta post = JsonConvert.DeserializeObject<DetalleVenta>(content);
            //        lblCampo.Text = post.idDetalleVentas.ToString();
            //    //    List<DetalleVenta> post =
            //    //    JsonConvert.DeserializeObject<List<DetalleVenta>>(content);
            //    //_post = new ObservableCollection<DetalleVenta>(post);
            //    //lstDetalles.ItemsSource = post;
            //// await Navigation.PushAsync(new MainPage(post));
            //// await Navigation.PushAsync(new PageMenu(post));
            //    }
            //    else
            //    {
            //        await DisplayAlert("Alerta", "No seleccionado", "Cerrar");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}



        }
    }
}