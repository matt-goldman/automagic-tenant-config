using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedManMobile.Helpers
{
    public class Constants
    {
        public string BearerToken { get; set; }
        public string ApiBaseUri { get; set; }
        public string[] Scopes => new string[] { $"https://{tenantName}.onmicrosoft.com/api/user_impersonation" };
    }
}
