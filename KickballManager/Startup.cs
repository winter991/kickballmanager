using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KickballManager.Startup))]
namespace KickballManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
