using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BDMSlackAPI.Conversations
{
	public class ConversationsAPI
	{
		private readonly Slack _Slack;

		public ConversationsAPI(Slack slack)
		{
			this._Slack = slack;
		}

		//https://api.slack.com/methods/conversations.list
		public ListResponse List(ListRequest request)
		{
			return JsonConvert.DeserializeObject<ListResponse>(this._Slack.MakeJsonAPICall("conversations.list", request));
		}

		//https://api.slack.com/methods/conversations.invite
		public InviteResponse Invite(InviteRequest request)
		{
			return JsonConvert.DeserializeObject<InviteResponse>(this._Slack.MakeJsonAPICall("conversations.invite", request));
		}

		//https://api.slack.com/methods/conversations.setPurpose
		public SetPurposeResponse SetPurpose(SetPurposeRequest request)
		{
			return JsonConvert.DeserializeObject<SetPurposeResponse>(this._Slack.MakeJsonAPICall("conversations.setPurpose", request));
		}

		//https://api.slack.com/methods/conversations.setTopic
		public SetTopicResponse SetTopic(SetTopicRequest request)
		{
			return JsonConvert.DeserializeObject<SetTopicResponse>(this._Slack.MakeJsonAPICall("conversations.setTopic", request));
		}

		//https://api.slack.com/methods/conversations.create
		public CreateResponse Create(CreateRequest request)
		{
			return JsonConvert.DeserializeObject<CreateResponse>(this._Slack.MakeJsonAPICall("conversations.create", request));
		}

		//https://api.slack.com/methods/conversations.archive
		public ArchiveResponse Archive(ArchiveRequest request)
		{
			return JsonConvert.DeserializeObject<ArchiveResponse>(this._Slack.MakeJsonAPICall("conversations.archive", request));
		}

		//https://api.slack.com/methods/conversations.history
		//https://api.slack.com/methods/conversations.members
	}
}