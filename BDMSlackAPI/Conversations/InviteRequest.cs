using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BDMSlackAPI.Conversations
{
	[JsonConverter(typeof(InviteRequestConverter))]
	public class InviteRequest : RequestBase
	{
		public InviteRequest()
		{
			this.Users = new List<String>();
		}

		public String Channel { get; set; }

		public List<String> Users { get; set; }

		public override String FormURLEncode()
		{
			Dictionary<String, Object> attributes = new()
            {
				{ "channel", this.Channel },
				{ "users", String.Join(",", this.Users) }
			};
			return base.FormURLEncodeAttributes(attributes);
		}
	}
}
