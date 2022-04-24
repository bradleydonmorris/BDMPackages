using Newtonsoft.Json;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BDMSerilogProc
{
	public class LogableException
    {
        [JsonProperty("Data")]
        public IDictionary<Object, Object> Data { get; set; }

        [JsonProperty("HelpLink")]
        public String HelpLink { get; set; }

        [JsonProperty("HResult")]
        public Int32 HResult { get; set; }

        [JsonProperty("InnerException")]
        public LogableException InnerException { get; set; }

        [JsonProperty("Message")]
        public String Message { get; set; }

        [JsonProperty("Source")]
        public String Source { get; set; }

        [JsonProperty("StackTrace")]
        public String StackTrace { get; set; }

        [JsonProperty("TargetSite")]
        public MethodBase TargetSite { get; set; }


        public LogableException() { }
        public LogableException(Exception excption)
        {
            this.Data = (IDictionary<Object, Object>)excption.Data;
            this.HelpLink = excption.HelpLink;
            this.HResult = excption.HResult;
            this.Message = excption.Message;
            this.Source = excption.Source;
            this.StackTrace = excption.StackTrace;
            this.TargetSite = excption.TargetSite;
            if (excption.InnerException != null)
                this.InnerException = new(excption.InnerException);
        }
    }
}
