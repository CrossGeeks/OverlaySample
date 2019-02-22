using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OverlaySample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(new MainPage() { Title = "Main"});
            tabbedPage.Children.Add(new MapPage() { Title = "Map" });
            tabbedPage.Children.Add(new CameraPage() { Title = "Camera" });
            MainPage = tabbedPage;
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
