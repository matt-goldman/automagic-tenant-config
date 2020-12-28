using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MedManMobile.Helpers
{
    public class Constants
    {
        public string BearerToken { get; set; }
        public string ApiBaseUri { get; set; }
        public string[] Scopes => new string[] { $"https://{tenantName}.onmicrosoft.com/api/user_impersonation" };
        public string AppId { get; set; }
        public string IosKeychainSecurityGroups { get; set; }
        public string TenantId { get; set; }

        private string tenantName;

        private bool secretsInitialised { get; set; }

        public bool SecretsInitialised { get { return secretsInitialised; } }

        public Constants()
        {
            _ = InitialiseSecrets();
        }

        public async Task InitialiseSecrets()
        {
            secretsInitialised = true;

            ApiBaseUri = await SecureStorage.GetAsync(nameof(ApiBaseUri));
            if(string.IsNullOrWhiteSpace(ApiBaseUri))
            {
                secretsInitialised = false;
            }

            AppId = await SecureStorage.GetAsync(nameof(AppId));
            if(string.IsNullOrWhiteSpace(AppId))
            {
                secretsInitialised = false;
            }

            IosKeychainSecurityGroups = await SecureStorage.GetAsync(nameof(IosKeychainSecurityGroups));
            if(string.IsNullOrWhiteSpace(IosKeychainSecurityGroups))
            {
                secretsInitialised = false;
            }

            TenantId = await SecureStorage.GetAsync(nameof(TenantId));
            if(string.IsNullOrWhiteSpace(TenantId))
            {
                secretsInitialised = false;
            }
        }
    }
}
