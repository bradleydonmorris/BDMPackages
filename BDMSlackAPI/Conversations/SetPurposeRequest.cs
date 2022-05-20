using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BDMSlackAPI.Conversations
{
	public class SetPurposeRequest : RequestBase
	{
		[JsonProperty("channel")]
		public String Channel { get; set; }

		[JsonProperty("purpose")]
		public String Purpose { get; set; }

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("channel", this.Channel);
			yield return new KeyValuePair<String, String>("purpose", this.Purpose);
		}
	}
}
