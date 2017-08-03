
using Xamarin.Forms;

namespace simplemapgeo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Hello => just print Welcome to Xamarin Forms!
            //MainPage => Simple Map for testing 
            //Sender => Send Geolocations to Backend
            //Receiver => Listen to bachend and retrive the geolocation in real time :) 
            MainPage = new TabbedPage
            {
                Children={
                    new Sender(),
                    new Receiver()
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
