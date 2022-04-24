using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDMSlackAPI.Chat
{
	[JsonConverter(typeof(ListScheduledMessagesRequestConverter))]
	public class ListScheduledMessagesRequest : RequestBase
	{
		public ListScheduledMessagesRequest()
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

		[JsonProperty("team_id")]
		public String TeamId { get; set; }
	}

	[JsonConverter(typeof(ListScheduledMessagesResponseConverter))]
	public class ListScheduledMessagesResponse : ResponseBase
	{
		[JsonProperty("scheduled_messages")]
		public List<ScheduledMessage> ScheduledMessages { get; set; }
	}

	[JsonConverter(typeof(ScheduledMessageConverter))]
	public class ScheduledMessage
	{
		public String Id { get; set; }
		public String ChannelId { get; set; }
		public DateTime PostAt { get; set; }
		public DateTime DateCreated { get; set; }
		public String Text { get; set; }
	}

	public class ListScheduledMessagesRequestConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			ListScheduledMessagesRequest request = value as ListScheduledMessagesRequest;
			writer.Formatting = Newtonsoft.Json.Formatting.Indented;
			writer.WriteStartObject();
			writer.WriteStringProperty(serializer, "token", request.Token, true);
			writer.WriteInt32Property(serializer, "pretty", request.Pretty);
			writer.WriteStringProperty(serializer, "channel", request.Channel);
			writer.WriteStringProperty(serializer, "cursor", request.Cursor);
			if (request.Latest > 0)
				writer.WriteInt32Property(serializer, "latest", request.Latest);
			if (request.Oldest > 0)
				writer.WriteInt32Property(serializer, "oldest", request.Oldest);
			if (request.Limit >= 1 && request.Limit <= 1000)
				writer.WriteInt32Property(serializer, "limit", request.Limit);
			else
				writer.WriteInt32Property(serializer, "limit", 100);
			writer.WriteStringProperty(serializer, "team_id", request.TeamId);
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(ListScheduledMessagesRequest).IsAssignableFrom(objectType);
		}
	}

	public class ListScheduledMessagesResponseConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			ListScheduledMessagesResponse returnValue = new();
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject response = JObject.Load(reader);
				if (response["ok"] != null)
					returnValue.Ok = response["ok"].Value<Boolean>();
				if (response["response_metadata"] != null)
					returnValue.ResponseMetaData = response["response_metadata"].ToObject<ResponseMetaData>(serializer);
				if (response["scheduled_messages"] != null)
					returnValue.ScheduledMessages = response["scheduled_messages"].ToObject<List<ScheduledMessage>>(serializer);
			}
			return returnValue;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(ListScheduledMessagesResponse).IsAssignableFrom(objectType);
		}
	}

	public class ScheduledMessageConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			ScheduledMessage returnValue = new();
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject message = JObject.Load(reader);
				if (message["id"] != null)
					returnValue.Id = message["id"].Value<String>();
				if (message["channel_id"] != null)
					returnValue.ChannelId = message["channel_id"].Value<String>();
				if (message["post_at"] != null)
					returnValue.PostAt = message["post_at"].Value<Int64>().ToUnixTime();
				if (message["date_created"] != null)
					returnValue.DateCreated = message["date_created"].Value<Int64>().ToUnixTime();
				if (message["text"] != null)
					returnValue.Text = message["text"].Value<String>();
			}
			return returnValue;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(ScheduledMessage).IsAssignableFrom(objectType);
		}
	}
}
