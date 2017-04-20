using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LobbyingMadeSimple.Startup))]
namespace LobbyingMadeSimple
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
