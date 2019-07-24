using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(Iris10ReportUI.Startup))]
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
