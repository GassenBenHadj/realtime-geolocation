using Plugin.Geolocator;
using Plugin.RestClient;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace simplemapgeo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sender : ContentPage
    {
        public Sender()
        {
            InitializeComponent();
            
            DoMethode();
        }
        private async void DoMethode()
        {
            var locator = CrossGeolocator.Current;
            await locator.StartListeningAsync(5000, 0);
            locator.DesiredAccuracy = 0.1;
            locator.AllowsBackgroundUpdates = true;
            var p = await locator.GetPositionAsync(10000);
            Geolocation g = new Geolocation()
            {
                Id = DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond,
                Latitude = p.Latitude,
                Longitude = p.Longitude

            };
            LabelId.Text =  g.Id;
            RestClient<Geolocation> restClient = new RestClient<Geolocation>();
            var x = await restClient.PostAsync(g);
            
            locator.PositionChanged += async (sender, e) =>
            {
                latitudeLabel.Text = " Latitude= " + e.Position.Latitude.ToString();
                longitudeLabel.Text = " Longitude= " + e.Position.Longitude.ToString();
                g.Latitude = e.Position.Latitude;
                g.Longitude = e.Position.Longitude;
                try
                {
                    x = await restClient.PutAsync(g.Id, g);
                }
                catch (Exception)
                {

                    throw;
                }
                GC.Collect();
                await Task.Delay(2000);
            };
        }
        
    }
}