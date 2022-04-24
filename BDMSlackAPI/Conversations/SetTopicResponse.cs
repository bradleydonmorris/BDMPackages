using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class SetTopicResponse : ResponseBase
	{
		[JsonProperty("channel")]
		public Conversation Conversation { get; set; }
	}
}
