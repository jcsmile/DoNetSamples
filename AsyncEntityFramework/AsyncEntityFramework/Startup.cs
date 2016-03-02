using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsyncEntityFramework.Startup))]
namespace AsyncEntityFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
