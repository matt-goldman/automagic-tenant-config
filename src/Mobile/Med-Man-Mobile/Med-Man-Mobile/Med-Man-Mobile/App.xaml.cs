using Med_Man_Mobile.Services;
using MedManMobile.Helpers;
using Xamarin.Forms;

namespace Med_Man_Mobile
{
    public partial class App : Application
    {
        public static Constants Constants { get; private set; }

        public App()
        {
            InitializeComponent();
            Constants = new Constants();
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
