using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS.Startup))]
namespace LMS
{
    public partial class Startup
    {
        //comment test check name
        //coykie nawala si aaaaaa
        //naa nko
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
