using MedManMobile.Views;
using Xamarin.Forms;

namespace Med_Man_Mobile.ViewModels
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
                .WithLoginHint(App.Constants.UserEmail)
                .ExecuteAsync();

            App.Constants.BearerToken = result.AccessToken;

            await Shell.Current.GoToAsync($"//{nameof(PatientsPage)}");
        }
    }
}
