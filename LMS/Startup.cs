using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS.Startup))]
namespace LMS
{
    public partial class Startup
    {
        //comment test check name
        //coykie nawala si aaaaaa
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
