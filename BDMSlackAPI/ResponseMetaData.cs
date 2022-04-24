using Newtonsoft.Json;
using System;

namespace BDMSlackAPI
{
	public class ResponseMetaData
	{
		[JsonProperty("next_cursor")]
		public String NextCursor { get; set; }
	}
}
