using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kollaborator.web.Startup))]
namespace Kollaborator.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
