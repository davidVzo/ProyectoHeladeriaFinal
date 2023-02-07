using ProyectoHeladeria.Models;
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
    public partial class IngresarUsuario : ContentPage
    {
        private const string Url = "http://192.168.56.1/heladeria/postUsuario.php";
        public IngresarUsuario()
        {
            InitializeComponent();
        }

       
        private async void Cancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void insertarUsuario_Clicked(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            try
            {
                var parameters = new System.Collections.Specialized.NameValueCollection();
                parameters.Add("idUsuario", "");
                parameters.Add("cedula", entCedula.Text);
                parameters.Add("nombres", entNombres.Text);
                parameters.Add("apellidos", entApellidos.Text);
                parameters.Add("telefono", entTelefono.Text);
                parameters.Add("correo", entCorreo.Text);
                parameters.Add("usuario", entUsuario.Text);
                parameters.Add("contrasena", entContrasena.Text);
                parameters.Add("direccion", entDireccion.Text);
                parameters.Add("estado", "1");
                parameters.Add("imagen", null);
                parameters.Add("Perfil_idPerfil", "3");
                client.UploadValues(Url, "POST", parameters);

               // await Navigation.PushAsync(new Login());
                await DisplayAlert("Sucess", "Registro Ingresado del Usuario: " + entNombres.Text + " " + entApellidos.Text, " Cerrar ");
                // Limpiar();


            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta ", ex.Message, " Cancelar ");
                //mostrar errores en consola
                Console.WriteLine(ex.Message, "eroro");
            }
        }
        public void Limpiar()
        {
            //calidad de software para limpiar las variables 
            entNombres.Text = String.Empty;
            entApellidos.Text = String.Empty;
            entCedula.Text = String.Empty;
            entContrasena.Text = String.Empty;
            entUsuario.Text = String.Empty;
            entCorreo.Text = String.Empty;
            entTelefono.Text = String.Empty;
            entDireccion.Text = String.Empty;



        }
    }
}