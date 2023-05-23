using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsPortal.PL.Startup))]
namespace NewsPortal.PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
