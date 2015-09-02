using System.Web.Http;
using MB.Owin.Logging.Log4Net.Demo;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MB.Owin.Logging.Log4Net.Demo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseLog4Net();//from Web.config
            app.UseLog4Net("~/log4net.config");

            var logger = app.CreateLogger<Startup>();
            
            logger.WriteInformation("Application is started.");

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);            
        }
    }
}
