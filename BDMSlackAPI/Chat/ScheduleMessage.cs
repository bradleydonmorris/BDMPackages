using Newtonsoft.Json;
using BDMSlackAPI.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDMSlackAPI.Chat
{
	[JsonConverter(typeof(ScheduleMessageRequestConverter))]
	public class ScheduleMessageRequest : RequestBase
	{
		public String Channel { get; set; }
		public DateTime PostAt { get; set; }
		public String Text { get; set; }
		public String UserName { get; set; }
		public Boolean UnfurlLinks { get; set; }
		public Boolean UnfurlMedia { get; set; }
		public List<Block> Blocks { get; set; }

		public ScheduleMessageRequest()
		{
			this.Blocks = new List<Block>();
			this.UnfurlLinks = false;
			this.UnfurlMedia = false;
		}
	}

	public class ScheduleMessageResponse : ResponseBase
	{
	}

	public class ScheduleMessageRequestConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			ScheduleMessageRequest request = value as ScheduleMessageRequest;
			writer.Formatting = Newtonsoft.Json.Formatting.Indented;
			writer.WriteStartObject();
			writer.WriteStringProperty(serializer, "token", request.Token, true);
			writer.WriteInt32Property(serializer, "pretty", request.Pretty);
			writer.WriteStringProperty(serializer, "channel", request.Channel);
			writer.WriteInt64Property(serializer, "post_at", Convert.ToInt64((request.PostAt - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds));
			writer.WriteStringProperty(serializer, "username", request.UserName);
			writer.WriteStringProperty(serializer, "text", request.Text);
			writer.WriteBooleanProperty(serializer, "unfurl_links", request.UnfurlLinks);
			writer.WriteBooleanProperty(serializer, "unfurl_media", request.UnfurlMedia);
			writer.WritePropertyName("blocks");
			writer.WriteStartArray();
			foreach (Block block in request.Blocks)
			{
				writer.WriteStartObject();
				if (block is DividerBlock)
					writer.WriteStringProperty(serializer, "type", "divider");
				else if (block is SectionBlock)
				{
					//SectionBlock sectionBlock = block as SectionBlock;
					writer.WriteStringProperty(serializer, "type", "section");
					writer.WritePropertyName("text");
					writer.WriteStartObject();
					//switch (sectionBlock.TextType)
					//{
					//	case TextType.Markdown:
					//		writer.WriteStringProperty(serializer, "type", "mrkdwn");
					//		writer.WriteStringProperty(serializer, "text", sectionBlock.Text);
					//		break;
					//}
					writer.WriteEndObject();
				}
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(ScheduleMessageRequest).IsAssignableFrom(objectType);
		}
	}
}
