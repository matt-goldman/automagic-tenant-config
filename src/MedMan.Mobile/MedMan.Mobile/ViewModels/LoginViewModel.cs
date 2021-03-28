using MedMan.Mobile.Views;
using Xamarin.Forms;

namespace MedMan.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

            if(App.AuthenticationClient == null)
            {
                App.InitialiseAuthClient();
            }

            var result = await App.AuthenticationClient
                .AcquireTokenInteractive(App.Constants.Scopes)
                .WithParentActivityOrWindow(App.UIParent)
                .WithUseEmbeddedWebView(true)
                .ExecuteAsync();

            App.Constants.BearerToken = result.AccessToken;

            await Shell.Current.GoToAsync($"//{nameof(PatientsPage)}");
        }
    }
}
