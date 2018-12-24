using System;
using YTCute.Resilience.Http;

namespace YTCute.Web.UI.Infrastructure
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}