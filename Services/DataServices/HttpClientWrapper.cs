using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;


namespace Services.DataServices
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, object content)
        {
            return await _httpClient.PostAsJsonAsync(requestUri, content);
        }
    }
}
