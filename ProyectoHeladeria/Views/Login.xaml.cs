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
using System.Net;

namespace ProyectoHeladeria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        //private const string Url = " "
        // 192.168.1.12
        private const string Url = "http://192.168.56.1/heladeria/postUsuarioLogin.php?usuario={0}&contrasena={1}";

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<Usuario> _inicioSesion;
        public Login()
        {
            InitializeComponent();
          
        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
          // await Navigation.PushAsync(new MainPage(entUsuario.Text));
            if (!String.IsNullOrEmpty(entUsuario.Text))
            {
                if (!String.IsNullOrEmpty(entContrasena.Text))
                {
                    string tUsuario = entUsuario.Text;
                    string tPass = entContrasena.Text;
                    try
                    {
                        var uri = new Uri(string.Format(Url, tUsuario, tPass));
                        var content = await client.GetStringAsync(uri);

                        if (content != "false")
                        {
                            Usuario post = JsonConvert.DeserializeObject<Usuario>(content);
                            await Navigation.PushAsync(new MainPage(post));
                           // await Navigation.PushAsync(new PageMenu(post));
                        }
                        else
                        {
                            await DisplayAlert("Alerta", "Usuario o Password Incorrecto.", "Cerrar");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    await DisplayAlert("Mensaje de Error", "Debe ingresar el password.", "Cerrar");
                }
            }
            else
            {
                await DisplayAlert("Mensaje de Error", "Debe ingresar el Usuario", "Cerrar");
            }
        }

        private async void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IngresarUsuario());
        }
    }
}