using BDMJsonConverters;
using Newtonsoft.Json;
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
            this.Exception = new(logEvent.Exception);
            this.Properties = new();
            foreach (KeyValuePair<String, LogEventPropertyValue> property in logEvent.Properties)
            {
                if (property.Value is null)
                    this.Properties.Add(property.Key, null);
                else if (property.Value is ScalarValue scalarValue)
                    this.Properties.Add(property.Key, scalarValue.Value);
                else
                    this.Properties.Add(property.Key, property.Value.ToString());
            }
            //this.Properties = new(logEvent.Properties);
        }

        [JsonConverter(typeof(DateTimeOffsetConverter))]
        [JsonProperty("Timestamp")]
        public DateTimeOffset Timestamp { get; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [JsonProperty("Level")]
        public LogEventLevel Level { get; }

        [JsonProperty("Message")]
        public String Message { get; }

        [JsonProperty("Exception")]
        public LogableException Exception { get; }

        [JsonProperty("Properties")]
        public Dictionary<String, Object> Properties { get; set; }

    }
}
