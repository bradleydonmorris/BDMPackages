using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BDMSlackAPI.Chat
{
	public class ChatAPI
	{
		private readonly Slack _Slack;

		public ChatAPI(Slack slack)
		{
			this._Slack = slack;
		}

		//https://api.slack.com/methods/chat.postMessage
		public PostMessageResponse PostMessage(PostMessageRequest request)
		{
			return JsonConvert.DeserializeObject<PostMessageResponse>(this._Slack.MakeJsonAPICall("chat.postMessage", request));
		}

		//https://api.slack.com/methods/chat.update
		//https://api.slack.com/methods/chat.delete

		//https://api.slack.com/methods/chat.deleteScheduledMessage
		public DeleteScheduleMessageResponse DeleteScheduledMessage(DeleteScheduleMessageRequest request)
		{
			return JsonConvert.DeserializeObject<DeleteScheduleMessageResponse>(this._Slack.MakeJsonAPICall("chat.deleteScheduledMessage", request));
		}

		//https://api.slack.com/methods/chat.scheduledMessages.list
		public ListScheduledMessagesResponse ListScheduledMessages(ListScheduledMessagesRequest request)
		{
			return JsonConvert.DeserializeObject<ListScheduledMessagesResponse>(this._Slack.MakeJsonAPICall("chat.scheduledMessages.list", request));
		}

		//https://api.slack.com/methods/chat.scheduleMessage
		public ScheduleMessageResponse ScheduleMessage(ScheduleMessageRequest request)
		{
			return JsonConvert.DeserializeObject<ScheduleMessageResponse>(this._Slack.MakeJsonAPICall("chat.scheduleMessage", request));
		}
	}
}