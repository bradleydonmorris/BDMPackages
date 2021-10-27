using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace BDMSerilogProc
{
	public static class Extensions
	{
        public static LoggerConfiguration BDMSerilogProc(
            this LoggerSinkConfiguration loggerConfiguration,
            String connectionString,
            String procSchema,
            String procName,
            String inputParamName,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException(nameof(loggerConfiguration));
            return loggerConfiguration.Sink(new BDMSerilogProcSink(
                connectionString,
                procSchema,
                procName,
                inputParamName),
                restrictedToMinimumLevel
            );
        }
    }
}
