using ProyectoHeladeria.Models;
using ProyectoHeladeria.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoHeladeria
{
    public partial class MainPage : MasterDetailPage
    {
        
        public MainPage( string correo,int idPerfil,int idUsuario,int numeroLista)
        {
            
            InitializeComponent();
          
            
            NavigationPage.SetHasNavigationBar(this, false);

            this.Master = new Master(correo, idPerfil, idUsuario);
            if (numeroLista == 1) {

                this.Detail = new NavigationPage(new Principal());
                App.MasterDetail = this;
            } else if (numeroLista == 2) {

                this.Detail = new NavigationPage(new ListaPerfil());
                App.MasterDetail = this;
            } else if (numeroLista == 3) {
                this.Detail = new NavigationPage(new ListaUsuario());
                App.MasterDetail = this;
            } else if ( numeroLista == 4) {
                this.Detail = new NavigationPage(new ListaCliente());
                App.MasterDetail = this;
            }
            else if (numeroLista == 5)
            {
                this.Detail = new NavigationPage(new ListaProducto(idUsuario));
                App.MasterDetail = this;
            }
            else if (numeroLista == 6)
            {
                this.Detail = new NavigationPage(new DetalleVentas(idUsuario));
                App.MasterDetail = this;
            }
            else if (numeroLista == 7)
            {
                this.Detail = new NavigationPage(new ListaVenta(idUsuario));
                App.MasterDetail = this;
            }



        }
    }
}
