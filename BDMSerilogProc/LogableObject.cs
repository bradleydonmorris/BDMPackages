using Serilog.Events;
using System;
using System.Collections.Generic;

namespace BDMSerilogProc
{
	public class LogableObject
    {
        public LogableObject() { }
        public LogableObject(LogEvent logEvent)
        {
            this.Timestamp = logEvent.Timestamp;
            this.Level = logEvent.Level;
            this.Message = logEvent.RenderMessage(null);
            this.Exception = logEvent.Exception;
            this.Properties = new();
            foreach (KeyValuePair<String, LogEventPropertyValue> property in logEvent.Properties)
            {
                if (property.Value is ScalarValue scalarValue)
                    this.Properties.Add(property.Key, scalarValue.Value);
                else
                    this.Properties.Add(property.Key, property.Value.ToString());
            }
            //this.Properties = new(logEvent.Properties);
        }
        public DateTimeOffset Timestamp { get; }
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public LogEventLevel Level { get; }
        public String Message { get; }
        public Exception Exception { get; }
        public Dictionary<String, Object> Properties { get; set; }

    }
}
