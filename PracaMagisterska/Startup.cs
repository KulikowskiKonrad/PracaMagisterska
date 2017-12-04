using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracaMagisterska.Startup))]
namespace PracaMagisterska
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
