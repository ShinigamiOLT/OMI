using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OMI.Startup))]
namespace OMI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
