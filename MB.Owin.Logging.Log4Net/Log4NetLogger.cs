using System;
using System.Diagnostics;
using System.Reflection;
using log4net.Core;
using log4net.Repository.Hierarchy;
using ILogger = Microsoft.Owin.Logging.ILogger;

namespace MB.Owin.Logging.Log4Net
{
    class Log4NetLogger : ILogger
    {
        private readonly Func<TraceEventType, Level> _getLogLevel;
        private readonly Logger _logger;
        private Assembly _repositoryAssembly;

        public Log4NetLogger(Assembly repositoryAssembly, string name, Func<TraceEventType, Level> getLogLevel)
        {
            _getLogLevel = getLogLevel;
            _repositoryAssembly = repositoryAssembly;
            _logger = (Logger) LoggerManager.GetLogger(repositoryAssembly, name);
        }

        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            var level = this._getLogLevel(eventType);

            // According to docs http://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin/Logging/ILogger.cs
            // "To check IsEnabled call WriteCore with only TraceEventType and check the return value, no event will be written."
            if (state == null)
            {
                return _logger.IsEnabledFor(level);
            }
            if (!_logger.IsEnabledFor(level))
            {
                return false;
            }

            _logger.Log(level, formatter(state, exception), exception);
            return true;
        }
    }
}
