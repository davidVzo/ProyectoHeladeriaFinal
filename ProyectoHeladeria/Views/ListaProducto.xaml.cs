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
    public partial class ListaProducto : ContentPage
    {
        private const string Url = "http://192.168.56.1/heladeria/postProductos.php";
        private const string UrlVenta = "http://192.168.56.1/heladeria/postVentas.php";
        ///
        private const string UrlVenta2 = "http://192.168.56.1/heladeria/postVentas2.php?Usuario_idUsuario={0}&precioTotal={1}";

        private readonly HttpClient venta = new HttpClient();
        public ObservableCollection<Ventas> _inicioSesion;
        /////////
        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<Productos> _post;
        public int idProductos = -1;
        public string nombreProducto, adereso,sabor,imagen;
        public string numeroSerie ="002";
        public double precio;

        //Usuario
        public int IdUsuarioVerificar;
        public int IdVentas;

        
        private  async void btnAgregarVenta_Clicked(object sender, EventArgs e)
        {
            if (idProductos > 0)
            {
                if (IdVentas == 0) {
                    //await DisplayAlert("Alerta ", "Debe terminar la compra", " Ok ");
                    // INSERTAR VENTA /////////////
                    var DateAndTime = DateTime.Now;
                    var Date = DateAndTime.Date.ToString("dd-MM-yyyy");
                    WebClient venta = new WebClient();
                    try
                    {
                        var parameters = new System.Collections.Specialized.NameValueCollection();

                        parameters.Add("idVentas", "");
                        parameters.Add("numeroVenta", numeroSerie);
                        parameters.Add("fecha", Date);
                        parameters.Add("precioTotal", "");

                        parameters.Add("Usuario_idUsuario", IdUsuarioVerificar.ToString());
                        parameters.Add("Clientes_idUsuario", "1");

                        venta.UploadValues(UrlVenta, "POST", parameters);

                        // await Navigation.PushAsync(new Login());

                        // Limpiar();

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alerta ", ex.Message, " Cancelar ");
                        //mostrar errores en consola
                        Console.WriteLine(ex.Message, "error");
                    }

                    //////////////////////////

                }
                else {
                   
                }
                /////////////////////////////////////
                int IdUsuario = IdUsuarioVerificar;
                double PrecioTotal = 0;
                try
                {
                    var uri = new Uri(string.Format(UrlVenta2, IdUsuario, PrecioTotal));
                    var content = await client.GetStringAsync(uri);

                    if (content != "false")
                    {
                        Ventas post = JsonConvert.DeserializeObject<Ventas>(content);
                        await Navigation.PushAsync(new DetalleVentas(idProductos, nombreProducto, adereso, precio, sabor, IdUsuarioVerificar, post.idVentas));
                        //await Navigation.PushAsync(new MainPage(post));
                        // await Navigation.PushAsync(new PageMenu(post));
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Ingrese Venta.", "Cerrar");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                /////////////////////////////
               
            }
            else
            {
                await DisplayAlert("Alerta", "No se ha seleccionado ", "OK");
            }
        }

        private void lstProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemSelected = e.CurrentSelection[0] as Productos;
            idProductos = itemSelected.idProductos;
            nombreProducto= itemSelected.nombreProducto;
            adereso= itemSelected.adereso;
            sabor= itemSelected.sabor;
            precio = itemSelected.costo;
            
            //imagen= itemSelected.imagen;

        }

        
        public ListaProducto(int idUsuario,int idVenta)
        {
            InitializeComponent();
            Get();
            IdUsuarioVerificar = idUsuario;
            lblIdUsuario.Text = idUsuario.ToString();
            IdVentas= idVenta;
            lblIdVenta.Text = IdVentas.ToString();
        }

     

        public async void Get()
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<Productos> post =
                    JsonConvert.DeserializeObject<List<Productos>>(content);
                _post = new ObservableCollection<Productos>(post);
                lstProductos.ItemsSource = post;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}