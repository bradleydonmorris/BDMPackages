using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;

namespace BDMSlackAPI.Users
{
	public class LookupByEmailRequest : RequestBase
	{
		[JsonProperty("email")]
		public String Email { get; set; }

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("email", this.Email);
		}
	}
}
