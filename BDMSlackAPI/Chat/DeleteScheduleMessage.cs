using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDMSlackAPI.Chat
{
	public class DeleteScheduleMessageRequest : RequestBase
	{
		[JsonProperty("channel")]
		public String Channel { get; set; }

		[JsonProperty("scheduled_message_id")]
		public String ScheduledMessageId { get; set; }
	}

	public class DeleteScheduleMessageResponse : ResponseBase
	{
	}
}
