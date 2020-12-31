using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MedManMobile.Helpers
{
    public class Constants
    {
        private string authorityBase { get { return $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/"; } }
        private bool secretsInitialised { get; set; }


        public string BearerToken { get; set; }
        public string ApiBaseUri { get; set; }
        public string[] Scopes => new string[] { $"https://{TenantId}/api/user_impersonation" };
        public string AppId { get; set; }
        public string IosKeychainSecurityGroups => "com.ssw.medman";
        public string TenantId { get; set; }
        public string TenantName { get; set; }
        public string SigninPolicy { get; set; }
        public string IDP { get; set; }
        public string AuthoritySignin
        {
            get
            {
                return $"{authorityBase}{SigninPolicy}";
            }
        }

        public bool SecretsInitialised { get { return secretsInitialised; } }

        public Constants()
        {
            _ = InitialiseSecrets();
        }

        public async Task<bool> InitialiseSecrets()
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

            TenantId = await SecureStorage.GetAsync(nameof(TenantId));
            if(string.IsNullOrWhiteSpace(TenantId))
            {
                secretsInitialised = false;
            }

            TenantName = await SecureStorage.GetAsync(nameof(TenantName));
            if(string.IsNullOrWhiteSpace(TenantName))
            {
                secretsInitialised = false;
            }

            IDP = await SecureStorage.GetAsync(nameof(IDP));
            if(string.IsNullOrWhiteSpace(IDP))
            {
                secretsInitialised = false;
            }

            if(IDP == "B2C")
            {
                SigninPolicy = await SecureStorage.GetAsync(nameof(SigninPolicy));
                if (string.IsNullOrWhiteSpace(SigninPolicy))
                {
                    secretsInitialised = false;
                }
            }

            return secretsInitialised;
        }
    }
}
