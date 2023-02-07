using MasterDetail.MenuItems;
using ProyectoHeladeria.Models;
using ProyectoHeladeria.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoHeladeria
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMenu : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }
        public PageMenu(Usuario post)
        {
            InitializeComponent();
            lblUsuario.Text = post.correo;
            int idPerfil = post.Perfil_idPerfil;

            menuList = new List<MasterPageItem>( );
            var page1 = new MasterPageItem() { Title = "Home", Icon = "home.ico", TargetType = typeof(Principal) };
            var page2 = new MasterPageItem() { Title = "Listado Perfiles", Icon = "perfil.ico", TargetType = typeof(ListaPerfil) };
            var page3 = new MasterPageItem() { Title = "Listado Usuarios", Icon = "user.ico", TargetType = typeof(ListaUsuario) };
            var page4 = new MasterPageItem() { Title = "Listado Clientes", Icon = "users.ico", TargetType = typeof(ListaCliente) };
            var page5 = new MasterPageItem() { Title = "Listado Productos", Icon = "ice.ico", TargetType = typeof(ListaProducto) };
            var page6 = new MasterPageItem() { Title = "Listado Ventas", Icon = "sales.eco", TargetType = typeof(ListaVenta) };
            var page7 = new MasterPageItem() { Title = "Listado Detalle Ventas", Icon = "venta.eco", TargetType = typeof(DetalleVentas) };

            if (idPerfil == 1)
            {
                menuList.Add(page1);
                menuList.Add(page2);
                menuList.Add(page3);
                menuList.Add(page4);
                menuList.Add(page5);
                menuList.Add(page6);
                menuList.Add(page7);

            } else if (idPerfil == 2)
            {
                menuList.Add(page1);
                menuList.Add(page3);
                menuList.Add(page4);
                menuList.Add(page5);
                menuList.Add(page6);
                menuList.Add(page7);


            } else if (idPerfil == 3) {
                menuList.Add(page1);
                menuList.Add(page5);
                menuList.Add(page4);
                menuList.Add(page7);

            }


            navigationDrawerList.ItemsSource = menuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Principal)));

            this.BindingContext = new
            {
                Header = "",
                Image = "",
                footer = "Bienvenido"

            };
        }
        private void navigationDrawerList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());

        }


    }
}