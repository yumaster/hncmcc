using Microsoft.Owin;
using Owin;

namespace HnydWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}