using Newtonsoft.Json;
using System;

namespace BDMSlackAPI.Conversations
{
	public class Conversation
	{
		[JsonProperty("id")]
		public String Id { get; set; }

		[JsonProperty("name")]
		public String Name { get; set; }

		[JsonProperty("name_normalized")]
		public String NameNormalized { get; set; }

		[JsonProperty("is_channel")]
		public Boolean IsChannel { get; set; }

		[JsonProperty("is_group")]
		public Boolean IsGroup { get; set; }

		[JsonProperty("is_im")]
		public Boolean IsIM { get; set; }

		[JsonProperty("is_mpim")]
		public Boolean IsMPIM { get; set; }

		[JsonProperty("is_private")]
		public Boolean IsPrivate { get; set; }

		[JsonProperty("is_archived")]
		public Boolean IsArchived { get; set; }

		[JsonProperty("is_general")]
		public Boolean IsGeneral { get; set; }

		[JsonProperty("created")]
		public Int32 Created { get; set; }

		[JsonProperty("creator")]
		public String Creator { get; set; }

		[JsonProperty("topic")]
		public Topic Topic { get; set; }

		[JsonProperty("purpose")]
		public Topic Purpose { get; set; }
	}
}