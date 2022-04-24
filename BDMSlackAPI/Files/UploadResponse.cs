using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Files
{
	public class UploadResponse : ResponseBase
	{
		[JsonProperty("channel")]
		public Conversations.Conversation Conversation { get; set; }
	}
}
