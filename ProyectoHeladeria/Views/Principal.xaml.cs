using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoHeladeria.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using static System.Net.WebRequestMethods;

namespace ProyectoHeladeria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {
        public Principal()
        {
            InitializeComponent();
            List<Images> images = new List<Images>()
            {
                new Images(){Title="Image 1",Url="https://img.freepik.com/vector-gratis/plantilla-menu-pizarra-helado-dibujado-mano_52683-63807.jpg?w=826&t=st=1675611848~exp=1675612448~hmac=51793d2b51872a0cd47cfb923c780654445e8dca5f00653238f4e2510562249a"},
                new Images(){Title="Image 2",Url="https://cdn.pixabay.com/photo/2015/10/20/22/05/italy-998551_960_720.jpg"},
                
                new Images(){Title="Image 3",Url="https://img.freepik.com/vector-gratis/plantilla-portada-youtube-comida_23-2148672047.jpg?w=740&t=st=1675611966~exp=1675612566~hmac=d360570146f386dcd3e9cf8e082b1f0f64b2573dcbe2a21887584446a62a6e18"}
            };

            Carousel.ItemsSource = images;

            Device.StartTimer(TimeSpan.FromSeconds(5), (Func<bool>)(() =>
            {
                Carousel.Position = (Carousel.Position + 1) % images.Count;
                return true;
            }));


        }
       
        
    }
}