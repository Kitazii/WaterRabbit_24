using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WaterRabbit_24.Startup))]
namespace WaterRabbit_24
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
