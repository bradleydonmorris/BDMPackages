using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class SetPurposeResponse : ResponseBase
	{
		[JsonProperty("channel")]
		public Conversation Conversation { get; set; }
	}
}
