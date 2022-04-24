using Newtonsoft.Json;
using System;

namespace BDMSlackAPI
{
	public class ResponseBase
	{
		[JsonProperty("ok")]
		public Boolean Ok { get; set; }

		[JsonProperty("response_metadata")]
		public ResponseMetaData ResponseMetaData { get; set; }

		[JsonProperty("error")]
		public String Error { get; set; }
	}
}
