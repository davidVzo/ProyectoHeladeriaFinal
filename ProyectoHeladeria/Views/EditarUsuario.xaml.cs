using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoHeladeria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarUsuario : ContentPage
	{
        private const string Url = "http://192.168.56.1/heladeria/postUsuario.php?idUsuario={0}&cedula={1}&nombres={2}&apellidos={3}&telefono={4}&direccion={5}&correo={6}&usuario={7}&contrasena={8}&estado={9}&imagen={10}&Perfil_idPerfil={11}";
		public EditarUsuario(int idUsuario, string cedula, string nombres, string apellidos, string telefono, string direccion, string correo, string usuario, string contraseña, int estado, string imagen, int Perfil_idPerfil)
		{
			InitializeComponent ();
			entIdUsuario.Text = idUsuario.ToString ();
            entCedulaUsuario.Text = cedula.ToString();
            entNombresUsuario.Text = nombres.ToString();
            entApellidosUsuario.Text = apellidos.ToString();
            entTelefonoUsuario.Text = telefono.ToString();
            entCorreoUsuario.Text = correo.ToString();
            entUsuario.Text = usuario.ToString();
            entContraseñaUsuario.Text = contraseña.ToString();
            entEstadoUsuario.Text = estado.ToString();
            entImagenUsuario.Text = imagen.ToString();
            entPerfilUsuario.Text = Perfil_idPerfil.ToString();
            entDireccionUsuario.Text = direccion.ToString();

        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            WebClient cliente = new WebClient();
            try
            {
                using (var webClient = new WebClient())
                {
                    var uri = new Uri(string.Format(Url,
                        entIdUsuario.Text, entCedulaUsuario.Text, entNombresUsuario.Text, entApellidosUsuario.Text, entTelefonoUsuario.Text,entDireccionUsuario.Text, entCorreoUsuario.Text, entUsuario.Text, entContraseñaUsuario.Text, entEstadoUsuario.Text, entImagenUsuario.Text, entPerfilUsuario.Text));

                    webClient.UploadString(uri, "PUT", string.Empty);

                   await DisplayAlert("Succes", "Registro de " + entNombresUsuario.Text + " " + entApellidosUsuario.Text + " Actualizado correctamente ", " cerrar");
                    await Navigation.PushAsync(new ListaUsuario());
                }
            }
            catch (Exception ex)
            {
               await DisplayAlert("Alerta ", ex.Message, " Cerrar");

            }
        }
    }
}