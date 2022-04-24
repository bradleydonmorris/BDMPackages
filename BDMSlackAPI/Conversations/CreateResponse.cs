using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class CreateResponse : ResponseBase
	{
		[JsonProperty("channel")]
		public Conversation Conversation { get; set; }
	}
}
