using Med_Man_Mobile.ViewModels;
using MedManMobile.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async Task Init()
        {
            await App.Constants.InitialiseSecrets();
            if(!App.Constants.SecretsInitialised)
            {
                await Navigation.PushModalAsync(new ConfigPage());
            }
        }

        public static string PatientsIcon = "\uf000";
    }
}
