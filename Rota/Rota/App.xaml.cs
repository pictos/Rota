using Rota.Pages;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using Xamarin.Forms;

namespace Rota
{
    public partial class App : Application
	{
		public App ()
		{
			//InitializeComponent();

            GmsPlace.Init("YOUR-KEY");
            GmsDirection.Init("YOUR-KEY");

            // The root page of your application
            var mainPage = new NavigationPage(new SamplePage());

            if (Device.OS == TargetPlatform.iOS)
            {
                mainPage.BarBackgroundColor = Color.FromHex("#f1f1f1");
            }
            MainPage = mainPage;
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
            // Handle when your app sleeps
            TKNativePlacesApi.Instance.DisconnectAndRelease();
        }

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
