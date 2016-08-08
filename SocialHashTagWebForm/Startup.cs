using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialHashTagWebForm.Startup))]
namespace SocialHashTagWebForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
