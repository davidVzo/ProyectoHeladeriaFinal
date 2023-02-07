using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoHeladeria.Models;

namespace ProyectoHeladeria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaCliente : ContentPage
	{
        private const string Url = "http://192.168.56.1/heladeria/postClientes.php";

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<Cliente> _post;
        public int idUsuario = -1, estado, Perfil_idPerfil;
        public string nombres, cedula, apellidos, telefono, direccion, correo, usuario, contrasena, imagen;
        public ListaCliente ()
        {
            InitializeComponent();
            Get();
        }

        private void lstClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Cliente)e.SelectedItem;
            idUsuario = obj.idUsuario;
            nombres = obj.nombres;
            apellidos = obj.apellidos;
            cedula = obj.cedula;
            telefono = obj.telefono;
            direccion = obj.direccion;
            usuario = obj.usuario;
            correo = obj.correo;
            estado = obj.estado;
            imagen = obj.imagen;
            contrasena = obj.contrasena;

            Perfil_idPerfil = obj.Perfil_idPerfil;
        }

        public async void Get()
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<Cliente> post =
                    JsonConvert.DeserializeObject<List<Cliente>>(content);
                _post = new ObservableCollection<Cliente>(post);
                lstClientes.ItemsSource = post;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}