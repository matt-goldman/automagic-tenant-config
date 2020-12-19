using MedManMobile.Helpers;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace Med_Man_Mobile
{
    public partial class App : Application
    {
        public static Constants Constants { get; private set; }
        public static IPublicClientApplication AuthenticationClient { get; private set; }

        public static object UIParent { get; set; } = null;

        public App()
        {
            InitializeComponent();
            Constants = new Constants();
            //DependencyService.Register<MockDataStore>();
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

        public static void InitialiseAuthClient()
        {
            AuthenticationClient = PublicClientApplicationBuilder.Create(Constants.AppId)
                                    .WithIosKeychainSecurityGroup(Constants.IosKeychainSecurityGroups)
                                    .WithTenantId(Constants.TenantId)
                                    .WithRedirectUri("msauth.com.verisaas.verinote://auth")
                                    .Build();
        }
    }
}
