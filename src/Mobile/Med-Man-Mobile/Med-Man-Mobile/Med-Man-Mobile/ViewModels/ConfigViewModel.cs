using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Med_Man_Mobile;
using Med_Man_Mobile.ViewModels;
using Newtonsoft.Json;
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

        public string UserEmail { get; set; }
        public string ValidationMessage { get; set; }

        public ICommand SaveConfigCommand => new Command(async () => await SaveConfig());

        public bool IsValid { get; set; } = true;

        private string configString { get; set; }

        private async Task SaveConfig()
        {
            //parse email

            if(!IsValidEmail(UserEmail))
            {
                IsValid = false;
                ValidationMessage = "Not a valid email address";
                OnPropertyChanged("IsValid");
                OnPropertyChanged("ValidationMessage");
                return;
            }

            var domain = GetDomainFromEmail(UserEmail);

            Uri configUri;

            //validate url
            IsValid = Uri.TryCreate($"https://discovermedman.{domain}/api/config", UriKind.Absolute, out configUri);

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

            try
            {
                var dto = JsonConvert.DeserializeObject<ConfigDto>(configString);

                AppId = dto.clientId;
                TenantId = dto.domain;
                TenantName = dto.tenantName;
                SigninPolicy = dto.signUpSignInPolicyId;
                ApiBaseUri = dto.apiBaseUri;
                IDP = dto.idp;
            }
            catch (Exception)
            {
                IsValid = false;
                ValidationMessage = "Not a valid MedMan URL";
                OnPropertyChanged("IsValid");
                OnPropertyChanged("ValidationMessage");
                return;
            }

            IsValid = ValidateConfig();

            if (!IsValid)
            {
                ValidationMessage = "Config from MedMan is invalid";
                OnPropertyChanged("IsValid");
                OnPropertyChanged("ValidationMessage");
                return;
            }
            
            // if got this far, config was retrieved successfully and is good

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

        private string GetDomainFromEmail(string email)
        {
            MailAddress address = new MailAddress(email);
            return address.Host;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private class ConfigDto
        {
            public string clientId { get; set; }
            public string domain { get; set; }
            public string tenantName { get; set; }
            public string instance { get; set; }
            public string signUpSignInPolicyId { get; set; }
            public string apiBaseUri { get; set; }
            public string idp { get; set; }
        }
    }
}