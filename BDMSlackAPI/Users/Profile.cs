using Newtonsoft.Json;
using System;

namespace BDMSlackAPI.Users
{
	public class Profile
	{
		[JsonProperty("team")]
		public String Team { get; set; }

		[JsonProperty("real_name")]
		public String RealName { get; set; }

		[JsonProperty("real_name_normalized")]
		public String RealNameNormalized { get; set; }

		[JsonProperty("display_name")]
		public String DisplayName { get; set; }

		[JsonProperty("display_name_normalized")]
		public String DisplayNameNormalized { get; set; }

		[JsonProperty("first_name")]
		public String FirstName { get; set; }

		[JsonProperty("last_name")]
		public String LastName { get; set; }

		[JsonProperty("title")]
		public String Title { get; set; }

		[JsonProperty("email")]
		public String Email { get; set; }

		[JsonProperty("phone")]
		public String Phone { get; set; }

		[JsonProperty("skype")]
		public String Skype { get; set; }

		[JsonProperty("status_text")]
		public String StatusText { get; set; }

		[JsonProperty("status_emoji")]
		public String StatusEmoji { get; set; }

		[JsonProperty("avatar_hash")]
		public String AvatarHash { get; set; }

		[JsonProperty("image_24")]
		public String Image24 { get; set; }

		[JsonProperty("image_32")]
		public String Image32 { get; set; }

		[JsonProperty("image_48")]
		public String Image48 { get; set; }

		[JsonProperty("image_72")]
		public String Image72 { get; set; }

		[JsonProperty("image_192")]
		public String Image192 { get; set; }

		[JsonProperty("image_512")]
		public String Image512 { get; set; }
	}
}
