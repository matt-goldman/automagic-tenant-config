using MedMan.Mobile;
using MedMan.Mobile.Helpers;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace MedMan.Mobile
{
    public partial class App : Application
    {
        public static Constants Constants { get; private set; }
        public static IPublicClientApplication AuthenticationClient { get; private set; }
        public static bool IsLoggedIn { get; set; } = false;
        public static object UIParent { get; set; } = null;

        public App()
        {
            InitializeComponent();
            Constants = new Constants();
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
                                    .WithB2CAuthority(Constants.AuthoritySignin)
                                    .WithRedirectUri("msauth.com.ssw.medman://auth")
                                    .Build();
        }
    }
}
