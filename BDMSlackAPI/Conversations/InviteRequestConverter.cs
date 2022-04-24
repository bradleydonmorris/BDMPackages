using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class InviteRequestConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			InviteRequest request = value as InviteRequest;
			writer.WriteStartObject();
			writer.WriteStringProperty(serializer, "token", request.Token, true);
			writer.WriteInt32Property(serializer, "pretty", request.Pretty);
			writer.WriteStringProperty(serializer, "channel", request.Channel);
			writer.WriteStringProperty(serializer, "users", String.Join(",", request.Users));
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(InviteRequest).IsAssignableFrom(objectType);
		}
	}
}
