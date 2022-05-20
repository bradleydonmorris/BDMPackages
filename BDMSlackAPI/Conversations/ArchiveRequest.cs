using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BDMSlackAPI.Conversations
{
	public class ArchiveRequest : RequestBase
	{
		[JsonProperty("channel")]
		public String Channel { get; set; }

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("channel", this.Channel);
		}
	}
}
