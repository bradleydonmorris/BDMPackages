using Newtonsoft.Json;
using System;

namespace BDMSlackAPI.Conversations
{
	public class Purpose
	{
		[JsonProperty("value")]
		public String Value { get; set; }

		[JsonProperty("creator")]
		public String Creator { get; set; }

		[JsonProperty("last_set")]
		public Int32 LastSet { get; set; }
	}
}
