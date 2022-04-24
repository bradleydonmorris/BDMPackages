using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	[JsonConverter(typeof(HistoryResponseConverter))]
	public class HistoryResponse : ResponseBase
	{
		[JsonProperty("messages")]
		public List<HistoryMessage> Messages { get; set; }

		[JsonProperty("has_more")]
		public Boolean HasMore { get; set; }

	}
}
