using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SSW.Med_Man.MVC.DTOs;

namespace SSW.Med_Man.MVC.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configutation;

        public ConfigController(IConfiguration configutation)
        {
            _configutation = configutation;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<ConfigDTO> Get()
        {
            var idp = _configutation.GetValue<string>("IDP");

            switch(idp)
            {
                case "B2C":
                    // in a real world application, you would
                    // define handlers for each IDP you want 
                    // to support and call the appropriate
                    // handler rather than building this here

                    B2CConfigDTO config = new B2CConfigDTO();
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
