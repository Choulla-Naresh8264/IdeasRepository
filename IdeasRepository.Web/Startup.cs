using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IdeasRepository.Web.Startup))]
namespace IdeasRepository.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
