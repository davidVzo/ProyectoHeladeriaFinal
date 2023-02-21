using ProyectoHeladeria.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoHeladeria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        int IdUsuario;
        int IdPerfil;
        public Master(string correo,int perfil,int idUsuario)
        {
            InitializeComponent();
             lblUsuario.Text = correo;
            IdUsuario = idUsuario;
            IdPerfil = perfil;
            if ( perfil == 1 ) {
                listadoPerfiles.IsVisible = true;
                listadoUsuarios.IsVisible = true;
                listadoClientes.IsVisible = true;
                listadoProductos.IsVisible = true;
                listadoVentas.IsVisible = true;
                Carrito.IsVisible = true;
                //listadoDetalles.IsVisible = true;
            } else if ( perfil == 2 ) {
                listadoPerfiles.IsVisible = false;
                listadoUsuarios.IsVisible = false;
                listadoClientes.IsVisible = false;
                listadoProductos.IsVisible = true;
                listadoVentas.IsVisible = true;
                Carrito.IsVisible = true;
                //listadoDetalles.IsVisible = true;

            } else if ( perfil == 3 ) {
                listadoPerfiles.IsVisible = false;
                listadoUsuarios.IsVisible = false;
                listadoClientes.IsVisible = false;
                listadoProductos.IsVisible = true;
                listadoVentas.IsVisible = false;
                Carrito.IsVisible = true;
                //listadoDetalles.IsVisible = true;

            }

        }
        private async void listadoHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(lblUsuario.Text, IdPerfil, IdUsuario, 1));
         
        }

        private async void listadoPerfiles_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(lblUsuario.Text,IdPerfil,IdUsuario,2));
         
        }

       

        private async void listadoUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(lblUsuario.Text, IdPerfil, IdUsuario, 3));
        }

        private async void listadoClientes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(lblUsuario.Text, IdPerfil, IdUsuario, 4));
        }

        private async void listadoProductos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(lblUsuario.Text, IdPerfil, IdUsuario, 5));

        }

        private async void Carrito_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(lblUsuario.Text, IdPerfil, IdUsuario, 6));
        }

        private async void listadoVentas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage(lblUsuario.Text, IdPerfil, IdUsuario, 7));
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void prueba_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagePrueba());
        }
    }
}