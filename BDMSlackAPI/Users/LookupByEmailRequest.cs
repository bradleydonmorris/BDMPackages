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

		public override String FormURLEncode()
		{
			Dictionary<String, Object> attributes = new()
            {
				{ "email", this.Email }
			};
			return base.FormURLEncodeAttributes(attributes);
		}
	}
}
