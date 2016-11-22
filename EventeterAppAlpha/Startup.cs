using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventeterAppAlpha.Startup))]
namespace EventeterAppAlpha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
