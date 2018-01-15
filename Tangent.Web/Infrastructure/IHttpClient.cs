using System.Threading.Tasks;

namespace Tangent.Web.Infrastructure
{
    public interface IHttpClient
    {
        Task<HttpResult> ReadAsStringAsync(string Url);

        Task<HttpResult> PostAsync(string Url, string Content);
    }
}
