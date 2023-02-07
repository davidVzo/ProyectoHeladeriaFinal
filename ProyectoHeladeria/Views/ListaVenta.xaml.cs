using Newtonsoft.Json;
using ProyectoHeladeria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoHeladeria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaVenta : ContentPage
    {
        private const string Url = "http://192.168.56.1/heladeria/postVentas.php";

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<Ventas> _post;

        public int idVentas = -1, Usuario_idUsuario,Clientes_idClientes;
        public string numeroVenta, fecha;
        public double precioTotal;
        private void lstVentas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Ventas)e.SelectedItem;

            idVentas = obj.idVentas;
            Usuario_idUsuario = obj.Usuario_idUsuario;
            Clientes_idClientes = obj.Clientes_idUsuario;
            numeroVenta = obj.numeroVenta;
            fecha = obj.fecha;
            precioTotal = obj.precioTotal;
            

        }

        
        public ListaVenta()
        {
            InitializeComponent();
            Get();
        }

        public async void Get()
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<Ventas> post =
                    JsonConvert.DeserializeObject<List<Ventas>>(content);
                _post = new ObservableCollection<Ventas>(post);
                lstVentas.ItemsSource = post;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}