using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class ListResponse : ResponseBase
	{
		[JsonProperty("channels")]
		public List<Conversation> Conversations { get; set; }
	}
}
