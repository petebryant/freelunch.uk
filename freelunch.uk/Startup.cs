using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(freelunch.uk.Startup))]
namespace freelunch.uk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
