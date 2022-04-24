using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace BDMJsonConverters
{
	public class DateTimeOffsetConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is DateTimeOffset?)
			{
				DateTimeOffset? dateTimeOffsetValue = value as DateTimeOffset?;
				if (dateTimeOffsetValue.HasValue)
					writer.WriteValue(dateTimeOffsetValue.Value.ToString("yyyy-MM-dd HH:mm:ss.fffffff K"));
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DateTimeOffset? returnValue = null;
			if (reader.TokenType == JsonToken.String)
			{
				JObject value = JObject.Load(reader);
				returnValue = DateTimeOffset.Parse(value.ToString());
			}
			return returnValue;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(DateTimeOffset?).IsAssignableFrom(objectType);
		}
	}
}