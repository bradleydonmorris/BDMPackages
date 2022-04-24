using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Users
{
	public class LookupByEmailResponse : ResponseBase
	{
		[JsonProperty("user")]
		public Member Member { get; set; }
	}
}
