using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuessWhere.Startup))]
namespace GuessWhere
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
