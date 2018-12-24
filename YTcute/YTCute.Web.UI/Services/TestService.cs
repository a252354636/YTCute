using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTCute.Resilience.Http;
using YTCute.Web.UI.Models;

namespace YTCute.Web.UI.Services
{
    public class TestService:ITestService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpClient _apiClient;
        private readonly string _testByPassUrl;
        private readonly string _purchaseUrl;
        private readonly IHttpContextAccessor _httpContextAccesor;

        private readonly string _bffUrl;

        public TestService(IOptionsSnapshot<AppSettings> settings,
            IHttpContextAccessor httpContextAccesor, IHttpClient httpClient)
        {
            _settings = settings;
            _testByPassUrl = $"{_settings.Value.PurchaseUrl}/api/values";
            _purchaseUrl = $"{_settings.Value.PurchaseUrl}/api/v1";
            _httpContextAccesor = httpContextAccesor;
            _apiClient = httpClient;
        }

         public async Task<string> GetTest(ApplicationUser user, string id)
        {
            var token = await GetUserTokenAsync();
            // var getOrderUri = API.Order.GetOrder(_testByPassUrl, id);
            string geturl = "http://localhost:6000/" + _testByPassUrl;
            var dataString = await _apiClient.GetStringAsync(geturl, token);

           // var response = JsonConvert.DeserializeObject<Order>(dataString);

            return dataString;
        }
        async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");
        }
    }
}
