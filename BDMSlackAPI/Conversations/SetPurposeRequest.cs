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

		public override String FormURLEncode()
		{
			Dictionary<String, Object> attributes = new()
            {
				{ "channel", this.Channel },
				{ "purpose", this.Purpose }
			};
			return base.FormURLEncodeAttributes(attributes);
		}
	}
}
