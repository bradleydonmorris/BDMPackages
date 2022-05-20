using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BDMSlackAPI.Users
{
	public class ListRequest : RequestBase
	{
		public ListRequest()
		{
			this._Limit = 100;
			this.IncludeLocale = false;
		}

		[JsonProperty("cursor")]
		public String Cursor { get; set; }

		[JsonProperty("include_locale")]
		public Boolean IncludeLocale { get; set; }

		private Int32 _Limit = 100;
		[JsonProperty("limit")]
		public Int32 Limit
		{
			get
			{
				return this._Limit;
			}
			set
			{
				if (value >= 1 && value <= 1000)
					this._Limit = value;
				else
					this._Limit = 100;
			}
		}

		[JsonProperty("team_id")]
		public String TeamId { get; set; }

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("cursor", this.Cursor);
			yield return new KeyValuePair<String, String>("include_locale", this.IncludeLocale.ToString());
			yield return new KeyValuePair<String, String>("limit", this.Limit.ToString());
			yield return new KeyValuePair<String, String>("team_id", this.TeamId);
		}
	}
}
