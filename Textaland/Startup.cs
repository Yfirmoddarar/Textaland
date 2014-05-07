using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Textaland.Startup))]
namespace Textaland
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
