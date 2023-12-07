using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, object content);
    }
}
