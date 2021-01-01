using Med_Man_Mobile;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedManMobile.Services
{
    public class BaseService
    {
        public static HttpClient httpClient { get; set; }
        public static string apiUri;

        public BaseService()
        {
            httpClient = new HttpClient();
            apiUri = App.Constants.ApiBaseUri;
            apiUri = apiUri.Replace("https://", "");
            if (apiUri.EndsWith("/"))
            {
                int strLength = apiUri.Length;
                apiUri = apiUri.Remove(strLength - 1, 1);
            }

            apiUri = $"https://{apiUri}/";

            MessagingCenter.Subscribe<object>(this, "ConstantsSaved", (obj) => apiUri = App.Constants.ApiBaseUri);
        }
        public static async Task<bool> HandleUnauthorizedAsync()
        {
            try
            {
                int refreshAttempts = 0;

                while (refreshAttempts < 3)
                {
                    if (App.AuthenticationClient == null)
                    {
                        App.InitialiseAuthClient();
                    }

                    IEnumerable<IAccount> accounts = await App.AuthenticationClient.GetAccountsAsync();

                    AuthenticationResult result = await App.AuthenticationClient
                        .AcquireTokenSilent(App.Constants.Scopes, accounts.FirstOrDefault())
                        .ExecuteAsync();

                    if (result.ExpiresOn.ToUniversalTime() > DateTime.UtcNow)
                    {
                        App.Constants.BearerToken = result.AccessToken;
                        return true;
                    }

                    refreshAttempts++;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unexpected error occurred while attempting to re-authenticate");
                Debug.Write(ex.Message);
                return false;
            }
        }
    }
}
