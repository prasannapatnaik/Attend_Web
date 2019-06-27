using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Attend_Web.Startup))]
namespace Attend_Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
