using Microsoft.Extensions.DependencyInjection;

namespace TestableHttp
{
    public class DependencyInjectionConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpClient, HttpClient>();
        }
    }
}