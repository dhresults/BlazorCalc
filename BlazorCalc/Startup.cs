using Microsoft.Extensions.DependencyInjection;
using WebWindows.Blazor;

namespace BlazorCalc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(DesktopApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            
        }
    }
}
