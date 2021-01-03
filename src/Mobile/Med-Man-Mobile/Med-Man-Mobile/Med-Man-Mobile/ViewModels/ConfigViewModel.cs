using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Med_Man_Mobile;
using Med_Man_Mobile.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MedManMobile.ViewModels
{
    public class ConfigViewModel : BaseViewModel
    {
        public string IDP { get; set; }
        public string ApiBaseUri { get; set; }
        public string TenantName { get; set; }
        public string TenantId { get; set; }
        public string AppId { get; set; }
        public string SigninPolicy { get; set; }

        public string MedmanUrl { get; set; }
        public string ValidationMessage { get; set; }

        public ICommand SaveConfigCommand => new Command(async () => await SaveConfig());

        public bool IsValid { get; set; } = true;

        private string configString { get; set; }

        private async Task SaveConfig()
        {
            //parse url

            MedmanUrl = MedmanUrl.Replace("https://", "");
            if(MedmanUrl.EndsWith("/"))
            {
                MedmanUrl = MedmanUrl.Remove(MedmanUrl.Length - 1, 1);
            }

            Uri configUri;

            //validate url
            IsValid = Uri.TryCreate($"https://{MedmanUrl}/api/config", UriKind.Absolute, out configUri);

            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidationMessage");

            if (!IsValid)
            {
                ValidationMessage = "Not a valid URL. Please try again.";
                return;
            }

            //get config from api

            using (HttpClient client = new HttpClient())
            {
                var configResult = await client.GetAsync(configUri);

                if(!configResult.IsSuccessStatusCode)
                {
                    IsValid = false;
                    ValidationMessage = "Could not retrieve config";
                    OnPropertyChanged("IsValid");
                    OnPropertyChanged("ValidationMessage");
                    return;
                }

                configString = await configResult.Content.ReadAsStringAsync();
            }

            //validate config

            IsValid = ValidateConfig();
            //ValidationMessage = "";

            OnPropertyChanged("IsValid");

            if (!IsValid)
                return;

            await SecureStorage.SetAsync(nameof(App.Constants.ApiBaseUri), ApiBaseUri);
            await SecureStorage.SetAsync(nameof(App.Constants.IDP), IDP);

            if(IDP == "B2C")
            {
                await SecureStorage.SetAsync(nameof(App.Constants.TenantId), TenantId);
                await SecureStorage.SetAsync(nameof(App.Constants.TenantName), TenantName);
                await SecureStorage.SetAsync(nameof(App.Constants.AppId), AppId);
                await SecureStorage.SetAsync(nameof(App.Constants.SigninPolicy), SigninPolicy);
            }

            await App.Constants.InitialiseSecrets();
            MessagingCenter.Send<object>(this, "ConstantsSaved");
            
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private bool ValidateConfig()
        {
            if(string.IsNullOrWhiteSpace(IDP))
            {
                return false;
            }

            if(string.IsNullOrWhiteSpace(ApiBaseUri))
            {
                return false;
            }

            switch(IDP)
            {
                case "B2C":
                    if(string.IsNullOrWhiteSpace(TenantName))
                    {
                        return false;
                    }
                    if(string.IsNullOrWhiteSpace(TenantId))
                    {
                        return false;
                    }
                    if(string.IsNullOrWhiteSpace(AppId))
                    {
                        return false;
                    }
                    if(string.IsNullOrWhiteSpace(SigninPolicy))
                    {
                        return false;
                    }
                    return true;
                default:
                    return false;
            }
        }
    }
}