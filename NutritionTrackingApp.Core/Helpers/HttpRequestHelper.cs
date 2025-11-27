using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Core.Helpers
{
    public static class HttpRequestHelper
    {
        public static HttpRequestMessage CreateGetRequestWithApiKey(
            string baseUrl,
            Dictionary<string, string?> queryParams,
            string apiKey)
        {
            var url = QueryHelpers.AddQueryString(baseUrl, queryParams);

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("x-api-key", apiKey);

            return request;
        }
    }
}
