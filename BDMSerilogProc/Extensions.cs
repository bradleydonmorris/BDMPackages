using Newtonsoft.Json;
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
            if (loggerConfiguration is null)
                throw new ArgumentNullException(nameof(loggerConfiguration));
            return loggerConfiguration.Sink(new BDMSerilogProcSink(
                connectionString,
                procSchema,
                procName,
                inputParamName),
                restrictedToMinimumLevel
            );
        }



		public static void WriteStringProperty(this JsonWriter writer, JsonSerializer serializer, String name, String value, Boolean includeNull = false)
		{
			if (!String.IsNullOrEmpty(value))
			{
				writer.WritePropertyName(name);
				serializer.Serialize(writer, value);
			}
			else if (includeNull)
			{
				writer.WritePropertyName(name);
				serializer.Serialize(writer, null);
			}
		}

		public static void WriteBooleanProperty(this JsonWriter writer, JsonSerializer serializer, String name, Boolean value)
		{
			writer.WritePropertyName(name);
			serializer.Serialize(writer, value.ToString().ToLower());
		}

		public static void WriteInt32Property(this JsonWriter writer, JsonSerializer serializer, String name, Int32 value)
		{
			writer.WritePropertyName(name);
			serializer.Serialize(writer, value.ToString());
		}

		public static void WriteInt64Property(this JsonWriter writer, JsonSerializer serializer, String name, Int64 value)
		{
			writer.WritePropertyName(name);
			serializer.Serialize(writer, value.ToString());
		}

		public static void WriteProperty(this JsonWriter writer, JsonSerializer serializer, Object name, Object value, Boolean includeNull = false)
		{
			writer.WritePropertyName(name.ToString());
			if (value is not null)
				serializer.Serialize(writer, value.ToString());
			else if (includeNull)
				serializer.Serialize(writer, null);
		}

		public static DateTime ToUnixTime(this Int64 seconds)
		{
			return (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(seconds);
		}
	}
}
