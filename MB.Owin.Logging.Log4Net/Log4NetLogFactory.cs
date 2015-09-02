using System;
using System.Diagnostics;
using System.Reflection;
using log4net.Core;
using Microsoft.Owin.Logging;
using ILogger = Microsoft.Owin.Logging.ILogger;

namespace MB.Owin.Logging.Log4Net
{
    public class Log4NetLogFactory : ILoggerFactory
    {
        private readonly Func<TraceEventType, Level> _getLogLevel;
        private readonly Assembly _repositoryAssembly;

        public Log4NetLogFactory(Assembly repositoryAssembly)
        {
            _repositoryAssembly = repositoryAssembly;
            _getLogLevel = DefaultGetLogLevel;
        }

        public Log4NetLogFactory(Func<TraceEventType, Level> getLogLevel, Assembly repositoryAssembly)
        {
            _getLogLevel = getLogLevel;
            _repositoryAssembly = repositoryAssembly;
        }

        static Level DefaultGetLogLevel(TraceEventType traceEventType)
        {
            switch (traceEventType)
            {
                case TraceEventType.Critical:
                    return Level.Fatal;
                case TraceEventType.Error:
                    return Level.Error;
                case TraceEventType.Warning:
                    return Level.Warn;
                case TraceEventType.Information:
                    return Level.Info;
                case TraceEventType.Verbose:
                    return Level.Trace;
                case TraceEventType.Start:
                    return Level.Debug;
                case TraceEventType.Stop:
                    return Level.Debug;
                case TraceEventType.Suspend:
                    return Level.Debug;
                case TraceEventType.Resume:
                    return Level.Debug;
                case TraceEventType.Transfer:
                    return Level.Debug;
                default:
                    throw new ArgumentOutOfRangeException("traceEventType");
            }
        }

        public ILogger Create(string name)
        {
            return new Log4NetLogger(_repositoryAssembly, name, _getLogLevel);
        }
    }
}
