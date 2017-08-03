using Plugin.RestClient;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace simplemapgeo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Receiver : ContentPage
    {
        string id = null;
        Map Cmap = null;
        
        public Receiver()
        {
            InitializeComponent();
            Cmap = new Map(
            MapSpan.FromCenterAndRadius(
                    new Position(36.718944, 10.204665), Distance.FromMiles(0.3)
                    )
                );
            stackLayout.Children.Add(Cmap);
        }
        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            id = EntryID.Text;
            await DoMethodeAsync();
        }
        private async Task DoMethodeAsync()
        {
            Geolocation x = null;
            Pin p = new Pin()
            {
                Type = PinType.Generic,
                Label = "some text here",
            };
            RestClient<Geolocation> restClient = new RestClient<Geolocation>();
            try
            {
                x = await restClient.GetAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            stackLayout.Children.Remove(Cmap);


            Cmap = new Map(
                MapSpan.FromCenterAndRadius(
                    new Position(x.Latitude, x.Longitude), Distance.FromMiles(0.3)
                    )
                );
            stackLayout.Children.Add(Cmap);

            while (true)
            {
                try
                {
                    x = await restClient.GetAsync(id);
                }
                catch (Exception)
                {
                    throw;
                }

                latitudeLabel.Text = " latitude= " + x.Latitude;
                longitudeLabel.Text = " longitude= " + x.Longitude;

                p.Position = new Position(x.Latitude, x.Longitude);

                Cmap.Pins.Clear();

                Cmap.Pins.Add(p);
                Cmap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(x.Latitude, x.Longitude), Cmap.VisibleRegion.Radius
                    ));

                await Task.Delay(4000);
            };

        }

        
    }
}