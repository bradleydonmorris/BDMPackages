using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BDMSlackAPI.Conversations
{
	public class CreateRequest : RequestBase
	{
		[JsonProperty("name")]
		public String Name { get; set; }

		[JsonProperty("is_private")]
		public Boolean IsPrivate { get; set; }

		public override String FormURLEncode()
		{
			Dictionary<String, Object> attributes = new()
            {
				{ "name", this.Name },
				{ "is_private", String.Join(",", this.IsPrivate) }
			};
			return base.FormURLEncodeAttributes(attributes);
		}
	}
}
