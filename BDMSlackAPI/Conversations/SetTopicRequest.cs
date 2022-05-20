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

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("channel", this.Channel);
			yield return new KeyValuePair<String, String>("topic", this.Topic);
		}
	}
}
