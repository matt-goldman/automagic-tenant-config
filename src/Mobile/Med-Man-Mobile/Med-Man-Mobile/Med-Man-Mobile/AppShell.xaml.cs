using MedManMobile.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Med_Man_Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            _ = Init();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            bool logout = await DisplayAlert("Logout", "Are you sure you want to log out?", "Yes", "No");
            if(logout)
            {
                SecureStorage.RemoveAll();
                await App.Constants.InitialiseSecrets();
                await Shell.Current.GoToAsync("//LoginPage");
                await Navigation.PushModalAsync(new ConfigPage());
            }
        }

        private async Task Init()
        {
            await App.Constants.InitialiseSecrets();
            if(!App.Constants.SecretsInitialised)
            {
                await Shell.Current.GoToAsync("//LoginPage");
                await Navigation.PushModalAsync(new ConfigPage());
            }
        }

        public static string PatientsIcon = "\uf000";
    }
}
