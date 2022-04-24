using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDMJsonConverters
{
	public class DateConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is DateTime?)
			{
				DateTime? dateTimeValue = value as DateTime?;
				if (dateTimeValue.HasValue)
					writer.WriteValue(dateTimeValue.Value.ToString("yyyy-MM-dd"));
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DateTime? returnValue = null;
			if (reader.TokenType == JsonToken.String)
			{
				JObject value = JObject.Load(reader);
				returnValue = DateTime.Parse(value.ToString() + " 00:00:00.0000000");
			}
			return returnValue;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(DateTime?).IsAssignableFrom(objectType);
		}
	}
}