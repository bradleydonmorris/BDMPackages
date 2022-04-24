using Newtonsoft.Json;
using System;

namespace BDMSlackAPI.Users
{
	public class Member
	{
		[JsonProperty("id")]
		public String Id { get; set; }

		[JsonProperty("deleted")]
		public Boolean IsDeleted { get; set; }

		[JsonProperty("is_admin")]
		public Boolean IsAdmin { get; set; }

		[JsonProperty("always_active")]
		public Boolean IsAlwaysActive { get; set; }

		[JsonProperty("is_app_user")]
		public Boolean IsAppUser { get; set; }

		[JsonProperty("is_bot")]
		public Boolean IsBot { get; set; }

		[JsonProperty("is_owner")]
		public Boolean IsOwner { get; set; }

		[JsonProperty("is_primary_owner")]
		public Boolean IsPrimaryOwner { get; set; }

		[JsonProperty("is_restricted")]
		public Boolean IsRestricted { get; set; }

		[JsonProperty("is_ultra_restricted")]
		public Boolean IsUltraRestricted { get; set; }

		[JsonProperty("update")]
		public Int32 Update { get; set; }

		[JsonProperty("name")]
		public String Name { get; set; }

		[JsonProperty("has_2fa")]
		public Boolean Has2FA { get; set; }

		[JsonProperty("team_id")]
		public String TeamId { get; set; }

		[JsonProperty("tz")]
		public String TimeZone { get; set; }

		[JsonProperty("tz_label")]
		public String TimeZoneLabel { get; set; }

		[JsonProperty("tz_offset")]
		public Int32 TimeZoneOffset { get; set; }

		[JsonProperty("is_email_confirmed")]
		public Boolean IsEmailConfimred { get; set; }

		[JsonProperty("who_can_share_contact_card")]
		public String WhoCanShareContactCard { get; set; }

		[JsonProperty("profile")]
		public Profile Profile { get; set; }
	}
}
