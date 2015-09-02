using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using log4net.Config;
using log4net.Core;
using Microsoft.Owin.Logging;
using Owin;

namespace MB.Owin.Logging.Log4Net
{
    public static class Extensions
    {
        public static void UseLog4Net(this IAppBuilder app, Assembly repositoryAssembly)
        {
            ConfigureLogger("~/Web.config");
            app.SetLoggerFactory(new Log4NetLogFactory(repositoryAssembly));
        }

        public static void UseLog4Net(this IAppBuilder app, Func<TraceEventType, Level> getLogLevel)
        {
            ConfigureLogger("~/Web.config");
            app.SetLoggerFactory(new Log4NetLogFactory(getLogLevel, Assembly.GetExecutingAssembly()));
        }

        public static void UseLog4Net(this IAppBuilder app, Func<TraceEventType, Level> getLogLevel, Assembly repositoryAssembly)
        {
            ConfigureLogger("~/Web.config");
            app.SetLoggerFactory(new Log4NetLogFactory(getLogLevel, repositoryAssembly));
        }

        //log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));

        public static void UseLog4Net(this IAppBuilder app, string configFile)
        {            
            ConfigureLogger(configFile);
            app.SetLoggerFactory(new Log4NetLogFactory(Assembly.GetExecutingAssembly()));
        }

        public static void UseLog4Net(this IAppBuilder app)
        {
            ConfigureLogger("~/Web.config");            
            app.SetLoggerFactory(new Log4NetLogFactory(Assembly.GetExecutingAssembly()));
        }

        private static void ConfigureLogger(string configFile)
        {
            var filePath = MapPath(configFile);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            XmlConfigurator.Configure(new FileInfo(filePath));
        }

        private static string MapPath(string virtualPath)
        {
            var path = HostingEnvironment.MapPath(virtualPath);
            if (path == null)
            {
                var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                if (uriPath==null)
                {
                    throw new ArgumentNullException();
                }
                return new Uri(uriPath).LocalPath + (virtualPath.TrimStart('~'));
            }
            return path;
        }
    }
}
