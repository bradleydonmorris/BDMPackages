using System;
using System.Collections.Generic;
using System.Text;

namespace BDMSlackAPI.Conversations
{
	[Flags]
	public enum ConversationTypes
	{
		Default = 0,
		PublicConversations = 1,
		PrivateConversations = 2,
		MultiPersonIMs = 4,
		IMs = 8
	}
}
