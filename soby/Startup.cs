using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(soby.Startup))]
namespace soby
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
