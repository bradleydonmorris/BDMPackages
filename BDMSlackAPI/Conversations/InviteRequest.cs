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

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("channel", this.Channel);
			yield return new KeyValuePair<String, String>("users", String.Join(",", this.Users));
		}
	}
}
