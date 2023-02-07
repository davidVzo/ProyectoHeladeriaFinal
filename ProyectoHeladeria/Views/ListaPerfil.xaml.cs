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

namespace ProyectoHeladeria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaPerfil : ContentPage
	{
        private const string Url = "http://192.168.56.1/heladeria/postPerfil.php";

        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<Perfil> _post;
        public int idPerfil = -1;
        public string nombre, descripcion;
        public ListaPerfil ()
		{
			InitializeComponent ();
            Get();
        }

        private void lstPerfiles_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Perfil)e.SelectedItem;
            idPerfil = obj.idPerfil;
            nombre = obj.nombre;
            descripcion = obj.descripcion;
        }

        public async void Get()
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<Perfil> post =
                    JsonConvert.DeserializeObject<List<Perfil>>(content);
                _post = new ObservableCollection<Perfil>(post);
                lstPerfiles.ItemsSource = post;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}