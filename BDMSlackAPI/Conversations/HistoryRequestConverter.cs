using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class HistoryRequestConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			HistoryRequest request = value as HistoryRequest;
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
			writer.WriteBooleanProperty(serializer, "include_all_metadata", request.IncludeAllMetaData);
			writer.WriteBooleanProperty(serializer, "inclusive", request.Inclusive);
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(HistoryRequest).IsAssignableFrom(objectType);
		}
	}
}
