using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BDMSlackAPI.Conversations
{
	[JsonConverter(typeof(ListRequestConverter))]
	public class ListRequest : RequestBase
	{
		public String Cursor { get; set; }
		public Boolean ExcludeArchived { get; set; }
		private Int32 _Limit = 100;

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

		public String TeamId { get; set; }
		public ConversationTypes ConversationTypes { get; set; }

		public override IEnumerable<KeyValuePair<String, String>> ToPairs()
		{
			List<String> types = new();
			if ((this.ConversationTypes & ConversationTypes.Default) == ConversationTypes.Default)
				types.AddRange(new String[] { "public_channel", "private_channel", "mpim", "im" });
			else
			{
				if ((this.ConversationTypes & ConversationTypes.PublicConversations) == ConversationTypes.PublicConversations)
					types.Add("public_channel");
				if ((this.ConversationTypes & ConversationTypes.PrivateConversations) == ConversationTypes.PrivateConversations)
					types.Add("private_channel");
				if ((this.ConversationTypes & ConversationTypes.MultiPersonIMs) == ConversationTypes.MultiPersonIMs)
					types.Add("mpim");
				if ((this.ConversationTypes & ConversationTypes.IMs) == ConversationTypes.IMs)
					types.Add("im");
			}
			yield return new KeyValuePair<String, String>("token", base.Token);
			yield return new KeyValuePair<String, String>("cursor", this.Cursor);
			yield return new KeyValuePair<String, String>("exclude_archived", this.ExcludeArchived.ToString());
			yield return new KeyValuePair<String, String>("limit", this.Limit.ToString());
			yield return new KeyValuePair<String, String>("types", String.Join(",", types));
		}
	}
}
