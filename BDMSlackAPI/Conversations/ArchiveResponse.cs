using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class ArchiveResponse : ResponseBase
	{
		[JsonProperty("channel")]
		public Conversation Conversation { get; set; }
	}
}
