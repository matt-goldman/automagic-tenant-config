using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MedMan.API.DTOs;

namespace MedMan.API.Controllers
{
    public class ConfigController : ApiControllerBase
    {
        private readonly IConfiguration _configutation;

        public ConfigController(IConfiguration configutation)
        {
            _configutation = configutation;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ConfigDto> Get()
        {
            var idp = _configutation.GetValue<string>("IDP");

            switch(idp)
            {
                case "B2C":
                    // in a real world application, you would
                    // define handlers for each IDP you want 
                    // to support and call the appropriate
                    // handler rather than building this here

                    B2CConfigDto config = new B2CConfigDto();
                    _configutation.Bind("AzureAdB2C", config);
                    config.ApiBaseUri = _configutation.GetValue<string>("ApiBaseUri");
                    config.IDP = idp;
                    return config;
                default:
                    // you can add handlers for other identity
                    // providers too, eg AAD, Auth0 or Okta
                    return null;
            }
        }
    }
}
