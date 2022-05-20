using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BDMSlackAPI.Conversations
{
	public class HistoryResponseConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			HistoryResponse returnValue = new();
			if (reader.TokenType == JsonToken.StartObject)
			{
				JObject response = JObject.Load(reader);
				if (response["ok"] is not null)
					returnValue.Ok = response["ok"].Value<Boolean>();
				if (response["response_metadata"] is not null)
					returnValue.ResponseMetaData = response["response_metadata"].ToObject<ResponseMetaData>(serializer);
				if (response["has_more"] is not null)
					returnValue.Ok = response["has_more"].Value<Boolean>();
				if (response["scheduled_messages"] is not null)
					returnValue.Messages = response["messages"].ToObject<List<HistoryMessage>>(serializer);
			}
			return returnValue;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(HistoryResponse).IsAssignableFrom(objectType);
		}
	}
}
