using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class ListRequestConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			ListRequest request = value as ListRequest;
			writer.WriteStartObject();
			writer.WriteStringProperty(serializer, "token", request.Token, true);
			writer.WriteInt32Property(serializer, "pretty", request.Pretty);
			writer.WriteStringProperty(serializer, "cursor", request.Cursor);
			if (request.ExcludeArchived)
				writer.WriteBooleanProperty(serializer, "exclude_archived", request.ExcludeArchived);
			if (request.Limit >= 1 && request.Limit <= 1000)
				writer.WriteInt32Property(serializer, "limit", request.Limit);
			else
				writer.WriteInt32Property(serializer, "limit", 100);
			writer.WriteStringProperty(serializer, "team_id", request.TeamId);
			if ((request.ConversationTypes & ConversationTypes.Default) == ConversationTypes.Default)
				writer.WriteStringProperty(serializer, "types", "public_channel,private_channel,mpim,im");
			else
			{
				List<String> types = new();
				if ((request.ConversationTypes & ConversationTypes.PublicConversations) == ConversationTypes.PublicConversations)
					types.Add("public_channel");
				if ((request.ConversationTypes & ConversationTypes.PrivateConversations) == ConversationTypes.PrivateConversations)
					types.Add("private_channel");
				if ((request.ConversationTypes & ConversationTypes.MultiPersonIMs) == ConversationTypes.MultiPersonIMs)
					types.Add("mpim");
				if ((request.ConversationTypes & ConversationTypes.IMs) == ConversationTypes.IMs)
					types.Add("im");
				writer.WriteStringProperty(serializer, "types", String.Join(",", types));
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(ListRequest).IsAssignableFrom(objectType);
		}
	}
}
