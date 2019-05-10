using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Iris10ReportUI.Startup))]
namespace Iris10ReportUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
