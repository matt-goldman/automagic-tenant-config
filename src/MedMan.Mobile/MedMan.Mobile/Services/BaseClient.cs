using MedMan.Mobile;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SSW.MedMan
{
    public partial class BaseClient
    {
        protected async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.Constants.BearerToken);
            httpRequestMessage.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true, NoStore = true };
            return await Task.FromResult(httpRequestMessage);
        }
    }
}
