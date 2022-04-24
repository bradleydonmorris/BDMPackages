using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDMSlackAPI.Conversations
{
	[JsonConverter(typeof(HistoryRequestConverter))]
	public class HistoryRequest : RequestBase
	{
		public HistoryRequest()
		{
			this._Limit = 100;
		}

		[JsonProperty("channel")]
		public String Channel { get; set; }

		[JsonProperty("cursor")]
		public String Cursor { get; set; }

		[JsonProperty("latest")]
		public Int32 Latest { get; set; }

		[JsonProperty("oldest")]
		public Int32 Oldest { get; set; }

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

		[JsonProperty("include_all_metadata")]
		public Boolean IncludeAllMetaData { get; set; }

		[JsonProperty("inclusive")]
		public Boolean Inclusive { get; set; }
	}
}
