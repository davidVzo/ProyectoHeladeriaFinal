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
	public partial class ListaUsuario : ContentPage
	{
        private const string Url = "http://192.168.56.1/heladeria/postUsuario.php";

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<Usuario> _post;
        public int idUsuario = -1, estado, Perfil_idPerfil;
        public string nombres, cedula, apellidos, telefono, direccion, correo, usuario, contrasena, imagen;

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            if (idUsuario > 0)
            {
                await Navigation.PushAsync(new EditarUsuario(idUsuario, cedula, nombres, apellidos, telefono, direccion, correo, usuario, contrasena, estado, imagen, Perfil_idPerfil));
            }
            else
            {
                await DisplayAlert("Alerta", "No se ha seleccionado un registro ", "OK");
            }
        }

        
        public ListaUsuario()
        {
            InitializeComponent();
            Get();
        }

        private void lstUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Usuario)e.SelectedItem;
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
                List<Usuario> post =
                    JsonConvert.DeserializeObject<List<Usuario>>(content);
                _post = new ObservableCollection<Usuario>(post);
                lstUsuarios.ItemsSource = post;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}