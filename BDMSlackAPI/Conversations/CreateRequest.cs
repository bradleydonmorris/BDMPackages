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

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("name", this.Name);
			yield return new KeyValuePair<String, String>("is_private", this.IsPrivate.ToString());
		}
	}
}
