using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Users
{
	public class ListResponse : ResponseBase
	{
		[JsonProperty("members")]
		public List<Member> Members { get; set; }
	}
}
