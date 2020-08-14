using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OTP.Startup))]
namespace OTP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
