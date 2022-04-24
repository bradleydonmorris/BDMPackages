using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BDMSlackAPI.Conversations
{
	public class SetTopicRequest : RequestBase
	{
		[JsonProperty("channel")]
		public String Channel { get; set; }

		[JsonProperty("topic")]
		public String Topic { get; set; }

		public override String FormURLEncode()
		{
			Dictionary<String, Object> attributes = new()
            {
				{ "channel", this.Channel },
				{ "topic", this.Topic }
			};
			return base.FormURLEncodeAttributes(attributes);
		}
	}
}
