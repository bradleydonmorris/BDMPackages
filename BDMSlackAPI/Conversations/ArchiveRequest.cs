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

		public override String FormURLEncode()
		{
			Dictionary<String, Object> attributes = new()
            {
				{ "channel", this.Channel }
			};
			return base.FormURLEncodeAttributes(attributes);
		}
	}
}
